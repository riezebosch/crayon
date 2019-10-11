using FluentAssertions;
using Xunit;

namespace Crayon.Tests
{
    public class NoColor
    {
        public NoColor() =>
            Output.Disable();

        [Fact]
        public void DisableColor()
        {
            Output
                .Bold("text")
                .Should()
                .Be("text");
        }

        [Fact]
        public void OutputChain() => Assert.IsType<OutputChainNoColor>(Output.Bold());
    }
}