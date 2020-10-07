using System;
using SQLLibrary;

namespace SqlServer2CSharp
{
    class Program
    {
        static void Main(string[] args)
        {

            var ss = new SqlServer();
            var ok = ss.Connect("localhost", "sqlexpress", "EdDb");

            var students = ss.SelectAllStudents("SELECT * From Student;");
            foreach(var stud in students)
            {

                Console.WriteLine(stud);
            }
            ss.Disconnect();
        }
    }
}
