using System;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Crayon.Tests
{
    public static class OutputChainNoColorTests
    {
        [Fact]
        public static void AlwaysReturnsItself()
        {
            var output = new OutputChainNoColor();
            typeof(IOutput)
                .GetMethods()
                .Where(m => m.ReturnType == typeof(IOutput))
                .Where(m => !m.GetParameters().Any())
                .Select(m => m.Invoke(output, Array.Empty<object>()))
                .Should()
                .AllBeEquivalentTo(output);
        }
        
        [Fact]
        public static void FromRgb()
        {
            var output = new OutputChainNoColor();
            output
                .FromRgb(0, 0, 0)
                .Should()
                .Be(output);
        }

        [Fact]
        public static void Text() =>
            new OutputChainNoColor()
                .Black()
                .Text("input")
                .Should()
                .Be("input");
    }
}