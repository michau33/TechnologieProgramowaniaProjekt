using System;
using System.Linq;
using TpProjektForms.Utility;
using TpProjektForms.ViewModels.TreeView;
using TpProjektModel.Reflection.Enums;
using TpProjektModel.Reflection.Model;

namespace TpProjektForms.ViewModels.Assembly
{
    public class TypeViewModel : TreeNodeViewModel
    {
        public readonly TypeModel TypeModel;

        public TypeViewModel(TypeModel typeModel, TreeNodeViewModel parent)
            : base(typeModel.Name, parent)
        {
            TypeModel = typeModel;

            Name = TypeModel.AccessLevel != AccessLevel.None ? $"{TypeModel.AccessLevel.ToString().ToLower()} " : String.Empty;
            Name += TypeModel.Modifier != Modifiers.Nothing ? $"{TypeModel.Modifier.ToString().ToLower()} " : String.Empty;
            Name += TypeModel.TypeEnum != TypeEnum.None ? $"{TypeModel.TypeEnum.ToString().ToLower()} " : String.Empty;
            Name += TypeModel.GenericParameters?.Count > 0 ? $"<{String.Join(",", TypeModel.GenericParameters?.Select(type => String.Join(" ", type.BaseType.Name, type.Name)).ToArray())}> " : String.Empty;
            Name += TypeModel.Name;
            Name += TypeModel.BaseType != null ? $" : {TypeModel.BaseType.Name} " : String.Empty;
            Name += TypeModel.Interfaces != null ? $" : {String.Join(", ", TypeModel.Interfaces?.Select(@interface => @interface.Name).ToArray())}" : String.Empty;

            if (TypeModel.BaseType != null || TypeModel.Properties?.Count > 0 || TypeModel.NestedTypes?.Count > 0 || TypeModel.Constructors?.Count > 0
                || TypeModel.Fields?.Count > 0 || TypeModel.Methods?.Count > 0 || TypeModel.Attributes?.Count > 0)
                AddDummyChild();
        }

        protected override void LoadChildren()
        {
            base.LoadChildren();

            if (TypeModel.BaseType != null)
                Children.Add(TypeModel.BaseType.CreateTypeViewmodel(this));

            TypeModel.GenericParameters?.ForEach(param => Children.Add(param.CreateTypeViewmodel(this)));
            TypeModel.Attributes?.ForEach(attr => Children.Add(new AttributeViewModel(attr, this)));
            TypeModel.Constructors?.ForEach(constructor => Children.Add(new MethodViewModel(constructor, this)));
            TypeModel.Fields?.ForEach(field => Children.Add(new FieldViewModel(field, this)));
            TypeModel.Properties?.ForEach(property => Children.Add(new PropertyViewModel(property, this)));
            TypeModel.Methods?.ForEach(method => Children.Add(new MethodViewModel(method, this)));
            TypeModel.NestedTypes?.ForEach(nestedType => Children.Add(nestedType.CreateTypeViewmodel(this)));
        }

        [Obsolete]
        public TypeViewModel() { }
    }
}