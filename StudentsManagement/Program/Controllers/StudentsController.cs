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
    public class StudentsController : Controller
    {
        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        /// <param name="userName">Имя пользователя</param>
        /// <param name="email">Адрес электронной почты</param>
        /// <param name="password">Пароль</param>
        /// <param name="role">Роль (chef, manager или customer)</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(string name, int age, string speciality)
        {
            if (name is null || name.Length == 0)
            {
                return new BadRequestObjectResult("Ученику нужно имя.");
            }
            if (age <= 0)
            {
                return new BadRequestObjectResult("Некорректный возраст.");
            }
            Student student = new Student(name, age, speciality);
            if (StudentManagement.CreateStudent(student))
            {
                return new OkObjectResult("Ученик добавлен.");
            }
            return new BadRequestObjectResult("Произошла ошибка, ученик не добавлен.");
        }

        [HttpGet]
        public IActionResult Get()
        {
            return new OkObjectResult(StudentManagement.GetStudents());
        }
    }
}