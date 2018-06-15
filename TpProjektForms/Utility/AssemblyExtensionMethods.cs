using StructureMap;
using TpProjektForms.Common;
using TpProjektForms.ViewModels.Assembly;
using TpProjektForms.ViewModels.Dashboard;
using TpProjektForms.ViewModels.TreeView;
using TpProjektModel.Reflection;
using TpProjektModel.Reflection.Model;

namespace TpProjektForms.Utility
{
    public static class AssemblyExtensionMethods
    {
        public static TypeViewModel CreateTypeViewmodel(this TypeModel typeModel, TreeNodeViewModel parent)
        {
            IReflector reflector = ((DashboardViewModel) ObjectFactory.Container.GetInstance<IDashboardViewModel>()).Reflector;

            return new TypeViewModel(reflector.Beans[typeModel.Name], parent);
        }
    }
}