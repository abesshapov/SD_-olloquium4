using System;
using System.Collections.Generic;
using System.Linq;
using StudentsManagement.Models;
using System.Data.SQLite;
using System.Data;

namespace StudentsManagement
{
    public static class StudentManagement
    {
        private static SQLiteConnection connection;
        private static SQLiteCommand command;
        private static bool Connect(string fileName)
        {
            try
            {
                connection = new SQLiteConnection("Data Source=" + fileName + ";Version=3; FailIfMissing=False");
                connection.Open();
                return true;
            }
            catch (SQLiteException e)
            {
                return false;
            }
        }

        static StudentManagement()
        {
            if (Connect("db.sqlite"))
            {
                command = new SQLiteCommand(connection);
                command.CommandText = "CREATE TABLE IF NOT EXISTS students (" +
                                    "id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE," +
                                    "name VARCHAR(255) NOT NULL, " +
                                    "age INT NOT NULL," +
                                    "speciality VARCHAR(255) NOT NULL);";
                command.ExecuteNonQuery();
            }
        }

        public static bool CreateStudent(Student student)
        {
            command.CommandText = "INSERT INTO students (name, age, speciality)" +
                                  "VALUES (:name, :age, :speciality)";
            command.Parameters.AddWithValue("name", student.Name);
            command.Parameters.AddWithValue("age", student.Age);
            command.Parameters.AddWithValue("speciality", student.Speciality);
            try
            {
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static List<Student> GetStudents()
        {
            command.CommandText = "SELECT * FROM students";
            DataTable data = new DataTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
            adapter.Fill(data);
            return (from DataRow row in data.Rows
                    select new Student(
                        row.Field<string>("name"),
                        row.Field<int>("age"),
                        row.Field<string>("speciality")
                        )
                        ).ToList();
        }
    }
}
