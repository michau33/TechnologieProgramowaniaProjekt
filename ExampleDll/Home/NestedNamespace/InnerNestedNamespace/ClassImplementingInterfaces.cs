using ExampleDll.Home.Interfaces;
using System;

namespace ExampleDll.Home.NestedNamespace.InnerNestedNamespace
{
    public class ClassImplementingInterfaces : IInterfaceable, IComparable
    {
        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        public void MetodicMethod(int i, string a)
        {
            throw new NotImplementedException();
        }
    }
}
