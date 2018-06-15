using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using TpProjektModel.Reflection.Enums;
using TpProjektModel.Reflection.Extensions;

namespace TpProjektModel.Reflection.Model
{
    [DataContract(IsReference = true)]
    public class FieldModel
    {
        [DataMember]
        public string Name { get; private set; }
        [DataMember]
        public AccessLevel AccessLevel { get; private set; }
        [DataMember]
        public TypeModel FieldType { get; private set; }

        
        public List<Attribute> Attributes { get; private set; }

        internal FieldModel(FieldInfo f, Dictionary<string, TypeModel> beans)
        {
            Name = f.Name;

            AccessLevel = GetAccessLevel(f);
            FieldType = TypeModel.GetTypeFromBeans(f.FieldType, beans);
            Attributes = f.GetAttributes(false).ToList();
        }

        private AccessLevel GetAccessLevel(FieldInfo f)
        {
            return f.IsPublic ? AccessLevel.Public :
                f.IsAssembly ? AccessLevel.Internal :
                f.IsFamily ? AccessLevel.Protected :
                AccessLevel.Private;
        }
    }
}