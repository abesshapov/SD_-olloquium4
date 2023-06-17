using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentsManagement.Models;
using System.Text.RegularExpressions;

namespace StudentsManagement.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class GradesController : Controller
    {
        [HttpPost]
        public IActionResult Post(int studentId, string subject, int mark)
        {
            if (subject is null || subject.Length == 0)
            {
                return new BadRequestObjectResult("Некорректное название дисциплины.");
            }
            if (mark < 0)
            {
                return new BadRequestObjectResult("Некорректная оценка.");
            }
            Grade grade = new Grade(studentId, subject, mark);
            if (GradeManagement.CreateGrade(grade))
            {
                return new OkObjectResult("Оценка поставлена.");
            }
            return new BadRequestObjectResult("Произошла ошибка, оценка не проставлена.");
        }

        [HttpGet("{studentId}")]
        public IActionResult Get(int studentId)
        {
            return new OkObjectResult(GradeManagement.GetGrades(studentId));
        }
    }
}