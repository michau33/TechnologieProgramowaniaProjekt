using System;
using TpProjektForms.Utility;
using TpProjektForms.ViewModels.TreeView;
using TpProjektModel.Reflection.Enums;
using TpProjektModel.Reflection.Model;

namespace TpProjektForms.ViewModels.Assembly
{
    public class FieldViewModel : TreeNodeViewModel
    {
        public readonly FieldModel FieldModel;

        public FieldViewModel(FieldModel fieldModel, TreeNodeViewModel parent)
            : base(fieldModel.Name, parent)
        {
            FieldModel = fieldModel;

            Name = FieldModel.AccessLevel != AccessLevel.None ? $"{FieldModel.AccessLevel.ToString().ToLower()} " : String.Empty;
            Name += FieldModel.FieldType != null ? $"{FieldModel.FieldType.Name} " : String.Empty;
            Name += FieldModel.Name;

            if (FieldModel.FieldType != null || FieldModel.Attributes?.Count > 0)
                AddDummyChild();
        }

        protected override void LoadChildren()
        {
            base.LoadChildren();

            Children.Add(FieldModel.FieldType?.CreateTypeViewmodel(this));
            FieldModel.Attributes?.ForEach(attribute => Children.Add(new AttributeViewModel(attribute, this)));
        }

        [Obsolete]
        public FieldViewModel() { }
    }
}