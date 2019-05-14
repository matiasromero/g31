using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Autofac;
using HomeSwitchHome.Application.Validators.Users;
using FluentValidation;
using Module = Autofac.Module;

namespace HomeSwitchHome.API.Config
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(AppAssemblies.All)
                   .Where(t => new[]
                   {
                       "Calculator",
                       "Service",
                       "Creator",
                       "Query",
                       "Queries",
                       "Provider",
                       "Parser",
                       "Builder",
                       "Process",
                       "Factory",
                       "Checker",
                       "Strategy",
                       "Indexer",
                       "Searcher",
                       "Translator",
                       "Mapper",
                       "Manager",
                       "Exporter",
                       "Binder",
                       "Notifier",
                       "Locator",
                       "Repository",
                       "Controller",
                       "Synchronizer",
                       "TextGenerator",
                       "Proyector",
                       "Planner",
                       "Updater",
                       "Recalculator",
                       "Job",
                       "Endpoint",
                       "Counter",
                       "Reader",
                       //"Validator",
                       "Generator",
                       "Persister",
                       "Importer",
                       "Executor",
                       "Workflow",
                       "Adapter"
                   }.Any(y =>
                   {
                       var a = t.Name;
                       return a.EndsWith(y);
                   }))
                   .Where(t => !typeof(BackgroundWorker).IsAssignableFrom(t))
                   .AsSelf()
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(AppAssemblies.All)
                   .Where(t => typeof(BackgroundWorker).IsAssignableFrom(t))
                   .AsImplementedInterfaces()
                   .SingleInstance();

            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(CreateUserRequestValidator)))
                   .AsClosedTypesOf(typeof(IValidator<>))
                   .InstancePerLifetimeScope();
        }
    }
}