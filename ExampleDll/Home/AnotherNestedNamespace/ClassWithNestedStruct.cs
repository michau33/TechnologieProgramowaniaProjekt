namespace ExampleDll.Home.AnotherNestedNamespace
{
    public class ClassWithNestedStruct
    {
        public virtual string StringProperty { get; protected set; }
        private NestedStruct nestedStruct;

        internal struct NestedStruct
        {
            public int I;
            private ClassWithNestedStruct @class;
        }

    }
}
