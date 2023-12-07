using Microsoft.Data.SqlClient;
using SchoolManagment.Action;
using SchoolManagment.DBconn;
using System;

namespace SchoolManagment
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Connect connect = new Connect(new SqlConnection());
            connect.DBConnect();
           

            do
            {
                Console.WriteLine("\n\n");

                Console.WriteLine($"\t\t ============================================================\n");

                Console.WriteLine($"\t\t Press {"1"} to add a student");
                Console.WriteLine($"\t\t Press {"2"} to add personnel");
                Console.WriteLine($"\t\t Press {"3"} to view the latest grades");
                Console.WriteLine($"\t\t Press {"4"} to view the list of personnel");
                Console.WriteLine($"\t\t Press {"5"} to view the list of all students");
                Console.WriteLine($"\t\t Press {"6"} to view students in a specific class");
                Console.WriteLine($"\t\t Press {"7"} to view the average grade for a specific course");
                Console.WriteLine($"\t\t Press {"0"} to exit\n");

                Console.WriteLine($"\t\t ============================================================\n");
                int choice = Convert.ToInt32(Console.ReadLine());

            

                switch (choice)
                {
                    case 0:

                     Environment.Exit(0);
                  
                    break;

                    case 1:
                   
                     Actions.AddStudent(connect._Conn);
                     Actions.EscapeKeyCall();
                     Console.Clear();
                     break;

                    case 2:

                    Actions.AddPersonel(connect._Conn);
                    Actions.EscapeKeyCall();
                    Console.Clear();
                    break;

                       
                    case 3:
                        
                    Actions.GetLatestGrades(connect._Conn);
                    Actions.EscapeKeyCall();
                    Console.Clear();

                    break;

                   case 4:

                   Actions.GetPersonel(connect._Conn);
                   Actions.EscapeKeyCall();
                   Console.Clear();
                   break;

                   case 5:

                   Actions.GetStudents(connect._Conn);
                   Actions.EscapeKeyCall();
                   Console.Clear();
                  break;

                   case 6:

                   Actions.GetStudentsInClass(connect._Conn);
                   Actions.EscapeKeyCall();
                   Console.Clear();

                   break;

                  case 7:

                  Console.WriteLine("Enter Course Title");
                  string coursetitle = Console.ReadLine().ToUpper();

                 Console.WriteLine($"The Avrage Grade for {coursetitle} is : {Actions.GetAverageGrade(connect._Conn, coursetitle)}");

                  Actions.EscapeKeyCall();
                  Console.Clear();

                  break;

                  default:

                  Console.WriteLine("Wrong Choice");

                  break;
                }




            } while (true);
        }
    }
}
