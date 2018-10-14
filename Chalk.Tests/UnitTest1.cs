using System;
using FluentAssertions;
using Microsoft.VisualBasic.CompilerServices;
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
            Output.Green("some text").Should().Be("\u001b[32msome text\u001b[0m");
        }
        
        [Fact]
        public void TestRed()
        {
            Output.Red("some text").Should().Be("\u001b[31msome text\u001b[0m");
        }

        [Fact]
        public void NestingColourShouldRestoreOriginal()
        {
            Output.Green($"green {Output.Red("red")} green").Should().Be("\u001b[32mgreen \u001b[31mred\u001b[0m\u001b[32m green\u001b[0m");
        }

        [Fact]
        public void TestBold()
        {
            Output.Bold("some text").Should().Be("\u001b[1msome text\u001b[0m");
        }
        
        [Fact]
        public void NestedBoldRequiresReset()
        {
            Output.Red($"{Output.Bold("bold")} text").Should().Be("\u001b[31m\u001b[1mbold\u001b[0m\u001b[31m text\u001b[0m");
        }
    }
}