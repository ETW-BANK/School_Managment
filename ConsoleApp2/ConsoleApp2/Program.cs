using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Task<string> task = Task.Run(async () =>
        {
            await Task.Delay(1000);
            return "hi";
        });

        Task<string> task2 = task.ContinueWith(previousTask =>
        {
            string result = previousTask.Result;
            return "the result is: " + result;
        });

        await task2;
        Console.WriteLine(task2.Result);
    }
}