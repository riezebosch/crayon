namespace Crayon
{
    public static class StringExtensions
    {
        private const string Reset = "\u001b[0m";
        
        public static string FormattingAfterReset(this string input, string code)
        {
            return input.Replace(Reset, $"{Reset}{code}");
        }
    }
}