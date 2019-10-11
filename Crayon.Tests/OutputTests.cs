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
        public void Enable() => 
            TestInitialize(null, typeof(OutputChain));

        [Fact]
        public void Disable() =>
            TestInitialize("some-value", typeof(OutputChainNoColor));

        [Fact]
        public void TestGreen() => 
            "some text"
                .Green()
                .Should()
                .Be("\u001b[32msome text\u001b[0m");

        [Fact]
        public void TestRed() => 
            "some text"
                .Red()
                .Should()
                .Be("\u001b[31msome text\u001b[0m");

        [Fact]
        public void NestingColourShouldRestoreOriginal() =>
            $"green {"red".Red()} green".Green()
                .Should()
                .Be("\u001b[32mgreen \u001b[31mred\u001b[0m\u001b[32m green\u001b[0m");

        [Fact]
        public void TestBold() => 
            "some text"
                .Bold()
                .Should()
                .Be("\u001b[1msome text\u001b[0m");

        [Fact]
        public void TestDim() => 
            "some text"
                .Dim()
                .Should()
                .Be("\u001b[2msome text\u001b[0m");

        [Fact]
        public void NestedBoldRequiresReset() =>
            $"{"bold".Bold()} text"
                .Red()
                .Should()
                .Be("\u001b[31m\u001b[1mbold\u001b[0m\u001b[31m text\u001b[0m");

        [Fact]
        public void AllOutputInSpecifiedColor() => 
            TestConstants<Colors>();
       
        [Fact]
        public void AllDecorationsInSpecifiedColor() => 
            TestConstants<Decorations>();

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
                method.Invoke(null, new object[] {"input"})
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
        public void OutputContainsFactoryMethodsForAllColors() => 
            TestFactories<Colors>();

        [Fact]
        public void OutputContainsFactoryMethodsForAllDecorations() => 
            TestFactories<Decorations>();

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
        
        
        private static void TestInitialize(string value, Type expected)
        {
            Environment.SetEnvironmentVariable("NO_COLOR", value);
            typeof(Output).TypeInitializer.Invoke(null, null);
            Output
                .Blue()
                .Should()
                .BeOfType(expected);
        }
        
        private static void TestConstants<T>()
        {
            var colors = ConstantsFrom<T>();
            foreach (var (name, value) in colors)
            {
                ColorMethod(name)
                    .Invoke(null, new object[] {"input"})
                    .Should()
                    .Be($"\u001b[{value}minput\u001b[0m");
            }
        }
        
        private static void TestFactories<T>()
        {
            var colors = ConstantsFrom<T>().Select(s => s.name);
            var methods = ColorMethods();

            colors.Should().BeSubsetOf(methods.Select(m => m.Name));
            methods.ToList().ForEach(m => m.Invoke(null, Array.Empty<object>()));
        }
    }
}