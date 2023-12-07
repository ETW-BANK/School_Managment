using ConsoleApp1;
using System;
using System.Threading;



class Program
{
    static void Main()
    {
        // Create two cars for the race


        Car ferrari = new Car("Ferrari", 120);
        Car bmw = new Car("BMW", 120);

        Console.WriteLine("The race has started!");
        Thread ferrariThread = new Thread(ferrari.Drive);
        Thread bmwThread = new Thread(bmw.Drive);
        ferrariThread.Start();
        bmwThread.Start();
        ferrariThread.Join();
        bmwThread.Join();

        


        Console.WriteLine("The race is over!");
    }
}
