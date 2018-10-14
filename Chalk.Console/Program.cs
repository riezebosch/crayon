using System;

namespace Chalk.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Output.Green($"green {Output.Red($"{Output.Bold("bold")} red")} green"));
            Console.WriteLine("normal");
            
            //Console.WriteLine(Output.Green().Bold().Text("hoi!"));
        }
    }
}