using StructureMap;
using TpProjektForms.Common.Factories;
using TpProjektForms.ViewModels.Dashboard;
using TpProjektForms.Views;
using TpProjektModel.Reflection;
using TpProjektModel.Serialization;

namespace TpProjektForms.Common
{
    public static class IOCContainer
    {
        public static void Initialize()
        {
            ObjectFactory.Initialize(x =>
            {
                Reflector reflector = new Reflector();
                ModelSerializer modelSerializer = new ModelSerializer();
                DashboardViewModel dashboardViewModel = new DashboardViewModel(reflector, modelSerializer);

                x.For<MainWindow>().Use<MainWindow>();
                x.For<IReflector>().Use(reflector);
                x.For<IModelSerializer>().Use(modelSerializer);
              
                x.For<IDashboardViewModel>().Use(dashboardViewModel);
                x.For<DashboardView>().Use<DashboardView>();
            });

            PageFactory.Initialize(new PageRegistry());
        }
    }
}