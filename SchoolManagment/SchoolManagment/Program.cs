using Microsoft.Data.SqlClient;
using SchoolManagment.Action;
using SchoolManagment.DBconn;

namespace SchoolManagment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Actions.GetStudents();


            Console.WriteLine($"\t\t\t\t\t WELCOME TO CHAS ACADEMY");
            Console.WriteLine($"\t\t\t\t\t=========================\n");


            do
            {

                Console.WriteLine($"\t\t Press {"3"} to view list of students , Press {"1"} to add student , Press {"2"} to Add Personel");
                Console.WriteLine($"\t\t =====================================================================================\n");
                int choice = Convert.ToInt32(Console.ReadLine());



               Connect connect = new Connect(new SqlConnection());
                connect.DBConnect();

                switch (choice)
                {


                    case 1:


                        // Insert the Student  into the database
                        Actions.AddStudent(connect._Conn);
                        connect._Conn.Close();
                        break;

                    case 2:

                        Actions.AddPersonel(connect._Conn);
                        connect._Conn.Close();
                     break;

                       


                    case 3:
                        Actions.GetStudents(connect._Conn);
                        connect._Conn.Close();

                        break;

                    case 'u':



                     
                        break;

                    default:

                        Console.WriteLine("Wrong Choice");

                        break;
                }




            } while (true);
        }
    }
}
