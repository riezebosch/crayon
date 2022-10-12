namespace Crayon
{
    internal static class StringExtensions
    {
        private const string Reset = "\u001b[0m";
        
        public static string? ReformatAfterReset(this string? input, string format) => 
            input?.Replace(Reset, $"{Reset}{format}");
    }
}