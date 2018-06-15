using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TpProjektModel.Reflection;
using TpProjektModel.Serialization;
using TpProjektModel.Interfaces;

namespace TpProjektTests
{
    [TestClass]
    public class ReflectorSerializationTest
    {
        private const string AssemblyPath = @"..\..\..\ExampleDll\obj\Debug\netstandard2.0\ExampleDll.dll";
        private const string SerializedFile = @"..\..\ser.xml";

        [TestMethod]
        public void SerializationTest()
        {
            Reflector reflector = new Reflector();
            reflector.GetAssemblyModel(AssemblyPath);
            IWriter serializer = new ModelSerializer();
            serializer.Write(reflector, SerializedFile);

            IReader deserializer = new ModelSerializer();
            Reflector deserializedReflector = deserializer.Read<Reflector>(SerializedFile);

            Assert.AreEqual(reflector.Beans.Count, deserializedReflector.Beans.Count);

            Assert.AreEqual(reflector.Beans["Object"], reflector.Beans["Object"]);

            Assert.AreEqual(reflector.AssemblyModel.Namespaces.Count, reflector.AssemblyModel.Namespaces.Count);
        }
    }
}
