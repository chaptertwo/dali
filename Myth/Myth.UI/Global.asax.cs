using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Myth.Data.Repositories;
using Myth.Domain.Interfaces;
using Myth.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Myth.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ConfigureAutofac();
        }

        void ConfigureAutofac()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()); //NEW
            builder.RegisterType<CreatureRepository>().As<ICreatureRepository>();
            builder.RegisterType<FootprintRepository>().As<IFootprintRepository>();
            builder.RegisterType<NestRepository>().As<INestRepository>();
            builder.RegisterType<RegionRepository>().As<IRegionRepository>();
            builder.RegisterType<TraitRepository>().As<ITraitRepository>();
            builder.RegisterType<TypeRepository>().As<ITypeRepository>();
            builder.RegisterType<MythService>().AsSelf();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            //config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            //DependencyResolver.SetResolver(new AutofacWebApiDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

        }
    }
}
