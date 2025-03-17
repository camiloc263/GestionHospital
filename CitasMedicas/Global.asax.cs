using AutoMapper;
using CitasMedicas.Application.Commands;
using CitasMedicas.Application.DTO;
using CitasMedicas.Application.Queries;
using CitasMedicas.Application.Services;
using CitasMedicas.Domain.Interfaces;
using CitasMedicas.Infrastructure.Messaging;
using CitasMedicas.Infrastructure.Repository;
using MediatR;
using RabbitMQ.Client;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;


namespace CitasMedicas
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var container = new Container();
            
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

           
            //registro de dependencias
            RegisterDependencies(container);
            //registro de controladores
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            //Verifica la configuracion
            container.Verify();
            // COnfigurar web api
            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            

        }

        private void RegisterDependencies(Container container)
        {
            container.Register<IConnectionFactory>(() => new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            }, Lifestyle.Singleton);

            // 2️⃣ Registrar RabbitMQProduce correctamente
            container.Register<IRabitMqRepository, RabbitMQProduce>(Lifestyle.Scoped);

            // 3️⃣ Registrar otros servicios
            container.Register<CitaMedicaContext>(Lifestyle.Scoped);
            container.Register<ICitaMedicaRepository,CitaMedicaRepository >(Lifestyle.Scoped);
            container.Register<ICitaMedicaServices, CitaMedicaServices>(Lifestyle.Scoped);            
            container.Register<HttpClient>(() => new HttpClient { BaseAddress = new Uri("https://localhost:44381/api/persona") }, Lifestyle.Scoped);
            container.Register<PersonaClient>(Lifestyle.Scoped);
            container.Register<IRequestHandler<GetCitasByIdQuery, CitaDto>, GetCitasByIdQueryHandler>(Lifestyle.Scoped);
            container.Register<IRequestHandler<GetAllCitasQuery, List<CitaDto>>, GetAllCitasQueryHandler>(Lifestyle.Scoped);
            container.Register<IRequestHandler<UpdateCitaCommand, bool>, UpdateCitaCommandHandler>(Lifestyle.Scoped);
            container.Register<IRequestHandler<AddCitaCommand, bool>, AddCitasCommandHandler>(Lifestyle.Scoped);
            container.Register<IRequestHandler<DeleteCitaCommand, bool>, DeleteCitaCommandHandler>(Lifestyle.Scoped);

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


            container.Collection.Register(typeof(INotificationHandler<>), assemblies);


            container.Collection.Register(typeof(IPipelineBehavior<,>), assemblies);



        }

    }
}
