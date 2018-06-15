using System;

namespace ExampleDll.Home.NestedNamespace.InnerNestedNamespace
{
    public class GenericClass<T, A>
    {
        T genericType;

        protected A GenericMethod<C>(C c)
        {
            throw new NotImplementedException();
        }

        public int NonGenericMethod(T t)
        {
            throw new NotImplementedException();
        }
    }
}
