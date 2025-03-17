using CitasMedicas.Application.Services;
using CitasMedicas.Domain.Interfaces;
using CitasMedicas.Infrastructure.Controllers;
using CitasMedicas.Infrastructure.Messaging;
using CitasMedicas.Infrastructure.Repository;
using MediatR;
using RabbitMQ.Client;
using RecetasMedicas.Application.Services;
using RecetasMedicas.Domain.Interfaces;
using RecetasMedicas.Infrastructure.Messaging;
using RecetasMedicas.Infrastructure.Repository;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using System;
using System.Linq;
using System.Web.Mvc;
using RecetasMedicas.Application.DTO;
using RecetasMedicas.Application.Queries;
using System.Collections.Generic;


namespace RecetasMedicas
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            // Registro de dependencias
            RegisterDependencies(container);

            // Registrar controladores
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            // Verificar configuración de SimpleInjector
            container.Verify();

            // Configurar Web API con SimpleInjector
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);

            RunBackgroundTasks(container).GetAwaiter();

        }

        private void RegisterDependencies(Container container)
        {
            container.Register<IConnectionFactory>(() => new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            }, Lifestyle.Singleton);

            container.Register<IRabitMqRepository, RabbitMQProduce>(Lifestyle.Singleton);
            container.Register<RabbitMQConsumer>(Lifestyle.Scoped);

            container.Register<CitaMedicaContext>(Lifestyle.Scoped);
            container.Register<FormulaMedicaContext>(Lifestyle.Scoped);

            container.Register<ICitaMedicaRepository, CitaMedicaRepository>(Lifestyle.Scoped);
            container.Register<IFormulaMedicaRepository, FormulaMedicaRepository>(Lifestyle.Scoped);

            container.Register<ICitaMedicaServices, CitaMedicaServices>(Lifestyle.Scoped);
            container.Register<IFormulaMedicaServices, FormulaMedicaServices>(Lifestyle.Scoped);
            container.Register<PersonaClient>(() => new PersonaClient(new HttpClient()), Lifestyle.Singleton);

           


            // Registrar AutoMapper
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
            });
            container.RegisterInstance<IMapper>(mapperConfig.CreateMapper());

            // Registrar ServiceFactory como Singleton
            container.RegisterSingleton<ServiceFactory>(() => type => container.GetInstance(type));

            // Registrar IMediator correctamente
            container.RegisterSingleton<IMediator>(() =>
                new Mediator(container.GetInstance<ServiceFactory>()));

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();



            var requestHandlers = assemblies.SelectMany(a => a.GetTypes())
                                    .Where(t => !t.IsAbstract && !t.IsInterface) // Solo clases concretas
                                    .Where(t => t.GetInterfaces()
                                                 .Any(i => i.IsGenericType &&
                                                           i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)));

            foreach (var handler in requestHandlers)
            {
                var handlerInterface = handler.GetInterfaces()
                                              .First(i => i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>));


            }

            // Registrar todos los `IRequestHandler<TRequest, TResponse>`
            container.Register(typeof(IRequestHandler<,>), assemblies);

            container.Collection.Register(typeof(INotificationHandler<>), assemblies);
            container.Collection.Register(typeof(IPipelineBehavior<,>), assemblies);

        }

        private async Task RunBackgroundTasks(Container container)
        {
            using (AsyncScopedLifestyle.BeginScope(container))
            {
                var receptorMQ = container.GetInstance<RabbitMQConsumer>(); await Task.Run(() => receptorMQ.Escuchar());
            }

        }

    }
}