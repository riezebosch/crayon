namespace Crayon
{
    internal class OutputChain : IOutput
    {
        private string _formatting;

        internal OutputChain(int color)
        {
            AppendFormat(color);
        }

        internal OutputChain(string format)
        {
            _formatting = format;
        }        
        
        private OutputChain AppendFormat(int code)
        {
            return AppendFormat($"\u001b[{code}m");
        }

        private OutputChain AppendFormat(string format)
        {
            _formatting += format;
            return this;
        }

        public IOutput Black() => AppendFormat(Colors.Black);
        public IOutput Red() => AppendFormat(Colors.Red);
        public IOutput Green() => AppendFormat(Colors.Green);
        public IOutput Yellow() => AppendFormat(Colors.Yellow);
        public IOutput Blue() => AppendFormat(Colors.Blue);
        public IOutput Magenta() => AppendFormat(Colors.Magenta);
        public IOutput Cyan() => AppendFormat(Colors.Cyan);
        public IOutput White() => AppendFormat(Colors.White);
        public IOutput FromRgb(byte r, byte g, byte b) => AppendFormat($"\u001b[38;2;{r};{g};{b}m");

        public IOutput Bold() => AppendFormat(Decorations.Bold);
        public IOutput Underline() => AppendFormat(Decorations.Underline);
        public IOutput Reversed() => AppendFormat(Decorations.Reversed);

        public string Text(string input) => $"{_formatting}{input.FormattingAfterReset(_formatting)}\u001b[0m";
    }
}