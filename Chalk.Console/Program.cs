using System;

namespace Chalk.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Output.Green("green ", Output.Red("red "), "green"));
            Console.WriteLine("normal");
        }
    }
}