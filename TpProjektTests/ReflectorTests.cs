using Microsoft.VisualStudio.TestTools.UnitTesting;
using TpProjektModel.Reflection;
using TpProjektModel.Reflection.Enums;
using TpProjektModel.Reflection.Model;

namespace TpProjektTests
{
    [TestClass]
    public class ReflectorTests
    {
        [TestMethod]
        public void TestIfReflectorReturnsAppropriateNamespace()
        {
            string path = @"..\..\..\ExampleDll\obj\Debug\netstandard2.0\ExampleDll.dll";
            Reflector reflector = new Reflector();
            reflector.GetAssemblyModel(path);
            Assert.AreEqual(17, reflector.Beans.Count);
            Assert.AreEqual(1, reflector.AssemblyModel.Namespaces.Count);
            NamespaceModel testedNamespace = reflector.AssemblyModel.Namespaces.Find(n => n.Name == "ExampleDll.Home");
            Assert.AreEqual(3, testedNamespace.Namespaces.Count);
            
            NamespaceModel testedNamespace2 = testedNamespace
                .Namespaces
                .Find(n => n.Name == "ExampleDll.Home.NestedNamespace")
                .Namespaces
                .Find(n => n.Name == "ExampleDll.Home.NestedNamespace.InnerNestedNamespace");

            TypeModel testedClass = testedNamespace2
                .Types
                .Find(t => t.Name == "ClassImplementingInterfaces");
            Assert.AreEqual(2, testedClass.Interfaces.Count);
        }

        [TestMethod]
        public void TestIfGenericTypesAreReflectedRight()
        {
            string path = @"..\..\..\ExampleDll\obj\Debug\netstandard2.0\ExampleDll.dll";
            Reflector reflector = new Reflector();
            reflector.GetAssemblyModel(path);
            Assert.AreEqual(2, reflector.Beans["GenericClass`2"].GenericParameters.Count);
        }

        [TestMethod]
        public void TestIfGenericethodsAreReflectedRight()
        {
            string path = @"..\..\..\ExampleDll\obj\Debug\netstandard2.0\ExampleDll.dll";
            Reflector reflector = new Reflector();
            reflector.GetAssemblyModel(path);
            MethodModel method = reflector.Beans["GenericClass`2"].Methods.Find(m => m.Name == "GenericMethod");

            Assert.AreEqual(1, method.GenericParameters.Count);
            Assert.AreEqual(AccessLevel.Protected, method.AccessLevel);
        }

        [TestMethod]
        public void CheckIfAbstractClassIsRight()
        {
            string path = @"..\..\..\ExampleDll\obj\Debug\netstandard2.0\ExampleDll.dll";
            Reflector reflector = new Reflector();
            reflector.GetAssemblyModel(path);
            TypeModel abstractClass = reflector.Beans["AbstractClass"];

            Assert.AreEqual(Modifiers.Abstract, abstractClass.Modifier);
            Assert.AreEqual(AccessLevel.Public, abstractClass.AccessLevel);

            MethodModel method = abstractClass.Methods.Find(m => m.Name == "AbstractMethod");
            Assert.AreEqual(Modifiers.Abstract, method.Modifier);
            Assert.AreEqual(AccessLevel.Public, method.AccessLevel);
        }

        [TestMethod]
        public void CheckIfStaticClassIsRight()
        {
            string path = @"..\..\..\ExampleDll\obj\Debug\netstandard2.0\ExampleDll.dll";
            Reflector reflector = new Reflector();
            reflector.GetAssemblyModel(path);
            TypeModel staticClass = reflector.Beans["StaticClass"];

            Assert.AreEqual(Modifiers.Static, staticClass.Modifier);
            Assert.AreEqual(2, staticClass.Methods.Count);
            Assert.AreEqual(TypeEnum.Class, staticClass.TypeEnum);

            MethodModel methodStatic = staticClass.Methods.Find(m => m.Name == "StaticMethod");
            Assert.AreEqual(Modifiers.Static, methodStatic.Modifier);

            MethodModel extensionMethod = staticClass.Methods.Find(m => m.Name == "ExtensionMethod");
            Assert.AreEqual(true, extensionMethod.IsExtension);
        }


    }
}
