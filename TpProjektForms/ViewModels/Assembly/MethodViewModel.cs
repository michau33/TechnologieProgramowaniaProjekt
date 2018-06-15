using System;
using System.Linq;
using TpProjektForms.Utility;
using TpProjektForms.ViewModels.TreeView;
using TpProjektModel.Reflection.Enums;
using TpProjektModel.Reflection.Model;

namespace TpProjektForms.ViewModels.Assembly
{
    public class MethodViewModel : TreeNodeViewModel
    {
        public readonly MethodModel MethodModel;

        public MethodViewModel(MethodModel methodModel, TreeNodeViewModel parent)
            : base(methodModel.Name, parent)
        {
            MethodModel = methodModel;

            Name = MethodModel.AccessLevel != AccessLevel.None ? $"{MethodModel.AccessLevel.ToString().ToLower()} " : String.Empty;
            Name += MethodModel.Modifier != Modifiers.Nothing ? $"{MethodModel.Modifier.ToString().ToLower()} " : String.Empty;
            Name += MethodModel.ReturnType != null ? $"{MethodModel.ReturnType.Name} " : String.Empty;
            Name += $"{MethodModel.Name} ";
            Name += MethodModel.GenericParameters?.Count > 0 ? $"<{String.Join(",", MethodModel.GenericParameters?.Select(type => String.Join(" ", type.BaseType.Name, type.Name)).ToArray())} > " : String.Empty;
            Name += MethodModel.IsExtension ? "(extension method)" : String.Empty;

            if (MethodModel.ReturnType != null || MethodModel.Parameters?.Count > 0 || MethodModel.Attributes?.Count > 0)
                AddDummyChild();
        }

        protected override void LoadChildren()
        {
            base.LoadChildren();

            Children.Add(MethodModel.ReturnType?.CreateTypeViewmodel(this));
            MethodModel.GenericParameters?.ForEach(param => Children.Add(param.CreateTypeViewmodel(this)));
            MethodModel.Parameters?.ForEach(parameter => Children.Add(new ParameterViewModel(parameter, this)));
            MethodModel.Attributes?.ForEach(attribute => Children.Add(new AttributeViewModel(attribute, this)));
        }

        [Obsolete]
        public MethodViewModel() { }
    }
}