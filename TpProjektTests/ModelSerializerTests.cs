using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TpProjektModel;
using TpProjektModel.Serialization;

namespace TpProjektTests
{
    [TestClass]
    public class ModelSerializerTests
    {
        [TestMethod]
        public void TestIfSerializationWorksForTeacher()
        {
            string filePath = @"ser.xml";
            ModelSerializer modelSerializer = new ModelSerializer();
            Teacher teacher = new Teacher("Grzegorz", "Grzegożewski");
            modelSerializer.Write(teacher, filePath);
            Teacher deserializedTeacher = modelSerializer.Read<Teacher>(filePath);

            Assert.AreEqual(teacher, deserializedTeacher);
        }

        [TestMethod]
        public void TestIfSerializationWorksForStudent()
        {
            string filePath = @"ser.xml";
            ModelSerializer modelSerializer = new ModelSerializer();
            Student student = new Student("Grzegorz", "Grzegożewski");
            modelSerializer.Write(student, filePath);
            Student deserializedStudent = modelSerializer.Read<Student>(filePath);

            Assert.AreEqual(student, deserializedStudent);
        }
    }
}
