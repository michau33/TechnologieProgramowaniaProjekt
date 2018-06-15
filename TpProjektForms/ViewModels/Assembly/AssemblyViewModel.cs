using System;
using TpProjektForms.ViewModels.TreeView;
using TpProjektModel.Reflection.Model;

namespace TpProjektForms.ViewModels.Assembly
{
    public class AssemblyViewModel : TreeNodeViewModel
    {
        public readonly AssemblyModel AssemblyModel;

        public AssemblyViewModel(AssemblyModel assemblyModel)
            : base(assemblyModel.Name, null)
        {
            AssemblyModel = assemblyModel;

            if (AssemblyModel.Namespaces?.Count > 0)
                AddDummyChild();
        }

        protected override void LoadChildren()
        {
            base.LoadChildren();

            AssemblyModel.Namespaces?.ForEach(@namespace => Children.Add(new NamespaceViewModel(@namespace, this)));
        }

        [Obsolete]
        public AssemblyViewModel() { }
    }
}