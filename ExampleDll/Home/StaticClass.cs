using System;

namespace ExampleDll.Home
{
    public static class StaticClass
    {
        public static double StaticMethod()
        {
            throw new NotImplementedException();
        }

        public static int ExtensionMethod(this int a)
        {
            throw new NotImplementedException();
        }
    }
}
