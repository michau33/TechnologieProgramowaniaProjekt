using System.Windows.Controls;
using StructureMap;
using StructureMap.Configuration.DSL;
using TpProjektForms.ViewModels.Base;

namespace TpProjektForms.Common.Factories
{
    public static class PageFactory
    {
        public static void Initialize(Registry registry)
        {
            ObjectFactory.Configure(o => o.AddRegistry(registry));
        }

        public static ContentControl Get(PageName pageName)
        {
            ContentControl control = ObjectFactory.GetNamedInstance<ContentControl>(pageName.ToString());

            control.Loaded += (s, e) => {
                control.DataContext = ObjectFactory.GetNamedInstance<BaseViewModel>(pageName.ToString());
            };

            return control;
        }
    }

    public enum PageName
    {
        Unknown = 0,
        MainWindow,
        Dashboard,
    }
}