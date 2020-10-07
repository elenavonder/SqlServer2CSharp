using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Metadata.Ecma335;

namespace SQLLibrary
{
    public class SqlServer
    {
        public SqlConnection connection = null;
        public List<Class> SelectAllClasses(string sql)
        {
            var cmd = new SqlCommand(sql, connection);
            var Class = new List<Class>();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var cla = new Class();
                cla.Id = Convert.ToInt32(reader["Id"]);
                cla.Subject = Convert.ToString(reader["Subject"]);
                cla.Section = Convert.ToInt32(reader["Section"]);
                cla.InstructorId = reader.IsDBNull("InstructorId")
                    ? (int?)null
                    : Convert.ToInt32(reader["InstructorId"]);
            }
            reader.Close();
            return Class;
        }

        public List<Instructor> SelectAllInstructor(string sql)
        {
            var mdc = new SqlCommand(sql, connection);
            var Instructors = new List<Instructor>();
            var reader = mdc.ExecuteReader();
            while (reader.Read())
            {
                var ins = new Instructor();
                ins.Id = Convert.ToInt32(reader["Id"]);
                ins.Firstname = Convert.ToString(reader["Firstname"]);
                ins.Lastname = Convert.ToString(reader["Lastname"]);
                ins.YearsExperience = Convert.ToInt32(reader["YearsExpereince"]);
                ins.IsTenured = Convert.ToInt32(reader["IsTenured"]);
            }
            reader.Close();
            return Instructors;
        }

        public List<Major> SelectAllMajors(string sql)
        {
            var wat = new SqlCommand(sql, connection);
            var Majors = new List<Major>();
            var reader = wat.ExecuteReader();
            while (reader.Read())
            {
                var maj = new Major();
                maj.Id = Convert.ToInt32(reader["Id"]);
                maj.Description = Convert.ToString(reader["Description"]);
                maj.MinSAT = Convert.ToInt32(reader["MinSAT"]);
                Majors.Add(maj);
            }
            reader.Close();
            return Majors;
        }

        public List<Student> SelectAllStudents(string sql)
        {
            var cmd = new SqlCommand(sql, connection);//allows you to do sql statement
            var students = new List<Student>();
            var reader = cmd.ExecuteReader();//going through each row
            while (reader.Read())//starts by pointing before any row and ends past last row
            {
                var stud = new Student();

                stud.Id = Convert.ToInt32(reader["Id"]);
                stud.Firstname = Convert.ToString(reader["firstname"]);
                stud.Lastname = Convert.ToString(reader["lastname"]);
                stud.SAT = reader.IsDBNull("SAT") 
                    ? (int?) null 
                    : Convert.ToInt32(reader["SAT"]);
                stud.GPA = Convert.ToDecimal(reader["GPA"]);
                stud.MajorId = reader.IsDBNull("MajorId")
                    ? (int?)null
                    : Convert.ToInt32(reader["MajorId"]);
                students.Add(stud);
            }
            reader.Close();
            return students;
        }

        //mehod to see if connection went through
        public bool Connect(string server, string instance, string database)
        {
            var connectionString = $"server={server}\\{instance};database={database};trusted_connection=true;";
            connection = new SqlConnection(connectionString);
            connection.Open();
            if(connection.State != System.Data.ConnectionState.Open)
            {
                throw new Exception("Cannot connect to Sql");
            }
            return true;
        }

        public void Disconnect()
        {
            if(connection == null)
            {
                return;
            }
            connection.Close();
            connection = null;
        }
    }
}
