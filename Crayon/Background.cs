namespace Crayon
{
    public class Background : IBackground
    {
        private readonly IOutput _chain;

        public Background(IOutput chain) => _chain = chain;

        public IOutput Black() => AppendFormat(Colors.Black);
        public IOutput Red() => AppendFormat(Colors.Red);
        public IOutput Green() => AppendFormat(Colors.Green);
        public IOutput Yellow() => AppendFormat(Colors.Yellow);
        public IOutput Blue() => AppendFormat(Colors.Blue);
        public IOutput Magenta() => AppendFormat(Colors.Magenta);
        public IOutput Cyan() => AppendFormat(Colors.Cyan);
        public IOutput White() => AppendFormat(Colors.White);
        public IOutput Rgb(byte r, byte g, byte b) => _chain.Append($"\u001b[48;2;{r};{g};{b}m");

        public string Black(string text) => Black().Text(text);
        public string Red(string input) => Red().Text(input);
        public string Green(string input) => Green().Text(input);
        public string Yellow(string input) => Yellow().Text(input);
        public string Blue(string input) => Blue().Text(input);
        public string Magenta(string input) => Magenta().Text(input);
        public string Cyan(string input) => Cyan().Text(input);
        public string White(string input) => White().Text(input);
        public string Rgb(byte r, byte g, byte b, string input) => Rgb(r, g, b).Text(input);
        
        private IOutput AppendFormat(int code) => _chain.Append($"\u001b[{code + 10}m");
    }
}