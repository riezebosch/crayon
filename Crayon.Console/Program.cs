using System;
using static Crayon.Output;

namespace Crayon.ConsoleApp
{
    static class Program
    {
        private static void Main()
        {
            Console.WriteLine(Green($"green {Bold("bold")} {Red("red")} green"));
            Console.WriteLine("normal");
            Console.WriteLine(Green().Reversed().Text("green"));
            Console.WriteLine(Green().Reversed("green"));
            Console.WriteLine($"{Bright.Green().Text("Bright")} and {Green("normal")} green");

            Console.WriteLine(Green($"The difference {Bold("between bold")}, {Bright.Green("bright green")} and {Dim("dim")}"));

            Console.WriteLine(Green().Bold().Underline().Reversed().Text("hoi!"));

            Console.WriteLine(
                Bold().Green().Text($"starting green {Red("then red")} must be green again"));

            Console.WriteLine(Rgb(55, 115, 155).Text("from rgb!"));
            Console.WriteLine(Black().Background.Rgb(55, 115, 155).Text("from rgb!"));
            Console.WriteLine(Rgb(55, 115, 155).Background.Green().Text("from rgb!"));
            
            Console.WriteLine(Red().Reversed().Green("green"));
            
            var rainbow = new Rainbow(0.5);
            for (var i = 0; i < 15; i++)
            {
                Console.WriteLine(rainbow.Next().Bold().Text("rainbow"));
            }
        }
    }
}