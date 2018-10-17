using System;

namespace Chalk.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Output.Green($"green {Output.Red($"{Output.Bold("bold")} red")} green"));
            Console.WriteLine("normal");
            Console.WriteLine(Output.BrightBlue($"Bright {Output.Green("and normal green")}"));

            Console.WriteLine(Output.Green($"The difference {Output.Bold("between bold")} and {Output.BrightGreen("bright green")}"));
            
            Console.WriteLine(Output.Green().Bold().Underline().Reversed().Text("hoi!"));
        }
    }
}