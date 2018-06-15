using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using TpProjektModel.Reflection.Enums;
using TpProjektModel.Reflection.Extensions;

namespace TpProjektModel.Reflection.Model
{
    [DataContract (IsReference = true)]
    public class TypeModel
    {
        [DataMember]
        public string Name { get; private set; }
        [DataMember]
        public Modifiers Modifier { get; private set; }
        [DataMember]
        public AccessLevel AccessLevel { get; private set; }

        [DataMember]
        public TypeEnum TypeEnum { get; private set; }

        [DataMember]
        public TypeModel BaseType { get; private set; }

        
        public List<Attribute> Attributes { get; private set; }
        [DataMember]
        public List<TypeModel> GenericParameters { get; private set; }
        [DataMember]
        public List<TypeModel> Interfaces { get; private set; }
        [DataMember]
        public List<MethodModel> Constructors { get; private set; }
        [DataMember]
        public List<FieldModel> Fields { get; private set; }
        [DataMember]
        public List<PropertyModel> Properties { get; private set; }
        [DataMember]
        public List<MethodModel> Methods { get; private set; }
        [DataMember]
        public List<TypeModel> NestedTypes { get; private set; }
        //public List<DelegateModel> NestedDelegates { get; private set; }

        internal TypeModel(Type t)
        {
            Name = t.Name;
        }

        internal void AnalyzeType(Type t, Dictionary<string, TypeModel> beans)
        {
            _isAnalyzed = true;
            Modifier = GetModifier(t);
            AccessLevel = GetAccessLevel(t);
            TypeEnum = GetTypeEnum(t);
            BaseType = GetBaseType(t, beans);


            Attributes = ExtractAttributes(t);
            GenericParameters = ExtractGenerics(t, beans);
            Interfaces = ExtractInterfaces(t, beans);
            Constructors = ExtractConstructors(t, beans);
            Fields = ExtractFields(t, beans);
            Properties = ExtractProperties(t, beans);
            Methods = ExtractMethods(t, beans);
            NestedTypes = ExctractNestedTypes(t, beans);
            //NestedDelegates = ExtractNestedDelegates()
        }

        internal static TypeModel GetTypeFromBeans(Type t, Dictionary<string, TypeModel> beans)
        {
            if (!beans.ContainsKey(t.Name))
            {
                beans.Add(t.Name, new TypeModel(t));
            }

            return beans[t.Name];
        }

        internal static TypeModel GetTypeAssemblyFromBeans(Type t, Dictionary<string, TypeModel> beans)
        {
            if (!beans.ContainsKey(t.Name))
            {
                beans.Add(t.Name, new TypeModel(t));
            }
            if (!beans[t.Name]._isAnalyzed)
            {
                beans[t.Name].AnalyzeType(t, beans);
            }
            
            return beans[t.Name];
        }

        #region Details Getters
        private List<MethodModel> ExtractConstructors(Type t, Dictionary<string, TypeModel> beans)
        {
            return t.GetConstructors().Select(c => new MethodModel(c, beans)).ToList();
        }

        private List<TypeModel> ExtractInterfaces(Type t, Dictionary<string, TypeModel> beans)
        {
            return t.GetInterfaces().Select(i => TypeModel.GetTypeFromBeans(i, beans)).ToList();
        }

        private List<TypeModel> ExtractGenerics(Type type, Dictionary<string, TypeModel> beans)
        {
            return type
                .GetGenericArguments()
                .Select(t => TypeModel.GetTypeFromBeans(t, beans))
                .ToList();
        }

        private List<Attribute> ExtractAttributes(Type t)
        {
            return t.GetAttributes(false).ToList();
        }

        private TypeModel GetBaseType(Type t, Dictionary<string, TypeModel> beans)
        {
            Type baseType = t.BaseType;
            bool ifNull = BaseType == null || baseType == typeof(Object) || baseType == typeof(ValueType) || baseType == typeof(Enum);
            
            return ifNull ? null : TypeModel.GetTypeFromBeans(t, beans); 
        }

        private List<TypeModel> ExctractNestedTypes(Type t, Dictionary<string, TypeModel> beans)
        {
            var tmp = t
                .GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic);
            return tmp
                .Select(nT => TypeModel.GetTypeAssemblyFromBeans(nT, beans))
                .ToList();
        }

        private List<MethodModel> ExtractMethods(Type t, Dictionary<string, TypeModel> beans)
        {
            
            MethodInfo[] tmp = t.GetMethods(BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
            return tmp.Select(m => new MethodModel(m, beans)).ToList();
        }

        private List<PropertyModel> ExtractProperties(Type t, Dictionary<string, TypeModel> beans)
        {
            return t.GetProperties(BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance)
                .Select(p => new PropertyModel(p, beans))
                .ToList();
        }

        private List<FieldModel> ExtractFields(Type t, Dictionary<string, TypeModel> beans)
        {
            return t.GetFields(BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance)
                .Select(f => new FieldModel(f, beans))
                .ToList();
        }

        private TypeEnum GetTypeEnum(Type t)
        {
            return t.IsEnum ? TypeEnum.Enum :
                t.IsValueType ? TypeEnum.Struct :
                t.IsInterface ? TypeEnum.Interface :
                TypeEnum.Class;
        }

        private Modifiers GetModifier(Type t)
        {
            return t.IsSealed && !t.IsAbstract ? Modifiers.Sealed :
                t.IsAbstract && !t.IsSealed ? Modifiers.Abstract :
                t.IsAbstract && t.IsSealed ? Modifiers.Static :
                Modifiers.Nothing;
        }

        private AccessLevel GetAccessLevel(Type t)
        {
            return t.IsPublic || t.IsNestedPublic ? AccessLevel.Public :
                t.IsNestedFamily ? AccessLevel.Protected :
                t.IsNestedFamANDAssem ? AccessLevel.Internal :
                AccessLevel.Internal;
        }
        
        private bool _isAnalyzed = false;
        #endregion
    }
}