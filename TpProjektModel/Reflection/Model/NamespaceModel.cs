using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace TpProjektModel.Reflection.Model
{
    [DataContract(IsReference = true)]
    public class NamespaceModel
    {
        [DataMember]
        public string Name { get; private set; }
        [DataMember]
        public List<NamespaceModel> Namespaces { get; private set; } = new List<NamespaceModel>();
        [DataMember]
        public List<TypeModel> Types { get; private set; }

        internal NamespaceModel(string name, IEnumerable<string> nestedNamespaces, Dictionary<string, TypeModel> beans, Assembly assembly)
        {
            Name = name;
            IEnumerable<string> specificNestedStrings = nestedNamespaces
                .Where(n => n.Contains(Name));
            IEnumerable<string> currentNestedNamespaces = specificNestedStrings
                .Where(n =>
                    n.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).Length
                    - name.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).Length
                    == 1);

            Types = assembly.GetTypes()
                .Where(t => t.Namespace == Name && !t.IsNested)
                .Select(t =>
                {
                    if (!beans.ContainsKey(t.Name))
                    {
                        beans.Add(t.Name, new TypeModel(t));
                    }
                    beans[t.Name].AnalyzeType(t, beans);
                    return beans[t.Name];
                })
                .ToList();

            Namespaces = currentNestedNamespaces
                .Select(n => new NamespaceModel(n, specificNestedStrings, beans, assembly))
                .ToList();
        }

    }
}
