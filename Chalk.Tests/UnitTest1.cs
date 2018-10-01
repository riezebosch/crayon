using FluentAssertions;
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
            new Output().Green("some text").Should().Be("\u001b[32msome text\u001b[0m");
        }
        
        [Fact]
        public void TestRed()
        {
            new Output().Red("some text").Should().Be("\u001b[31msome text\u001b[0m");
        }

        [Fact]
        public void Nested()
        {
            var output = new Output();
            output.Green("green ", output.Red("red")).Should().Be("\u001b[32mgreen \u001b[0m\u001b[32m\u001b[31mred\u001b[0m\u001b[0m");
        }
    }
}