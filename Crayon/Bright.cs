namespace Crayon
{
    internal class Bright : IBright
    {
        private readonly IOutput _chain;

        public Bright(IOutput chain) => _chain = chain;
        
        public IOutput Black() => Append(Colors.Black);
        public IOutput Red() => Append(Colors.Red);
        public IOutput Green() => Append(Colors.Green);
        public IOutput Yellow() => Append(Colors.Yellow);
        public IOutput Blue() => Append(Colors.Blue);
        public IOutput Magenta() => Append(Colors.Magenta);
        public IOutput Cyan() => Append(Colors.Cyan);
        public IOutput White() => Append(Colors.White);
        public string Black(string text) => Append(Colors.Black).Text(text);
        public string Red(string text) => Append(Colors.Red).Text(text);
        public string Green(string text) => Append(Colors.Green).Text(text);
        public string Yellow(string text) => Append(Colors.Yellow).Text(text);
        public string Blue(string text) => Append(Colors.Blue).Text(text);
        public string Magenta(string text) => Append(Colors.Magenta).Text(text);
        public string Cyan(string text) => Append(Colors.Cyan).Text(text);
        public string White(string text) => Append(Colors.White).Text(text);
        private IOutput Append(int code) => _chain.Append($"\u001b[{code};1m");
    }
}