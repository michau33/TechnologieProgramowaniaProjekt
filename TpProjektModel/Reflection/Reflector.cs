using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using TpProjektModel.Reflection.Model;

namespace TpProjektModel.Reflection
{
    [DataContract]
    public class Reflector : IReflector
    {
        [DataMember]
        public Dictionary<string, TypeModel> Beans { get; private set; } = new Dictionary<string, TypeModel>();

        [DataMember]
        public AssemblyModel AssemblyModel { get; private set; }


        public void Reflect(string path)
        {
            Assembly assembly = Assembly.LoadFile(Path.GetFullPath(path));
            AssemblyModel = new AssemblyModel(assembly, Beans);
        }
    }
}
