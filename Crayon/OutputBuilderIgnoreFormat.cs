namespace Crayon
{
    public class OutputBuilderIgnoreFormat : IOutput
    {
        public IOutput Black() => this;
        public IOutput Red() => this;
        public IOutput Green() => this;
        public IOutput Yellow() => this;
        public IOutput Blue() => this;
        public IOutput Magenta() => this;
        public IOutput Cyan() => this;
        public IOutput White() => this;
        
        public string Black(string text) => text;
        public string Red(string input) => input;
        public string Green(string input) => input;
        public string Yellow(string input) => input;
        public string Blue(string input) => input;
        public string Magenta(string input) => input;
        public string Cyan(string input) => input;
        public string White(string input) => input;
        
        public IOutput Rgb(byte r, byte g, byte b) => this;
        public string Rgb(byte r, byte g, byte b, string input) => input;


        public IOutput Bold() => this;
        public IOutput Dim() => this;
        public IOutput Underline() => this;
        public IOutput Reversed() => this;
        public string Bold(string input) => Text(input);
        public string Dim(string input) => Text(input);
        public string Underline(string input) => Text(input);
        public string Reversed(string input) => Text(input);
        
        public IBright Bright => new Bright(this);
        public IBackground Background => new Background(this);
        
        public string Text(string text) => text;
        public IOutput Append(string format) => this;
    }
}