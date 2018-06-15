using ExampleDll.Home.AnotherNestedNamespace.Attributes;
using ExampleDll.Home.Interfaces;

namespace ExampleDll.Home.NestedNamespace
{
    public abstract class AbstractClass
    {
        public abstract IInterfaceable AbstractMethod(int a, ClassWithAttributes classWith);
    }
}
