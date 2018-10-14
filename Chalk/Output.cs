using System.Text;

namespace Chalk
{
    public class Output
    {
        public static string Green(string input)
        {
            return Format(input, 32);
        }

        private static string Format(string input, int code)
        {
            var format = $"\u001b[{code}m";
            return $"{format}{ReplaceInterpolatedResets(input, format)}{reset}";
        }

        private static string ReplaceInterpolatedResets(string input, string format)
        {
            return input.Replace(reset, $"{reset}{format}");
        }

        private const string reset = "\u001b[0m";

        public static string Red(string input)
        {
            return Format(input, 31);
        }

        public static string Bold(string input)
        {
            return Format(input, 1);
        }
    }
}