using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace SQLLibrary
{
    public class SqlServer
    {
        public SqlConnection connection = null;

        public List<Student> ExecuteQuery(string sql)
        {
            var cmd = new SqlCommand(sql, connection);//allows you to do sql statement
            var students = new List<Student>();
            var reader = cmd.ExecuteReader();//
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
