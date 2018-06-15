using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using TpProjektModel.Reflection.Enums;
using TpProjektModel.Reflection.Extensions;

namespace TpProjektModel.Reflection.Model
{
    [DataContract(IsReference = true)]
    public class MethodModel
    {
        [DataMember]
        public string Name { get; private set; }
        [DataMember]
        public Modifiers Modifier { get; private set; }
        [DataMember]
        public AccessLevel AccessLevel { get; private set; }

        [DataMember]
        public bool IsExtension { get; private set; }

        [DataMember]
        public TypeModel ReturnType { get; private set; }

        [DataMember]
        public List<TypeModel> GenericParameters { get; private set; }
        [DataMember]
        public List<ParameterModel> Parameters { get; private set; }
         //TODO change Attributes
        public List<Attribute> Attributes { get; private set; }

        internal MethodModel(MethodBase m, Dictionary<string, TypeModel> beans)
        {
            Name = m.Name;
            Modifier = GetModifier(m);
            AccessLevel = GetAccessLevel(m);
            IsExtension = GetExtensionInfo(m);

            ReturnType = GetReturnType(m, beans);

            GenericParameters = GetGenericParameters(m, beans);
            Parameters = GetParameters(m, beans);
            Attributes = GetAttributes(m);
        }

        public static AccessLevel GetAccessLevel(MethodBase m)
        {
            return m.IsPublic ? AccessLevel.Public :
                m.IsFamily ? AccessLevel.Protected :
                m.IsAssembly ? AccessLevel.Internal :
                AccessLevel.Private;
        }

        #region Detail Getters
        private List<Attribute> GetAttributes(MethodBase m)
        {
            return m.GetAttributes(false).ToList();
        }

        private List<ParameterModel> GetParameters(MethodBase m, Dictionary<string, TypeModel> beans)
        {
            return m.GetParameters().Select(p => new ParameterModel(p.Name, p.ParameterType, beans)).ToList();
        }

        private List<TypeModel> GetGenericParameters(MethodBase m, Dictionary<string, TypeModel> beans)
        {
            return m.IsGenericMethodDefinition ? 
                m.GetGenericArguments().Select(t => TypeModel.GetTypeFromBeans(t, beans))
                .ToList() : 
                null;
        }

        private TypeModel GetReturnType(MethodBase m, Dictionary<string, TypeModel> beans)
        {
            MethodInfo methodInfo = m as MethodInfo;

            if (methodInfo == null)
            {
                return null;
            }

            Type returnType = methodInfo.ReturnType;
            return TypeModel.GetTypeFromBeans(returnType, beans);
        }

        private bool GetExtensionInfo(MethodBase m)
        {
            return m.IsDefined(typeof(ExtensionAttribute), true);
        }

        private Modifiers GetModifier(MethodBase m)
        {
            return m.IsAbstract ? Modifiers.Abstract :
                m.IsVirtual ? Modifiers.Virtual :
                m.IsStatic ? Modifiers.Static :
               Modifiers.Nothing;
        }
        #endregion
    }
}
