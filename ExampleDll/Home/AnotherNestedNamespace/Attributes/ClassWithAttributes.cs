using System;

namespace ExampleDll.Home.AnotherNestedNamespace.Attributes
{
    [Serializable]
    public sealed class ClassWithAttributes : ClassWithNestedStruct
    {
        [NonSerialized]
        internal int _nonSerializable;
    }
}
