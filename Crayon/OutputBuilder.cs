using System.Text;

namespace Crayon
{
    internal class OutputBuilder : IOutput
    {
        private readonly StringBuilder _formatting = new StringBuilder();

        public IOutput Black() => Append(Colors.Black);
        public IOutput Red() => Append(Colors.Red);
        public IOutput Green() => Append(Colors.Green);
        public IOutput Yellow() => Append(Colors.Yellow);
        public IOutput Blue() => Append(Colors.Blue);
        public IOutput Magenta() => Append(Colors.Magenta);
        public IOutput Cyan() => Append(Colors.Cyan);
        public IOutput White() => Append(Colors.White);
        public string Black(string text) => Black().Text(text);
        public string Red(string text)=> Red().Text(text);
        public string Green(string text)=> Green().Text(text);
        public string Yellow(string text)=> Yellow().Text(text);
        public string Blue(string text)=> Blue().Text(text);
        public string Magenta(string text)=> Magenta().Text(text);
        public string Cyan(string text)=> Cyan().Text(text);
        public string White(string text)=> White().Text(text);
        
        public string Rgb(byte r, byte g, byte b, string text) => Rgb(r, g, b).Text(text);
        public IOutput Rgb(byte r, byte g, byte b) => Append($"\u001b[38;2;{r};{g};{b}m");
        
        public IOutput Bold() => Append(Decorations.Bold);
        public IOutput Dim() => Append(Decorations.Dim);
        public IOutput Underline() => Append(Decorations.Underline);
        public IOutput Reversed() => Append(Decorations.Reversed);
        public string Bold(string text) => Bold().Text(text);
        public string Dim(string text) => Dim().Text(text);
        public string Underline(string text) => Underline().Text(text);
        public string Reversed(string text) => Reversed().Text(text);
        
        public IBright Bright => new Bright(this);
        public IBackground Background => new Background(this);
        
        public string Text(string text) =>
            _formatting
                .Append(text.ReformatAfterReset(_formatting.ToString()))
                .Append("\u001b[0m").ToString();

        public IOutput Append(string format)
        {
            _formatting.Append(format);
            return this;
        }

        private IOutput Append(int code) => Append($"\u001b[{code}m");
    }
}