using FluentAssertions;
using Xunit;

namespace Crayon.Tests
{
    public class DisableColorTests
    {
        public DisableColorTests() =>
            Output.Disable();

        [Fact]
        public void Disable()
        {
            Output
                .Bold("text")
                .Should()
                .Be("text");
        }

        [Fact]
        public void OutputBuilder() =>
            Output.Bold().Should().BeOfType<OutputBuilderIgnoreFormat>();
    }
}