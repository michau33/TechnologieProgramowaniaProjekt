using System.Windows.Controls;
using StructureMap.Configuration.DSL;
using TpProjektForms.ViewModels.Base;
using TpProjektForms.ViewModels.Dashboard;
using TpProjektForms.Views;

namespace TpProjektForms.Common.Factories
{
    public class PageRegistry : Registry
    {
        public PageRegistry()
        {
            Register<DashboardView, DashboardViewModel>(PageName.Dashboard);
        }

        void Register<TControl, TViewModel>(PageName pageName)
            where TControl : ContentControl where TViewModel : BaseViewModel
        {
            For<ContentControl>().Singleton().Use<TControl>().Named(pageName.ToString());
            For<BaseViewModel>().Singleton().Use<TViewModel>().Named(pageName.ToString());
        }
    }
}