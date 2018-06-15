using System.Windows;
using System.Windows.Controls;
using StructureMap;
using TpProjektForms.Common;
using TpProjektForms.Common.Factories;
using TpProjektForms.Utility;
using TpProjektForms.ViewModels.Dashboard;

namespace TpProjektForms
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            IOCContainer.Initialize();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            IContainer container = ObjectFactory.Container;

            MainWindow = container.GetInstance<MainWindow>();

            if (MainWindow != null)
            {
                MainWindow.Content = PageFactory.Get(PageName.Dashboard);
                MainWindow.ShowDialog();
            }
        }
    }
}
