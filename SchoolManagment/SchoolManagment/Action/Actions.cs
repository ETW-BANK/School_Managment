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
     


        public static void AddStudent(SqlConnection conn)
        {
            using (SqlCommand cmd = new SqlCommand())
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
                Console.WriteLine("Enter Grade");
                int grade=Convert.ToInt32(Console.ReadLine());

                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;

                // Insert into Student table and get the generated Student_ID
                cmd.CommandText = "INSERT INTO Student(FirstName, LastName, Gender, Age) OUTPUT INSERTED.Student_ID VALUES (@Fname, @Lname, @gender, @age)";
                cmd.Parameters.AddWithValue("@Fname", Fname);
                cmd.Parameters.AddWithValue("@Lname", Lname);
                cmd.Parameters.AddWithValue("@gender", gender);
                cmd.Parameters.AddWithValue("@age", age);

                int studentId = Convert.ToInt32(cmd.ExecuteScalar());

                // Insert into Course table and get the generated Course_ID
                cmd.CommandText = "INSERT INTO Course(CourseTitel) OUTPUT INSERTED.Course_ID VALUES (@coursetitle)";
                cmd.Parameters.Clear(); // Clear previous parameters
                cmd.Parameters.AddWithValue("@coursetitle", course);

                int courseId = Convert.ToInt32(cmd.ExecuteScalar());

                cmd.CommandText = "INSERT INTO Relation(Grade,GradeDate) OUTPUT INSERTED.Course_ID VALUES (@grade,@gradedate)";
                cmd.Parameters.Clear(); // Clear previous parameters
                cmd.Parameters.AddWithValue("@coursetitle", course);

                //int courseId = Convert.ToInt32(cmd.ExecuteScalar());



                // Insert into Class table and get the generated Class_ID
                cmd.CommandText = "INSERT INTO Class(ClassName) OUTPUT INSERTED.Class_ID VALUES (@classname)";
                cmd.Parameters.Clear(); // Clear previous parameters
                cmd.Parameters.AddWithValue("@classname", clasname);

                int classId = Convert.ToInt32(cmd.ExecuteScalar());
                DateTime gradedate=DateTime.Now;

                // Insert into Relation table using the generated IDs
                cmd.CommandText = "INSERT INTO Relation(StudentID, CourseID, ClassID,Grade,GradeDate) VALUES (@studentId, @courseId, @classId,@grade,@gradedate)";
                cmd.Parameters.Clear(); // Clear previous parameters
                cmd.Parameters.AddWithValue("@studentId", studentId);
                cmd.Parameters.AddWithValue("@courseId", courseId);
                cmd.Parameters.AddWithValue("@classId", classId);
                cmd.Parameters.AddWithValue("@grade", grade);
                cmd.Parameters.AddWithValue("@gradedate", gradedate);


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
        public static void GetStudentsInClass(SqlConnection conn)
        {
            using (SqlCommand cmd = new SqlCommand())
            {

                Console.WriteLine("Enter Class Name");
                string ClassName = Console.ReadLine().ToUpper();
             
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                cmd.CommandText = "select Student.Student_ID,Student.FirstName,Student.LastName,Student.Gender,Student.Age " +
                    "from " +
                    "Student " +
                    "left join Relation on Student.Student_ID=Relation.StudentID " +
                    "left join Class on Relation.ClassID=Class.Class_ID " +
                    "where Class.ClassName=@ClassName";
           

                cmd.Parameters.AddWithValue("@ClassName", ClassName);
              
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        Console.WriteLine($"Student ID: {reader["Student_ID"]}, Name: {reader["FirstName"]} {reader["LastName"]}, Gender: {reader["Gender"]}, Age: {reader["Age"]}");
                    }


                }
                else
                {
                    Console.WriteLine("No students found in the specified class.");
                }
                reader.Close();

            }

        }
        public static void GetPersonel(SqlConnection conn)
        {
            using (SqlCommand cmd=new SqlCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.Connection= conn;
                cmd.CommandText = "select * from Personel WHERE CourseName IS NOT NULL ";

                SqlDataReader reader=cmd.ExecuteReader();   

                if (reader.HasRows)
                {
                    Console.WriteLine("\t\t\t{0}\t{1}\t{2}\t{3}\t\t{4}", "Personel ID", "First Name", "Last Name", "Department", "Course Name");
                    Console.WriteLine("\t\t\t{0}\t{1}\t{2}\t{3}\t\t{4}", "==========", "============", "========", "==========", "===========");


                    while (reader.Read())
                    {
                        Console.WriteLine("\t\t\t{0}\t\t{1}\t\t{2}\t\t{3}\t\t{4}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4));

                    }
                }
                else

                {
                    Console.WriteLine("No rows");
                }
              
                reader.Close();
            }

        }
        public static void GetAvrageGrade()
        {

        }


    }
    }
    
