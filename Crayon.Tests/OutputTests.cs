using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Xunit;

namespace Crayon.Tests
{
    /// <summary>
    /// http://www.lihaoyi.com/post/BuildyourownCommandLinewithANSIescapecodes.html
    /// </summary>
    public class OutputTests
    {
        public OutputTests() => 
            Output.Enable();

        [Fact]
        public void Enable()
        {
            Environment.SetEnvironmentVariable("NO_COLOR", null);
            typeof(Output).TypeInitializer.Invoke(null, null);
            Output
                .Blue()
                .Should()
                .BeOfType<OutputChain>();
        }
        
        [Fact]
        public void Disable()
        {
            Environment.SetEnvironmentVariable("NO_COLOR", "must-have-some-value");
            typeof(Output).TypeInitializer.Invoke(null, null);
            Output
                .Blue()
                .Should()
                .BeOfType<OutputChainNoColor>();
        }
        

        [Fact]
        public void TestGreen() => 
            Output
                .Green("some text")
                .Should()
                .Be("\u001b[32msome text\u001b[0m");

        [Fact]
        public void TestRed() => 
            Output
                .Red("some text")
                .Should()
                .Be("\u001b[31msome text\u001b[0m");

        [Fact]
        public void NestingColourShouldRestoreOriginal() =>
            Output
                .Green($"green {Output.Red("red")} green")
                .Should()
                .Be("\u001b[32mgreen \u001b[31mred\u001b[0m\u001b[32m green\u001b[0m");

        [Fact]
        public void TestBold() => 
            Output
                .Bold("some text")
                .Should()
                .Be("\u001b[1msome text\u001b[0m");

        [Fact]
        public void TestDim() => 
            Output
                .Dim("some text")
                .Should()
                .Be("\u001b[2msome text\u001b[0m");

        [Fact]
        public void NestedBoldRequiresReset() =>
            Output
                .Red($"{Output.Bold("bold")} text")
                .Should()
                .Be("\u001b[31m\u001b[1mbold\u001b[0m\u001b[31m text\u001b[0m");

        [Fact]
        public void AllOutputInSpecifiedColor()
        {
            var colors = ConstantsFrom<Colors>();
            foreach (var (name, value) in colors)
            {
                ColorMethod(name)
                    .Invoke(null, new[] {"input"})
                    .Should()
                    .Be($"\u001b[{value}minput\u001b[0m");
            }
        }

        [Fact] 
        public void AllDecorationsInSpecifiedColor()
        {
            var colors = ConstantsFrom<Decorations>();
            foreach (var (name, value) in colors)
            {
                ColorMethod(name)
                    .Invoke(null, new[] {"input"})
                    .Should()
                    .Be($"\u001b[{value}minput\u001b[0m");
            }
        }

        [Fact]
        public void AllOutputInSpecifiedBrightColor()
        {
            var colors = ConstantsFrom<Colors>();
            foreach (var (name, value) in colors)
            {
                var method = ColorMethod($"Bright{name}");
                method
                    .Should()
                    .NotBeNull($"No bright method found for {name}");
                method.Invoke(null, new[] {"input"})
                    .Should()
                    .Be($"\u001b[{value};1minput\u001b[0m");
            }
        }

        [Fact]
        public void BrightBlack() => 
            Output
                .BrightBlack("input")
                .Should()
                .Be("\u001b[30;1minput\u001b[0m");

        [Fact]
        public void OutputContainsFactoryMethodsForAllColors()
        {
            var colors = ConstantsFrom<Colors>().Select(s => s.name);
            var methods = ColorMethods();

            colors.Should().BeSubsetOf(methods.Select(m => m.Name));
            methods.ToList().ForEach(m => m.Invoke(null, Array.Empty<object>()));
        }

        [Fact]
        public void OutputContainsFactoryMethodsForAllDecorations()
        {
            var decorations = ConstantsFrom<Decorations>().Select(s => s.name);
            var methods = ColorMethods();

            decorations.Should().BeSubsetOf(methods.Select(m => m.Name));
            methods.ToList().ForEach(m => m.Invoke(null, Array.Empty<object>()));
        }

        private static IEnumerable<MethodInfo> ColorMethods()
        {
            return typeof(Output)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(s => !s.GetParameters().Any());
        }

        private static MethodInfo ColorMethod(string name) => 
            typeof(Output).GetMethod(name, new[] {typeof(string)});

        private static IEnumerable<(string name, object value)> ConstantsFrom<T>() =>
            typeof(T)
                .GetRuntimeFields()
                .Select(x => (x.Name, x.GetRawConstantValue()));
    }
}