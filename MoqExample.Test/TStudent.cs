using NUnit.Framework;
using Moq;
using MoqExample.Entities;
using MoqExample.Repositories.Interfaces;
using System.Collections.Generic;

namespace Tests
{
    public class TStudent
    {
        private Mock<IStudentRepository> mockRepository;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            mockRepository = new Mock<IStudentRepository>(MockBehavior.Strict);
            mockRepository.Setup(x => x.GetGrades(It.IsAny<string>())).Returns(GetMockGrades());
            mockRepository.Setup(x => x.Save(It.IsAny<Student>()));
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            mockRepository = null;
        }

        [Test]
        public void Student_AddGrade_AddAValidGrade_Returns_OneMoreGradeInList()
        {
            // 1) Arrange
            var student = new Student(mockRepository.Object, "ABC123", "Smith", "John");
            int originalNumGrades = student.Grades.Count;

            // 2) Act
            student.AddGrade(100);

            // 3) Assert
            Assert.AreEqual(originalNumGrades + 1, student.Grades.Count);
        }

        [Test]
        public void Student_AddGrade_AddAnInvalidGrade_Returns_SameNumberOfGrades()
        {
            // 1) Arrange
            var student = new Student(mockRepository.Object, "ABC123", "Smith", "John");
            int originalNumGrades = student.Grades.Count;

            // 2) Act
            student.AddGrade(1000);

            // 3) Assert
            Assert.AreEqual(originalNumGrades, student.Grades.Count);
        }

        [Test]
        public void Student_AddGrade_AddAGrade_Returns_HasUnsavedGrades()
        {
            // 1) Arrange
            var student = new Student(mockRepository.Object, "ABC123", "Smith", "John");

            // 2) Act
            student.AddGrade(100);

            // 3) Assert
            Assert.IsTrue(student.HasUnsavedGrades());
        }

        [Test]
        public void Student_Save_SaveStudent_Returns_NoUnsavedGrades()
        {
            // 1) Arrange
            var student = new Student(mockRepository.Object, "ABC123", "Smith", "John");
            student.AddGrade(100);

            // 2) Act
            student.Save();

            // 3) Assert
            Assert.IsFalse(student.HasUnsavedGrades());
        }

        private List<int> GetMockGrades()
        {
            return new List<int>()
            {
                100, 0
            };
        }
    }
}