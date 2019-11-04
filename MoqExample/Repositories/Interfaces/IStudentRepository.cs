using System;
using System.Collections.Generic;
using System.Text;
using MoqExample.Entities;

namespace MoqExample.Repositories.Interfaces
{
    /// <summary>
    /// Interface for the student repository
    /// </summary>
    public interface IStudentRepository
    {
        /// <summary>
        /// Retrieves the grades for a given student
        /// </summary>
        /// <param name="studentId">The student's ID</param>
        /// <returns>A list of grades</returns>
        List<int> GetGrades(string studentId);

        /// <summary>
        /// Saves a student entity
        /// </summary>
        /// <param name="student">The student entity to save</param>
        void Save(Student student);
    }
}
