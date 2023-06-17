using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using StudentsManagement.Models;
using System.Data.SQLite;
using System.Security.Cryptography;
using System.Text;
using System.Data;

namespace StudentsManagement
{
    public static class GradeManagement
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
            catch (SQLiteException ex)
            {
                Console.WriteLine($"Access exception: {ex.Message}");
                return false;
            }
        }

        static GradeManagement()
        {
            if (Connect("db.sqlite"))
            {
                command = new SQLiteCommand(connection);
                command.CommandText = "CREATE TABLE IF NOT EXISTS grades (" +
                                      "studentId INT NOT NULL," +
                                      "subject VARCHAR(255) NOT NULL," +
                                      "mark INT NOT NULL," +
                                      "FOREIGN KEY (studentId) REFERENCES students(id));";
                command.ExecuteNonQuery();
            }
        }

        public static bool CreateGrade(Grade grade)
        {
            command.CommandText = "INSERT INTO grades (studentId, subject, mark)" +
                                  "VALUES (:studentId, :subject, :mark)";
            command.Parameters.AddWithValue("studentId", grade.StudentId);
            command.Parameters.AddWithValue("subject", grade.Subject);
            command.Parameters.AddWithValue("mark", grade.Mark);
            try
            {
                command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static List<Grade> GetGrades(int id)
        {
            command.CommandText = "SELECT * FROM grades WHERE studentId = :studentId";
            command.Parameters.AddWithValue("studentId", id);
            DataTable data = new DataTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
            adapter.Fill(data);
            return (from DataRow row in data.Rows
                    select new Grade(
                        row.Field<int>("studentId"),
                        row.Field<string>("subject"),
                        row.Field<int>("mark")
                        )
                        ).ToList();
        }
    }
}