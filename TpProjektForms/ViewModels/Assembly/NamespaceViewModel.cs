using System;
using TpProjektForms.Utility;
using TpProjektForms.ViewModels.TreeView;
using TpProjektModel.Reflection.Model;

namespace TpProjektForms.ViewModels.Assembly
{
    public class NamespaceViewModel : TreeNodeViewModel
    {
        public readonly NamespaceModel NamespaceModel;

        public NamespaceViewModel(NamespaceModel namespaceModel, TreeNodeViewModel parent)
            : base(namespaceModel.Name, parent)
        {
            NamespaceModel = namespaceModel;

            if (NamespaceModel.Namespaces?.Count > 0 || NamespaceModel?.Types.Count > 0)
                AddDummyChild();
        }

        protected override void LoadChildren()
        {
            Children.Clear();
            
            NamespaceModel.Namespaces?.ForEach(@namespace => Children.Add(new NamespaceViewModel(@namespace, this)));
            NamespaceModel.Types?.ForEach(type => Children.Add(type.CreateTypeViewmodel(this)));
        }

        [Obsolete]
        public NamespaceViewModel() { }
    }
}