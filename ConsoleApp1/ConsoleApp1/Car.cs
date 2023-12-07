using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Car
    {
        private static readonly Random rand = new Random();
        public string Name { get; }
        public int Distance { get; private set; }
        public int Speed { get; private set; }

        public Car(string name, int speed)
        {
            Name = name;
            Distance = 0;
            Speed = speed;
        }
       
            public void Drive()
            {
            

                while (Distance <= 10)  // Race distance is 10 km
                {
                    Thread.Sleep(1000);  // Sleep for 1 second (simulating time)

                    // Simulate random events every 30 seconds
                    if (DateTime.Now.Second % 30 == 0)
                    {
                        int eventProbability = rand.Next(1, 10);

                        if (eventProbability <= 2)
                        {
                            Console.WriteLine($"{Name} has run out of gas! Needs to refuel and stops for 30 seconds.");
                            Thread.Sleep(30000);
                        }
                        else if (eventProbability <= 4)
                        {
                            Console.WriteLine($"{Name} has a flat tire! Needs to change it and stops for 20 seconds.");
                            Thread.Sleep(20000);
                        }
                        else if (eventProbability <= 10)
                        {
                            Console.WriteLine($"{Name} has a bird on the windshield! Stops to clean it for 10 seconds.");
                            Thread.Sleep(10000);
                        }
                        else if (eventProbability <= 30)
                        {
                            Console.WriteLine($"{Name} has a minor engine issue! Speed reduced by 1 km/h.");
                            Speed -= 1;
                        }
                    }

                    // Update the distance based on the speed
                    Distance += Speed;
                }

                Console.WriteLine($"{Name} has finished the race!");
            }
        }
    }

    
