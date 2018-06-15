using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TpProjektModel.Reflection.Model
{
    [DataContract(IsReference = true)]
    public class ParameterModel
    {
        [DataMember]
        public string Name { get; private set; }
        [DataMember]
        public TypeModel Type { get; private set; }

        internal ParameterModel(string name, Type type, Dictionary<string, TypeModel> beans)
        {
            Name = name;
            
            if (!beans.ContainsKey(type.Name))
            {
                beans.Add(type.Name, new TypeModel(type));
            }
            Type = beans[type.Name];
        }
    }
}
