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

                Console.WriteLine($"\t\t Press {"3"} to view list of students , Press {"1"} to add student , Press {"2"} to Add Personel");
                Console.WriteLine($"\t\t =====================================================================================\n");
                int choice = Convert.ToInt32(Console.ReadLine());

            

                switch (choice)
                {


                    case 1:
                      

                     Actions.AddStudent(connect._Conn);
                       

                    break;

                    case 2:

                    Actions.AddPersonel(connect._Conn);
                   
                    break;

                       
                    case 3:
                        
                    Actions.GetLatestGrades(connect._Conn);
               
                   break;

                   case 4:

                   Actions.GetPersonel(connect._Conn);
                
                   break;

                   case 5:

                   Actions.GetStudents(connect._Conn);
                
                   break;

                   case 6:

                   Actions.GetStudentsInClass(connect._Conn);
                  

                  break;

                  case 7:

                   Actions.GetAvrageGrade(connect._Conn);


                  break;

                  default:

                  Console.WriteLine("Wrong Choice");

                  break;
                }




            } while (true);
        }
    }
}
