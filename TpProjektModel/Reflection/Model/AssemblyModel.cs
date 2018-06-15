using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace TpProjektModel.Reflection.Model
{
    [DataContract]
    public class AssemblyModel
    {
        [DataMember]
        public string Name { get; private set; }
        [DataMember]
        public List<NamespaceModel> Namespaces { get; private set; } = new List<NamespaceModel>();

        internal AssemblyModel(Assembly assembly, Dictionary<string, TypeModel> beans)
        {
            Name = assembly.ManifestModule.Name;
            var tmp = assembly.GetTypes();
            List<string> namespaces = tmp
                .Select(t => t.Namespace)
                .Distinct()
                .ToList();

            List<string> nestedNamespaces = namespaces
                .Where(n => namespaces.Any(anyN => n.Contains(anyN) && anyN != n))
                .ToList();

            namespaces = namespaces
               .Where(n => !nestedNamespaces.Contains(n))
               .ToList();

            foreach (string ns in namespaces)
            {
                Namespaces.Add(new NamespaceModel(ns, nestedNamespaces, beans, assembly));
            }
        }
    }
}
