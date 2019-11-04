using MoqExample.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MoqExample.Entities
{
    /// <summary>
    /// The student entity
    /// </summary>
    public class Student
    {
        private IStudentRepository studentRepository;

        private string id;
        private string surname;
        private string givenName;
        private List<(bool isNew, int grade)> grades;

        /// <summary>
        /// The student's system ID
        /// </summary>
        string Id { get { return id; } }
        
        /// <summary>
        /// The student's last name
        /// </summary>
        string Surname
        {
            get { return surname; }
            set { surname = value; }
        }

        /// <summary>
        /// The student's first name
        /// </summary>
        string GivenName
        {
            get { return givenName; }
            set { givenName = value; }
        }

        /// <summary>
        /// The student's grades
        /// </summary>
        public ReadOnlyCollection<int> Grades
        {
            get
            {
                GetGrades();

                return grades.Select(x => x.grade).ToList().AsReadOnly();
            }
        }
        
        /// <summary>
        /// The student's GPA
        /// </summary>
        public double GradeAverage
        {
            get { return grades.Select(x => x.grade).ToList().Average(); }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_studentRepository">The repository used for data system interaction</param>
        /// <param name="_id">The student's system ID</param>
        /// <param name="_surname">The student's last name</param>
        /// <param name="_givenName">The student's first name</param>
        public Student(IStudentRepository _studentRepository, string _id, string _surname, string _givenName)
        {
            studentRepository = _studentRepository;
            id = _id;
            surname = _surname;
            givenName = _givenName;
        }

        /// <summary>
        /// Whether or not the student has grades not yet saved to the data system
        /// </summary>
        /// <returns>Whether or not unsaved grades are present</returns>
        public bool HasUnsavedGrades()
        {
            GetGrades();

            return grades.Any(x => x.isNew);
        }

        /// <summary>
        /// Attempts to add a new grade to the student
        /// </summary>
        /// <param name="grade">the grade to add</param>
        public void AddGrade(int grade)
        {
            GetGrades();

            if (grade >= 0 && grade <= 100)
                grades.Add((true, grade));
        }

        /// <summary>
        /// Saves all changes made to the data system
        /// </summary>
        public void Save()
        {
            studentRepository.Save(this);
            grades = grades.Select(x => (false, x.grade)).ToList();
        }

        /// <summary>
        /// Gets the student's grades from the data system
        /// </summary>
        private void GetGrades()
        {
            if (grades == null)
                grades = studentRepository.GetGrades(id).Select(x => (false, x)).ToList();
        }
    }
}
