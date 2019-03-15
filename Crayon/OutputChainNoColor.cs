namespace Crayon
{
    public class OutputChainNoColor : IOutput
    {
        public IOutput Black() => this;

        public IOutput Red() => this;

        public IOutput Green() => this;

        public IOutput Yellow() => this;

        public IOutput Blue() => this;

        public IOutput Magenta() => this;

        public IOutput Cyan() => this;

        public IOutput White() => this;

        public IOutput Bold() => this;

        public IOutput Dim() => this;

        public IOutput Underline() => this;

        public IOutput Reversed() => this;

        public string Text(string input) => input;

        public IOutput FromRgb(byte r, byte g, byte b) => this;
    }
}