using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TpProjektModel
{
    [Serializable]
    [DataContract(IsReference = true)]
    public abstract class Person<T>
    {
        [DataMember]
        public List<T> Relations { get; private set; }

        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Surname { get; set; }

        protected Person(string name, string surname, IReadOnlyCollection<T> relations)
        {
            Name = name;
            Surname = surname;
            Relations = new List<T>(relations);
        }

        protected Person(string name, string surname) : this(name, surname, new List<T>())
        {
            Name = name;
            Surname = surname;
        }

        public void AddRelation(T person)
        {
            if (!Relations.Contains(person))
            {
                Relations.Add(person);
            }
        }

        public void AddRelationRange(IReadOnlyCollection<T> people)
        {
            foreach (T person in people)
            {
                AddRelation(person);
            }
        }

        public void RemoveRelatedPerson(T person)
        {
            Relations.Remove(person);
        }

        public void ClearRelations()
        {
            Relations.Clear();
        }

        public override bool Equals(object obj)
        {
            var person = obj as Person<T>;

            return Name == person.Name &&
                   Surname == person.Surname &&
                   Relations.Count == person.Relations.Count;
        }

        public override int GetHashCode()
        {
            var hashCode = 1140632572;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Surname);
            return hashCode;
        }
    }
}
