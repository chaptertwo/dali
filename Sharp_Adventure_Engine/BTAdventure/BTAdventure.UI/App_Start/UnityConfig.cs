using BTAdventure.Data.DapperRepositories;
using BTAdventure.Interfaces;
using System.Web.Http;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Mvc5;

namespace BTAdventure.UI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();


            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IPlayerCharacterRepository, DapperPlayerCharacterRepository>();
            container.RegisterType<IGameRepository, DapperGameRepository>();


            // container.RegisterType<ILevelRepository, DapperLevelRepository>();

            container.RegisterType<IEventChoiceRepository, DapperEventChoiceRepository>();

            // container.RegisterType<ILevelRepository, DapperLevelRepository>();
            container.RegisterType<IEventChoiceRepository, DapperEventChoiceRepository>();

            container.RegisterType<IOutcomeRepository, DapperOutcomeRepository>();
            container.RegisterType<IPlayerRepository, DapperPlayerRepository>();
            container.RegisterType<ISceneRepository, DapperSceneRepository>();
            container.RegisterType<IEndingRepository, DapperEndingRepository>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
    }
}