using System;
using Xunit;

namespace Chalk.Tests
{
    /// <summary>
    /// http://www.lihaoyi.com/post/BuildyourownCommandLinewithANSIescapecodes.html
    /// </summary>
    public class UnitTest1
    {
        [Fact]
        public void TestGreen()
        {
            var input = "some text";
            var result = Green(input);
            
            Assert.Equal("\u001b[32msome text\u001b[0m", result);
        }
        
        [Fact]
        public void TestRed()
        {
            var input = "some text";
            var result = Red(input);
            
            Assert.Equal("\u001b[31msome text\u001b[0m", result);
        }

        private static string Green(string input)
        {
            return Color(input, "32");
        }

        private static string Color(string input, string code)
        {
            return $"\u001b[{code}m{input}[0m";
        }

        private static string Red(string input)
        {
            return Color(input, "31");
        }
    }
}