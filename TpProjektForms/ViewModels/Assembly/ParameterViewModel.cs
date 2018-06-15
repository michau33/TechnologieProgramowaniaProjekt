using System;
using TpProjektForms.Utility;
using TpProjektForms.ViewModels.TreeView;
using TpProjektModel.Reflection.Model;

namespace TpProjektForms.ViewModels.Assembly
{
    public class ParameterViewModel : TreeNodeViewModel
    {
        public readonly ParameterModel ParameterModel;

        public ParameterViewModel(ParameterModel parameterModel, TreeNodeViewModel parent)
            : base(parameterModel.Name, parent)
        {
            ParameterModel = parameterModel;

            Name = ParameterModel.Type != null ? $"{ParameterModel.Type.Name} " : String.Empty;
            Name += ParameterModel.Name;

            if (ParameterModel.Type != null)
                AddDummyChild();
        }

        protected override void LoadChildren()
        {
            base.LoadChildren();

            Children.Add(ParameterModel.Type.CreateTypeViewmodel(this));
        }

        [Obsolete]
        public ParameterViewModel() { }
    }
}