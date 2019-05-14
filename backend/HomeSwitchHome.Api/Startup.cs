using Autofac;
using Autofac.Extensions.DependencyInjection;
using HomeSwitchHome.API.Config;
using HomeSwitchHome.API.Filters;
using HomeSwitchHome.API.Infrastructure;
using HomeSwitchHome.Application;
using HomeSwitchHome.Application.Validators.Users;
using HomeSwitchHome.Infrastructure;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Security.Claims;
using SwaggerOptions = HomeSwitchHome.API.Options.SwaggerOptions;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]

namespace HomeSwitchHome.API
{
    public class Startup
    {
        private static readonly Logger logger = LogManager.GetLogger(typeof(Startup).FullName);
        private readonly AppConfiguration config;
        private ILifetimeScope container;

        public Startup(IHostingEnvironment env, IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder()
                          .SetBasePath(env.ContentRootPath)
                          .AddJsonFile("appsettings.json", true, true)
                          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                          .AddEnvironmentVariables();

            config = configuration.Get<AppConfiguration>();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc(config =>
                    {
                        config.Filters.Add(new NhUnitOfWorkFilter());
                        config.Filters.Add(typeof(CustomExceptionFilterAttribute));
                        config.Filters.Add(new ValidateModelAttribute());
                    }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                    .AddFluentValidation(fv =>
                                             fv.RegisterValidatorsFromAssemblyContaining<
                                                 AuthenticateUserRequestValidator>());

            services.AddHttpContextAccessor();
            services.AddSwaggerGen(x => { x.SwaggerDoc("v1", new Info { Title = "Digital Menu API", Version = "v1" }); });

            services.ConfigureAuthentication(config.Authentication);

            AppBootstrapper.InitializeContainer(config, builder =>
            {
                builder.RegisterInstance(config);
                builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();
                builder.Register(c =>
                {
                    var requestUser = c.Resolve<IHttpContextAccessor>().HttpContext?.User;
                    if (requestUser == null || !requestUser.Identity.IsAuthenticated)
                        return ClaimsPrincipalHelper.System;
                    return requestUser;
                }).As<ClaimsPrincipal>();
                builder.Populate(services);
            });
            container = AppBootstrapper.GetContainer();

            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseCors(x => x.AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader());

            var swaggerOptions = new SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);
            app.UseSwagger(option => { option.RouteTemplate = swaggerOptions.JsonRoute; });
            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint(swaggerOptions.UiEndpoint, swaggerOptions.Description);
            });

            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseMvc();

            logger.Info("Listening for requests on: {0}",
                        string.Join(", ", app.ServerFeatures.Get<IServerAddressesFeature>().Addresses));
        }
    }
}