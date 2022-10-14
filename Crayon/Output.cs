using System;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Crayon.Tests")]

namespace Crayon
{
    public static class Output
    {
        private static Func<IOutput> _output = () => new OutputBuilderIgnoreFormat();

        static Output()
        {
            if (Environment.GetEnvironmentVariable("NO_COLOR") == null)
            {
                Enable();
            }
        }

        public static void Enable()
        {
            ColorsOnWindows.Enable();
            _output = () => new OutputBuilder();
        }

        public static void Disable() => 
            _output = () => new OutputBuilderIgnoreFormat();

        public static string Black(string text) => _output().Black(text);
        public static string Red(string text) => _output().Red(text);
        public static string Green(string text) => _output().Green(text);
        public static string Yellow(string text) => _output().Yellow(text);
        public static string Blue(string text) => _output().Blue(text);
        public static string Magenta(string text) => _output().Magenta(text);
        public static string Cyan(string text) => _output().Cyan(text);
        public static string White(string text) => _output().White(text);
        public static string Bold(string text) => _output().Bold(text);
        public static string Dim(string text) => _output().Dim(text);
        public static string Underline(string text) => _output().Underline(text);
        public static string Reversed(string text) => _output().Reversed(text);

        public static IOutput Black() => _output().Black();
        public static IOutput Red() => _output().Red();
        public static IOutput Green() => _output().Green();
        public static IOutput Yellow() => _output().Yellow();
        public static IOutput Blue() => _output().Blue();
        public static IOutput Magenta() => _output().Magenta();
        public static IOutput Cyan() => _output().Cyan();
        public static IOutput White() => _output().White();

        public static IOutput Bold() => _output().Bold();
        public static IOutput Dim() => _output().Dim();
        public static IOutput Underline() => _output().Underline();
        public static IOutput Reversed() => _output().Reversed();

        public static IOutput Rgb(byte r, byte g, byte b) => _output().Rgb(r, g, b);

        public static IBackground Background => new Background(_output());
        public static IBright Bright => new Bright(_output());
    }
}