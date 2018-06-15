using System;
using TpProjektForms.ViewModels.TreeView;

namespace TpProjektForms.ViewModels.Assembly
{
    public class AttributeViewModel : TreeNodeViewModel
    {
        public AttributeViewModel(Attribute attribute, TreeNodeViewModel parent)
            : base(attribute.ToString(), parent)
        {
            
        }

        [Obsolete]
        public AttributeViewModel() { }
    }
}