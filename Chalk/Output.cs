namespace Chalk
{
    public class Output
    {
        public static string Black(string input) => Format(input, Colors.Black);
        public static string Red(string input) => Format(input, Colors.Red);
        public static string Green(string input) => Format(input, Colors.Green);
        public static string Yellow(string input) => Format(input, Colors.Yellow);
        public static string Blue(string input) => Format(input, Colors.Blue);
        public static string Magenta(string input) => Format(input, Colors.Magenta);
        public static string Cyan(string input) => Format(input, Colors.Cyan);
        public static string White(string input) => Format(input, Colors.White);
        public static string Bold(string input) => Format(input, Colors.Bold);
        public static string Underline(string input) => Format(input, Colors.Underline);
        public static string Reversed(string input) => Format(input, Colors.Reversed);
        public static string Reset(string input) => Format(input, Colors.Reset);

        private static string Format(string input, Colors color)
        {
            var format = $"\u001b[{(int)color}m";
            return $"{format}{ReplaceInterpolatedResets(input, format)}{reset}";
        }

        private static string ReplaceInterpolatedResets(string input, string format)
        {
            return input.Replace(reset, $"{reset}{format}");
        }

        private const string reset = "\u001b[0m";

    }
}