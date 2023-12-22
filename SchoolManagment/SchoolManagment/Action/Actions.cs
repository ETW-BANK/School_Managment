using Azure;
using Microsoft.Data.SqlClient;
using SchoolManagment.DBconn;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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

                  Console.WriteLine($"\n\t\t\t\u001b[34mPress A for Ascending or D for Descending\n\t\t\t\u001b[0m");
                  string choice = Console.ReadLine().ToUpper();

          if (choice == "A")
      {
      cmd.CommandText += " ORDER BY FirstName ASC";
      }
      else if (choice == "D")
      {
      cmd.CommandText += " ORDER BY FirstName DESC";
      }
      else
      {
     
      Console.WriteLine("\n\t\t\t\u001b[33mInvalid choice. Defaulting to ascending order.\n\t\t\t\u001b[0m");
                    Thread.Sleep(2000);
                    cmd.CommandText += " ORDER BY FirstName ASC";
      }
                cmd.Connection = conn;

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    Console.WriteLine("\t{0,-15}\t{1,-15}\t{2,-15}\t{3,-15}\t{4,-15}", "Student ID", "First Name", "Last Name", "Gender", "Age");
                    Console.WriteLine("\t{0,-15}\t{1,-15}\t{2,-15}\t{3,-15}\t{4,-15}", "==========", "==========","=========", "======", "===");



                    while (reader.Read())
                    {

                        Console.WriteLine("\t{0,-15}\t{1,-15}\t{2,-15}\t{3,-15}\t{4,-15}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4));
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
               
                Console.WriteLine("\n\t\t\tInserting data into the Students table.");
                Console.WriteLine("\t\t\t=========================================\n\n");
                Console.WriteLine("Enter First Name");
                string Fname = Console.ReadLine().ToUpper();
                Console.WriteLine("Enter Last Name");
                string Lname = Console.ReadLine().ToUpper();
                Console.WriteLine("Enter Student Gender");
                string gender = Console.ReadLine().ToUpper();
                Console.WriteLine("Enter Age");
                int age = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter Class Name");
                string clasname = Console.ReadLine().ToUpper();
                Console.WriteLine("Enter Course Title");
                string course = Console.ReadLine().ToUpper();
                Console.WriteLine("Enter Grade");
                int grade=Convert.ToInt32(Console.ReadLine());

                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;

              
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
                    Console.WriteLine("\t\t\t \u001b[32m Data inserted into the Students table.\u001b[0m\n");
                }
                else
                {
                    Console.WriteLine("\t\t\t\u001b[31mFailed to insert data into the Students table.\u001b[0m");
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
                    Console.WriteLine("\t\t\t\u001b[32mData inserted into the Personel table.\u001b[0m\n");
                }
                else
                {
                    Console.WriteLine("\t\t\t\u001b[31m Failed to insert data into the Personel table.\u001b[0m");
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
                    Console.WriteLine("\t{0,-15}\t{1,-15}\t{2,-15}\t{3,-15}\t{4,-15}", "Student ID", "First Name", "Last Name", "Gender", "Age");
                    Console.WriteLine("\t{0,-15}\t{1,-15}\t{2,-15}\t{3,-15}\t{4,-15}", "==========", "==========", "=========", "======", "===");
                    while (reader.Read())
                    {

                        Console.WriteLine("\t{0,-15}\t{1,-15}\t{2,-15}\t{3,-15}\t{4,-15}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4));
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
                    Console.WriteLine("\t{0,-15}\t{1,-15}\t{2,-15}\t{3,-15}\t{4,-15}", "Personel ID", "First Name", "Last Name", "Department", "Course Name");
                    Console.WriteLine("\t{0,-15}\t{1,-15}\t{2,-15}\t{3,-15}\t{4,-15}", "==============", "==============", "==============", "==============", "==============");



                    while (reader.Read())
                    {
                        Console.WriteLine("\t{0,-15}\t{1,-15}\t{2,-15}\t{3,-15}\t{4,-15}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4));
                    }
                }
                else

                {
                    Console.WriteLine("No rows");
                }
              
                reader.Close();
            }

        }

        public static double GetAverageGrade(SqlConnection conn, string courseTitle)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;

                
                cmd.CommandText = "SELECT AVG(Relation.Grade) AS AverageGrade " +
                                  "FROM Relation " +
                                  "INNER JOIN Course ON Relation.CourseID = Course.Course_ID " +
                                  "WHERE Course.CourseTitel = @courseTitle";

                cmd.Parameters.AddWithValue("@courseTitle", courseTitle);

                object averageGrade = cmd.ExecuteScalar();




                if (averageGrade != null && averageGrade != DBNull.Value && courseTitle !=null )
                {
                 
                    
                    return Math.Round(Convert.ToDouble(averageGrade),1);
                }
                else
                {
                    Console.WriteLine("\u001b[31m Course Titel NOT FOUND \u001b[0m");
                }

               

                
                return 0.0;
            }
        }

        public static void GetLatestGrades(SqlConnection conn)
        {
            using (SqlCommand cmd = new SqlCommand())
            {



                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;



                cmd.CommandText = "SELECT Student.FirstName, Student.LastName, Course.CourseTitel, Relation.Grade, Relation.GradeDate " +
                                "FROM " +
                                "Student " +
                                "JOIN Relation ON Student.Student_ID = Relation.StudentID " +
                                "JOIN Course ON Relation.CourseID = Course.Course_ID " +
                                "WHERE Relation.Grade IS NOT NULL " +
                                "AND Relation.GradeDate IS NOT NULL " +
                                "AND MONTH(Relation.GradeDate) = MONTH(GETDATE()) " +
                                "AND YEAR(Relation.GradeDate) = YEAR(GETDATE());";



                SqlDataReader reader = cmd.ExecuteReader();


                if (reader.HasRows)
                {
                    Console.WriteLine("\t{0,-15}\t{1,-15}\t{2,-15}\t{3,-15}\t{4,-15}", "First Name", "Last Name", "Course Title", "Grade", "Grade Date");
                    Console.WriteLine("\t{0,-15}\t{1,-15}\t{2,-15}\t{3,-15}\t{4,-15}", "==========", "=========", "=============", "=====","==========");



                    while (reader.Read())
                    {

                        Console.WriteLine("\t{0,-15}\t{1,-15}\t{2,-15}\t{3,-15}\t{4,-15}",
                        reader.GetString(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetInt32(3),
                        reader.GetDateTime(4).ToShortDateString());

                    }

                }
                else
                {
                    Console.WriteLine("NoRows");
                }

                reader.Close();
            }

        }
        public static void EscapeKeyCall()
        {
            Console.WriteLine("\n\n");
            Console.WriteLine("\t\t\t\u001b[0m Press \u001b[34m ESC \u001b[0m to exit");

            ConsoleKey key;
            do
            {
                key = Console.ReadKey().Key;

                if (key != ConsoleKey.Escape)
                {
                    Console.WriteLine("\n\t\t\t\u001b[31m Wrong key pressed. Press \u001b[34m ESC\u001b[0m \u001b[31m to exit.\t\t\t\u001b[0m");
                }
            } while (key != ConsoleKey.Escape);
        }












    }
}
    
