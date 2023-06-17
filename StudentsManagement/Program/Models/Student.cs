using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace StudentsManagement.Models
{
    /// <summary>
    /// Класс, описывающий студента.
    /// </summary>
    [DataContract]
    public class Student
    {
        /// <summary>
        /// Имя студента.
        /// </summary>
        [DataMember, Required]
        public string Name { get; set; }

        /// <summary>
        /// Возраст студента.
        /// </summary>
        [DataMember, Required]
        public int Age { get; set; }

        /// <summary>
        /// Специальность студента.
        /// </summary>
        [DataMember, Required]
        public string Speciality { get; set; }

        public Student(string name, int age, string speciality) {
            Name = name;
            Age = age;
            Speciality = speciality;
        }
    }
}