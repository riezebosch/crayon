using System;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Crayon.Tests")]

namespace Crayon
{
    public static class Output
    {
        private const string Normal = "\u001b[{0}m";
        private const string Bright = "\u001b[{0};1m";
        private const string Reset = "\u001b[0m";

        private static Func<string, byte, string, string> _format;
        private static Func<int, IOutput> _chain;
        private static Func<string, IOutput> _chainFormat;

        static Output()
        {
            if (Environment.GetEnvironmentVariable("NO_COLOR") == null)
            {
                Enable();
            }
            else
            {
                Disable();
            }
        }

        public static void Enable()
        {
            ColorsOnWindows.Enable();
            _format = (input, color, format) =>
             {
                 var code = string.Format(format, (int)color);
                 return $"{code}{input.FormattingAfterReset(code)}{Reset}";
             };

            _chain = color => new OutputChain(color);
            _chainFormat = format => new OutputChain(format);
        }

        public static void Disable()
        {
            _format = (input, code, format) => input;
            _chain = color => new OutputChainNoColor();
            _chainFormat = format => new OutputChainNoColor();
        }

        public static string Black(this string input) => _format(input, Colors.Black, Normal);
        public static string Red(this string input) => _format(input, Colors.Red, Normal);
        public static string Green(this string input) => _format(input, Colors.Green, Normal);
        public static string Yellow(this string input) => _format(input, Colors.Yellow, Normal);
        public static string Blue(this string input) => _format(input, Colors.Blue, Normal);
        public static string Magenta(this string input) => _format(input, Colors.Magenta, Normal);
        public static string Cyan(this string input) => _format(input, Colors.Cyan, Normal);
        public static string White(this string input) => _format(input, Colors.White, Normal);
        public static string Bold(this string input) => _format(input, Decorations.Bold, Normal);
        public static string Dim(this string input) => _format(input, Decorations.Dim, Normal);
        public static string Underline(this string input) => _format(input, Decorations.Underline, Normal);
        public static string Reversed(this string input) => _format(input, Decorations.Reversed, Normal);

        public static string BrightBlack(this string input) => _format(input, Colors.Black, Bright);
        public static string BrightRed(this string input) => _format(input, Colors.Red, Bright);
        public static string BrightGreen(this string input) => _format(input, Colors.Green, Bright);
        public static string BrightYellow(this string input) => _format(input, Colors.Yellow, Bright);
        public static string BrightBlue(this string input) => _format(input, Colors.Blue, Bright);
        public static string BrightMagenta(this string input) => _format(input, Colors.Magenta, Bright);
        public static string BrightCyan(this string input) => _format(input, Colors.Cyan, Bright);
        public static string BrightWhite(this string input) => _format(input, Colors.White, Bright);

        public static IOutput Black() => _chain(Colors.Black);
        public static IOutput Red() => _chain(Colors.Red);
        public static IOutput Green() => _chain(Colors.Green);
        public static IOutput Yellow() => _chain(Colors.Yellow);
        public static IOutput Blue() => _chain(Colors.Blue);
        public static IOutput Magenta() => _chain(Colors.Magenta);
        public static IOutput Cyan() => _chain(Colors.Cyan);
        public static IOutput White() => _chain(Colors.White);

        public static IOutput Bold() => _chain(Decorations.Bold);
        public static IOutput Dim() => _chain(Decorations.Dim);
        public static IOutput Underline() => _chain(Decorations.Underline);
        public static IOutput Reversed() => _chain(Decorations.Reversed);

        public static IOutput FromRgb(byte r, byte g, byte b) => _chainFormat($"\u001b[38;2;{r};{g};{b}m");
    }
}