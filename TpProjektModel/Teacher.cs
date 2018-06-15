using System;
using System.Collections.Generic;

namespace TpProjektModel
{
    [Serializable]
    public class Teacher : Person<Student>
    {
        public Teacher(string name, string surname, IReadOnlyCollection<Student> relations) : base(name, surname, relations)
        {
        }

        public Teacher(string name, string surname) : base(name, surname)
        {
        }
    }
}
