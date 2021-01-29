using System.Linq;
using FluentAssertions;
using Xunit;

namespace Crayon.Tests
{
    public static class OutputBuilderIgnoreFormatTests
    {
        [Fact]
        public static void InputEqualsOutput()
        {
            var output = new OutputBuilderIgnoreFormat();
            typeof(OutputBuilderIgnoreFormat)
                .GetMethods()
                .Where(m => m.ReturnType == typeof(string))
                .Where(m => m.GetParameters().Length == 1)
                .Select(m => m.Invoke(output, new object[] { "input" }))
                .Should()
                .AllBeEquivalentTo("input");
        }
        
        [Fact]
        public static void FromRgb()
        {
            var output = new OutputBuilderIgnoreFormat();
            output
                .Rgb(0, 0, 0)
                .Should()
                .Be(output);
        }

        [Fact]
        public static void Text() =>
            new OutputBuilderIgnoreFormat()
                .Black()
                .Text("input")
                .Should()
                .Be("input");
    }
}