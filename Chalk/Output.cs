using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Chalk.Tests")]

namespace Chalk
{
    public class Output
    {
        private static readonly string _normal = "\u001b[{0}m";
        private static readonly string _bright = "\u001b[{0};1m";
        
        public static string Black(string input) => Format(input, Colors.Black, _normal);
        public static string Red(string input) => Format(input, Colors.Red, _normal);
        public static string Green(string input) => Format(input, Colors.Green, _normal);
        public static string Yellow(string input) => Format(input, Colors.Yellow, _normal);
        public static string Blue(string input) => Format(input, Colors.Blue, _normal);
        public static string Magenta(string input) => Format(input, Colors.Magenta, _normal);
        public static string Cyan(string input) => Format(input, Colors.Cyan, _normal);
        public static string White(string input) => Format(input, Colors.White, _normal);

        public static string Bold(string input) => Format(input, Decorations.Bold, _normal);
        public static string Underline(string input) => Format(input, Decorations.Underline, _normal);
        public static string Reversed(string input) => Format(input, Decorations.Reversed, _normal);
        
        public static string BrightBlack(string input) => Format(input, Colors.Black, _bright);
        public static string BrightRed(string input) => Format(input, Colors.Red, _bright);
        public static string BrightGreen(string input) => Format(input, Colors.Green, _bright);
        public static string BrightYellow(string input) => Format(input, Colors.Yellow, _bright);
        public static string BrightBlue(string input) => Format(input, Colors.Blue, _bright);
        public static string BrightMagenta(string input) => Format(input, Colors.Magenta, _bright);
        public static string BrightCyan(string input) => Format(input, Colors.Cyan, _bright);
        public static string BrightWhite(string input) => Format(input, Colors.White, _bright);
        
        private static string Format(string input, byte color, string s1)
        {
            var format = string.Format(s1, (int) color);
            return $"{format}{ReplaceInterpolatedResets(input, format)}{reset}";
        }

        private static string ReplaceInterpolatedResets(string input, string format)
        {
            return input.Replace(reset, $"{reset}{format}");
        }

        private const string reset = "\u001b[0m";

    }
}