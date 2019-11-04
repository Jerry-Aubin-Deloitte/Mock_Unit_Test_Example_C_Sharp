using NUnit.Framework;
using Moq;
using MoqExample.Entities;
using MoqExample.Repositories.Interfaces;
using System.Collections.Generic;

namespace Tests
{
    /// <summary>
    /// Test class for the student entity
    /// </summary>
    public class TStudent
    {
        /// <summary>
        /// The mock repository for the student
        /// </summary>
        private Mock<IStudentRepository> mockRepository;

        /// <summary>
        /// Runs one time before all the tests
        /// </summary>
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            // Instantiate the mock object
            mockRepository = new Mock<IStudentRepository>(MockBehavior.Strict);
            
            // Set up the methods being mocked by the object
            mockRepository.Setup(x => x.GetGrades(It.IsAny<string>())).Returns(GetMockGrades());
            mockRepository.Setup(x => x.Save(It.IsAny<Student>()));
        }

        /// <summary>
        /// Runs one time after all tests have been run
        /// </summary>
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            // Destroy the objects created in setup
            mockRepository = null;
        }

        /// <summary>
        /// Makes sure a valid grade is added to the list
        /// </summary>
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

        /// <summary>
        /// Makes sure an invalid grade is not added to the list
        /// </summary>
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

        /// <summary>
        /// Makes sure the entity says there are unsaved grades when a valid grade is added
        /// </summary>
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

        /// <summary>
        /// Makes sure the entity says there are no unsaved grades when the entity is saved
        /// </summary>
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

        /// <summary>
        /// Creates a list of mock grades for testing purposes
        /// </summary>
        private List<int> GetMockGrades()
        {
            return new List<int>()
            {
                100, 0
            };
        }
    }
}