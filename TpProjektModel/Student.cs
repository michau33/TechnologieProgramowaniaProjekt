using System;
using System.Collections.Generic;

namespace TpProjektModel
{
    [Serializable]
    public class Student : Person<Teacher>
    {
        public Student(string name, string surname, IReadOnlyCollection<Teacher> relations) : base(name, surname, relations)
        {
        }

        public Student(string name, string surname) : base(name, surname) 
        {
        }
    }
}
