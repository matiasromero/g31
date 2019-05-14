using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HomeSwitchHome.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace HomeSwitchHome.API.Config
{
    public static class AuthenticationServiceCollectionExtensions
    {
        public static void ConfigureAuthentication(this IServiceCollection services, AuthenticationConfig configuration)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.JWT_SECRET_KEY));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature);

            services.AddAuthentication(x =>
                    {
                        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                    .AddJwtBearer(x =>
                    {
                        x.RequireHttpsMetadata = false;
                        x.SaveToken = true;
                        x.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = signingKey,
                            ValidIssuer = configuration.JWT_ISSUER_TOKEN,
                            ValidAudience = configuration.JWT_AUDIENCE_TOKEN,
                            ValidateIssuer = false,
                            ValidateAudience = false
                        };
                        x.Events = new JwtBearerEvents
                        {
                            OnMessageReceived = context =>
                            {
                                if (context.Request.Query.TryGetValue("access_token", out var token))
                                {
                                    context.Token = token;
                                }

                                return Task.CompletedTask;
                            }
                        };
                    });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(JwtBearerDefaults.AuthenticationScheme, policy =>
                {
                    policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireClaim(ClaimTypes.NameIdentifier);
                    policy.RequireClaim(ClaimTypes.GivenName);
                });
            });

            services.AddSingleton(signingCredentials);
            services.AddSingleton(configuration);
        }
    }
}