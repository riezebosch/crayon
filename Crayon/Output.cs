[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Crayon.Tests")]

namespace Crayon
{
    public static class Output
    {
        private const string Normal = "\u001b[{0}m";
        private const string Bright = "\u001b[{0};1m";
        private const string Reset = "\u001b[0m";

        static Output()
        {
            ColorsOnWindows.Enable();
        }

        public static string Black(string input) => Format(input, Colors.Black, Normal);
        public static string Red(string input) => Format(input, Colors.Red, Normal);
        public static string Green(string input) => Format(input, Colors.Green, Normal);
        public static string Yellow(string input) => Format(input, Colors.Yellow, Normal);
        public static string Blue(string input) => Format(input, Colors.Blue, Normal);
        public static string Magenta(string input) => Format(input, Colors.Magenta, Normal);
        public static string Cyan(string input) => Format(input, Colors.Cyan, Normal);
        public static string White(string input) => Format(input, Colors.White, Normal);

        public static string Bold(string input) => Format(input, Decorations.Bold, Normal);
        public static string Underline(string input) => Format(input, Decorations.Underline, Normal);
        public static string Reversed(string input) => Format(input, Decorations.Reversed, Normal);

        public static string BrightBlack(string input) => Format(input, Colors.Black, Bright);
        public static string BrightRed(string input) => Format(input, Colors.Red, Bright);
        public static string BrightGreen(string input) => Format(input, Colors.Green, Bright);
        public static string BrightYellow(string input) => Format(input, Colors.Yellow, Bright);
        public static string BrightBlue(string input) => Format(input, Colors.Blue, Bright);
        public static string BrightMagenta(string input) => Format(input, Colors.Magenta, Bright);
        public static string BrightCyan(string input) => Format(input, Colors.Cyan, Bright);
        public static string BrightWhite(string input) => Format(input, Colors.White, Bright);

        private static string Format(string input, byte color, string format)
        {
            var code = string.Format(format, (int) color);
            return $"{code}{input.FormattingAfterReset(code)}{Reset}";
        }

        public static IOutput Black() => new OutputChain(Colors.Black);
        public static IOutput Red() => new OutputChain(Colors.Red);
        public static IOutput Green() => new OutputChain(Colors.Green);
        public static IOutput Yellow() => new OutputChain(Colors.Yellow);
        public static IOutput Blue() => new OutputChain(Colors.Blue);
        public static IOutput Magenta() => new OutputChain(Colors.Magenta);
        public static IOutput Cyan() => new OutputChain(Colors.Cyan);
        public static IOutput White() => new OutputChain(Colors.White);
        
        public static IOutput Bold() => new OutputChain(Decorations.Bold);
        public static IOutput Underline() => new OutputChain(Decorations.Underline);
        public static IOutput Reversed() => new OutputChain(Decorations.Reversed);

        public static IOutput FromRgb(byte r, byte g, byte b) => new OutputChain($"\u001b[38;2;{r};{g};{b}m");
    }
}