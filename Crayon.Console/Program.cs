using System;

namespace Crayon.ConsoleApp
{
    static class Program
    {
        private static void Main()
        {
            Console.WriteLine($"{"green".Green()} {Output.Red($"{Output.Bold("bold")} red")} green");
            Console.WriteLine("normal");
            Console.WriteLine("green".Green().Reversed());
            Console.WriteLine($"{Output.BrightGreen($"Bright")} and {Output.Green("normal")} green");

            Console.WriteLine($"The difference {"between bold".Bold()}, {"bright green".BrightGreen()} and {"dim".Dim()}".Green());

            Console.WriteLine(Output.Green().Bold().Underline().Reversed().Text("hoi!"));

            Console.WriteLine(
                Output.Bold().Green().Text($"starting green {Output.Red("then red")} must be green again"));

            Console.WriteLine(Output.FromRgb(55, 115, 155).Text("from rgb!"));

            var rainbow = new Rainbow(0.5);
            for (var i = 0; i < 15; i++)
            {
                Console.WriteLine(rainbow.Next().Bold().Text("rainbow"));
            }
        }
    }
}