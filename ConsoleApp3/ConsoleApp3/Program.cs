using System.Text;

namespace ConsoleApp3
{
    internal class Program
    {
        static async Task  Main()
        {
           CancellationTokenSource cts = new CancellationTokenSource(); 
            CancellationToken token = cts.Token;

            Task<string> task = Task.Run(async () => await Dostuff(token));
            await Task.Delay(400);
            cts.Cancel();
            string result=await task;
            await Console.Out.WriteLineAsync(result);
        }
        static async  Task <string> Dostuff(CancellationToken token) 
        { 
        StringBuilder sb=new StringBuilder();

            while(!token.IsCancellationRequested)
            {
                sb.Append('a');
                await Task.Delay(200); 
            }
         return sb.ToString();
        }
    }
}
