using System.Linq;
using FluentAssertions;
using Xunit;

namespace Crayon.Tests
{
    public class RainbowTests
    {
        public RainbowTests() => 
            Output.Enable();

        [Fact]
        public void Test()
        {
            var rainbow = new Rainbow(0.5);
            new[]
            {
                "a",
                "b",
                "c"
            }.Select(x => rainbow.Next().Text(x))
            .Should()
            .BeEquivalentTo(
                "\u001b[38;2;128;243;32ma\u001b[0m", 
                "\u001b[38;2;189;204;4mb\u001b[0m",
                "\u001b[38;2;235;146;6mc\u001b[0m");
        }
    }
}