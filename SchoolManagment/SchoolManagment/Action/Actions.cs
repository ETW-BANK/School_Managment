using Azure;
using Microsoft.Data.SqlClient;
using SchoolManagment.DBconn;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagment.Action
{
    internal class Actions
    {
       

        public static void GetStudents(SqlConnection conn)

        {


            using (SqlCommand cmd = new SqlCommand())
            {


                cmd.CommandText = "select * from Student";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    Console.WriteLine("\t\t\t{0}\t{1}\t{2}\t{3}\t\t{4}", "Student ID",  "First Name",   "Last Name",   "Gender",    "Age");
                    Console.WriteLine("\t\t\t{0}\t{1}\t{2}\t{3}\t\t{4}", "==========", "============", "===========","==========", "====");


                    while (reader.Read())
                    {
                        
                        Console.WriteLine("\t\t\t{0}\t\t{1}\t\t{2}\t\t{3}\t\t{4}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4));
                    }
                }
                else
                {
                    Console.WriteLine("No rows");
                }
                reader.Close();
            }
        }
        public static void AddStudent( SqlConnection conn)

        {
            {
                using (SqlCommand cmd =new SqlCommand())
                {


                    // Insert data into the Students table
                Console.WriteLine("\n\t\t\tInserting data into the Students table.");
                Console.WriteLine("\t\t\t=========================================\n\n");
                Console.WriteLine("Enter First Name");
                string Fname = Console.ReadLine().ToUpper();
                Console.WriteLine("Enter Last Name");
                string Lname = Console.ReadLine().ToUpper();
                Console.WriteLine("Enter Student Gender");
                string gender = Console.ReadLine().ToUpper();
                Console.WriteLine("Enter Course Name");
                string course = Console.ReadLine().ToUpper();
                Console.WriteLine("Enter Age");
                int age = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter Class Name");
               string clasname = Console.ReadLine().ToUpper();

               cmd.CommandType = CommandType.Text;
               cmd.Connection = conn;   
              
                cmd.CommandText = "insert into Student(FirstName,LastName,Gender,Age)OUTPUT INSERTED.Student_ID values(@Fname,@Lname,@gender,@age)";
                cmd.Parameters.AddWithValue("@Fname", Fname);
                cmd.Parameters.AddWithValue("@Lname", Lname);
                cmd.Parameters.AddWithValue("@gender", gender);
                cmd.Parameters.AddWithValue("@age", age);

                cmd.CommandText = "insert into Course(CourseTitel)OUTPUT INSERTED.Course_ID values(@course)";
                cmd.Parameters.AddWithValue("@course", course);

               cmd.CommandText = "insert into Class(ClassName)OUTPUT INSERTED.Class_ID values(@classname)";
               cmd.Parameters.AddWithValue("@classname", clasname);

                int studentId = Convert.ToInt32(cmd.ExecuteScalar());
                int courseId = Convert.ToInt32(cmd.ExecuteScalar());
                int classId=Convert.ToInt32(cmd.ExecuteScalar());



                cmd.CommandText = "insert into Relation(StudentID,CourseID,ClassID) values(@studentId,@courseId,@classId)";
                cmd.Parameters.Clear(); // Clear previous parameters
                cmd.Parameters.AddWithValue("@studentId", studentId);
                cmd.Parameters.AddWithValue("@courseId", courseId);
                cmd.Parameters.AddWithValue("@classId", classId);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("\t\t\tData inserted into the Students table.\n");
                }
                else
                {
                    Console.WriteLine("\t\t\tFailed to insert data into the Students table.");
                }
                    Thread.Sleep(1000);
                    Console.Clear();
            }
        }

        }
        public static void AddPersonel(SqlConnection conn)
        {

            using (SqlCommand cmd = new SqlCommand())
            {
                Console.WriteLine("\n\t\t\tInserting data into the Personel table.");
                Console.WriteLine("\t\t\t=========================================\n\n");

                Console.WriteLine("Enter First Name");
                string Fname = Console.ReadLine().ToUpper();
                Console.WriteLine("Enter Last Name");
                string Lname = Console.ReadLine().ToUpper();
                Console.WriteLine("Enter Department");
                string department = Console.ReadLine().ToUpper();
                Console.WriteLine("Enter Course Name");
                string coursename = Console.ReadLine().ToUpper();

                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;

                cmd.CommandText = "insert into Personel(FirstName,LastName,Department,CourseName)OUTPUT INSERTED.Teacher_ID values(@Fname,@Lname,@department,@coursename)";
                cmd.Parameters.AddWithValue("@Fname", Fname);
                cmd.Parameters.AddWithValue("@Lname", Lname);
                cmd.Parameters.AddWithValue("@department", department);
                cmd.Parameters.AddWithValue("@coursename", coursename);

                int teacherId = Convert.ToInt32(cmd.ExecuteScalar());



                cmd.CommandText = "insert into Relation(TeacherID) values(@teacherId)";
                cmd.Parameters.Clear(); // Clear previous parameters
                cmd.Parameters.AddWithValue("@teacherId", teacherId);
              

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("\t\t\tData inserted into the Personel table.\n");
                }
                else
                {
                    Console.WriteLine("\t\t\tFailed to insert data into the Personel table.");
                }

            }
        }
        public static void GetStudentsInClass()
        {

        }
        public static void GetPersonel()
        {

        }
        public static void GetGrade()
        {

        }


    }
    }
    
