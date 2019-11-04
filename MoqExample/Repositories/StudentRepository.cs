using System;
using System.Collections.Generic;
using System.Text;
using MoqExample.Entities;
using MoqExample.Repositories.Interfaces;

namespace MoqExample.Repositories
{
    /// <summary>
    /// The repository for student entities
    /// </summary>
    public class StudentRepository : IStudentRepository
    {
        /// <summary>
        /// Retrieves the grades for a given student
        /// </summary>
        /// <param name="studentId">The student's ID</param>
        /// <returns>A list of grades</returns>
        public List<int> GetGrades(string studentId)
        {
            throw new NotImplementedException("Not yet implemented");
        }

        /// <summary>
        /// Saves a student entity
        /// </summary>
        /// <param name="student">The student entity to save</param>
        public void Save(Student student)
        {
            throw new NotImplementedException("Not yet implemented");
        }
    }
}
