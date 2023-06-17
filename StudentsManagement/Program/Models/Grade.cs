using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace StudentsManagement.Models
{
    /// <summary>
    /// Класс, описывающий оценку.
    /// </summary>
    [DataContract]
    public class Grade
    {
        /// <summary>
        /// Id студента.
        /// </summary>
        [DataMember, Required]
        public int StudentId { get; set; }

        /// <summary>
        /// Название предмета.
        /// </summary>
        [DataMember, Required]
        public string Subject { get; set; }

        /// <summary>
        /// Оценка по предмету.
        /// </summary>
        [DataMember, Required]
        public int Mark { get; set; }

        public Grade(int studentId, string subject, int mark) {
            StudentId = studentId;
            Subject = subject;
            Mark = mark;
        }
    }
}