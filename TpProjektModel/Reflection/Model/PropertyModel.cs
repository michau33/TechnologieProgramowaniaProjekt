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
    public class PropertyModel
    {
        [DataMember]
        public string Name { get; private set; }
        [DataMember]
        public AccessLevel PropertyAccessLevel { get; private set; }
        [DataMember]
        public Modifiers Modifier { get; private set; }
        [DataMember]
        public MethodModel Getter { get; private set; }
        [DataMember]
        public MethodModel Setter { get; private set; }

        [DataMember]
        public TypeModel ReturnType { get; private set; }
        
        public List<Attribute> Attributes { get; private set; }

        internal PropertyModel(PropertyInfo p, Dictionary<string, TypeModel> beans)
        {
            Name = p.Name;
            Getter = p.GetGetMethod() == null ? null : new MethodModel(p.GetGetMethod(), beans);
            Setter = p.GetSetMethod() == null ? null : new MethodModel(p.GetSetMethod(), beans);
            Modifier = GetModifier(Getter, Setter, p);
            PropertyAccessLevel = GetPropertyAccessLevel(Getter, Setter);
            ReturnType = Getter.ReturnType;
            Attributes = p.GetAttributes(false).ToList();
        }


        private Modifiers GetModifier(MethodModel getter, MethodModel setter, PropertyInfo p)
        {
            return getter.Modifier != Modifiers.Nothing || setter == null ? getter.Modifier :
                setter.Modifier != Modifiers.Nothing || getter == null ? setter.Modifier :
                GetIfPropIsVirtual(p);
        }

        private AccessLevel GetPropertyAccessLevel(MethodModel getter, MethodModel setter)
        {
            if (setter == null)
            {
                return getter.AccessLevel;
            }

            if (getter == null)
            {
                return setter.AccessLevel;
            }

            return setter.AccessLevel < getter.AccessLevel ? setter.AccessLevel : getter.AccessLevel;
        }

        private Modifiers GetIfPropIsVirtual(PropertyInfo p)
        {
            bool? found = false;

            foreach (MethodInfo method in p.GetAccessors())
            {
                if (found.HasValue)
                {
                    if (found.Value != method.IsVirtual)
                        return Modifiers.Nothing;
                }
                else
                {
                    found = method.IsVirtual;
                }
            }

            return Modifiers.Virtual;
        }
    } 
}
