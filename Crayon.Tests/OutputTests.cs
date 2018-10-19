using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualBasic.CompilerServices;
using Xunit;

namespace Crayon.Tests
{
    /// <summary>
    /// http://www.lihaoyi.com/post/BuildyourownCommandLinewithANSIescapecodes.html
    /// </summary>
    public class OutputTests
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
            Output.Green($"green {Output.Red("red")} green").Should()
                .Be("\u001b[32mgreen \u001b[31mred\u001b[0m\u001b[32m green\u001b[0m");
        }

        [Fact]
        public void TestBold()
        {
            Output.Bold("some text").Should().Be("\u001b[1msome text\u001b[0m");
        }

        [Fact]
        public void NestedBoldRequiresReset()
        {
            Output.Red($"{Output.Bold("bold")} text").Should()
                .Be("\u001b[31m\u001b[1mbold\u001b[0m\u001b[31m text\u001b[0m");
        }

        [Fact]
        public void OutputContainsMethodsForAllColors()
        {
            var colors = typeof(Colors).GetRuntimeFields().Select(s => s.Name);
            var methods = typeof(Output).GetMembers(BindingFlags.Public | BindingFlags.Static).Select(s => s.Name);

            colors.Should().BeSubsetOf(methods);
        }

        [Fact]
        public void AllOutputInSpecifiedColor()
        {
            var colors = typeof(Colors)
                .GetRuntimeFields();
            foreach (var c in colors)
            {
                typeof(Output).GetMethod(c.Name, new[] {typeof(string)}).Invoke(null, new[] {"input"}).Should()
                    .Be($"\u001b[{c.GetRawConstantValue()}minput\u001b[0m");
            }
        }

        [Fact]
        public void AlDecorationsInSpecifiedColor()
        {
            var colors = typeof(Decorations)
                .GetRuntimeFields();
            foreach (var c in colors)
            {
                typeof(Output).GetMethod(c.Name, new[] {typeof(string)}).Invoke(null, new[] {"input"}).Should()
                    .Be($"\u001b[{c.GetRawConstantValue()}minput\u001b[0m");
            }
        }

        [Fact]
        public void AllOutputInSpecifiedBrightColor()
        {
            var colors = typeof(Colors)
                .GetRuntimeFields();
            foreach (var c in colors)
            {
                var method = typeof(Output).GetMethod($"Bright{c.Name}", new[] {typeof(string)});

                method.Should().NotBeNull($"No bright method found for {c.Name}");
                method.Invoke(null, new[] {"input"}).Should().Be($"\u001b[{c.GetRawConstantValue()};1minput\u001b[0m");
            }
        }

        [Fact]
        public void BrightBlack()
        {
            Output.BrightBlack("input").Should().Be("\u001b[30;1minput\u001b[0m");
        }

        
        [Fact]
        public void OutputContainsFactoryMethodsForAllColors()
        {
            var colors = typeof(Colors).GetRuntimeFields().Select(s => s.Name);
            var methods = typeof(Output).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(s => !s.GetParameters().Any()).Select(s => s.Name);

            colors.Should().BeSubsetOf(methods);
        }

        [Fact]
        public void OutputContainsFactoryMethodsForAllDecorations()
        {
            var decorations = typeof(Decorations).GetRuntimeFields().Select(s => s.Name);
            var methods = typeof(Output).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(s => !s.GetParameters().Any()).Select(s => s.Name);

            decorations.Should().BeSubsetOf(methods);
        }
    }
}