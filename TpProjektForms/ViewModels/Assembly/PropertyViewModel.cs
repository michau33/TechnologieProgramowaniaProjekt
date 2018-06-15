using System;
using TpProjektForms.Utility;
using TpProjektForms.ViewModels.TreeView;
using TpProjektModel.Reflection.Enums;
using TpProjektModel.Reflection.Model;

namespace TpProjektForms.ViewModels.Assembly
{
    public class PropertyViewModel : TreeNodeViewModel
    {
        public readonly PropertyModel PropertyModel;

        public PropertyViewModel(PropertyModel propertyModel, TreeNodeViewModel parent)
            : base (propertyModel.Name, parent)
        {
            PropertyModel = propertyModel;

            Name = PropertyModel.PropertyAccessLevel != AccessLevel.None ? $"{PropertyModel.PropertyAccessLevel.ToString().ToLower()} " : String.Empty;
            Name += PropertyModel.Modifier != Modifiers.Nothing ? $"{PropertyModel.Modifier.ToString().ToLower()} " : String.Empty;
            Name += PropertyModel.ReturnType != null ? $"{PropertyModel.ReturnType.Name} " : String.Empty;
            Name += PropertyModel.Name;

            if (PropertyModel?.ReturnType != null || PropertyModel?.Getter != null || PropertyModel?.Setter != null || PropertyModel?.Attributes.Count > 0)
                AddDummyChild();
        }

        protected override void LoadChildren()
        {
            base.LoadChildren();

            if (PropertyModel.Getter != null)
                Children.Add(new MethodViewModel(PropertyModel.Getter, this));

            if (PropertyModel.Setter != null)
                Children.Add(new MethodViewModel(PropertyModel.Setter, this));

            Children.Add(PropertyModel.ReturnType?.CreateTypeViewmodel(this));
            PropertyModel.Attributes?.ForEach(attribute => Children.Add(new AttributeViewModel(attribute, this)));
        }

        [Obsolete]
        public PropertyViewModel() { }
    }
}