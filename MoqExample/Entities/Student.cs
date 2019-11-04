using MoqExample.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MoqExample.Entities
{
    public class Student
    {
        private IStudentRepository studentRepository;

        private string id;
        private string surname;
        private string givenName;
        private List<(bool isNew, int grade)> grades;

        string Id { get { return id; } }

        string Surname
        {
            get { return surname; }
            set { surname = value; }
        }

        string GivenName
        {
            get { return givenName; }
            set { givenName = value; }
        }

        public ReadOnlyCollection<int> Grades
        {
            get
            {
                GetGrades();

                return grades.Select(x => x.grade).ToList().AsReadOnly();
            }
        }

        public double GradeAverage
        {
            get { return grades.Select(x => x.grade).ToList().Average(); }
        }

        public Student(IStudentRepository _studentRepository, string _id, string _surname, string _givenName)
        {
            studentRepository = _studentRepository;
            id = _id;
            surname = _surname;
            givenName = _givenName;
        }

        public bool HasUnsavedGrades()
        {
            GetGrades();

            return grades.Any(x => x.isNew);
        }

        public void AddGrade(int grade)
        {
            GetGrades();

            if (grade >= 0 && grade <= 100)
                grades.Add((true, grade));
        }

        public void Save()
        {
            studentRepository.Save(this);
            grades = grades.Select(x => (false, x.grade)).ToList();
        }

        private void GetGrades()
        {
            if (grades == null)
                grades = studentRepository.GetGrades(id).Select(x => (false, x)).ToList();
        }
    }
}
