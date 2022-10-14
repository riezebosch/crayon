using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Xunit;
using static Crayon.Output;

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
            Blue()
                .Should()
                .BeOfType<OutputBuilder>();

        [Fact]
        public void Disable()
        {
            Output.Disable();
            Blue()
                .Should()
                .BeOfType<OutputBuilderIgnoreFormat>();
        }

        [Fact]
        public void TestGreen() => 
            Green("some text")
                .Should()
                .Be("\u001b[32msome text\u001b[0m");

        [Fact]
        public void TestRed() => 
            Red("some text")
                .Should()
                .Be("\u001b[31msome text\u001b[0m");

        [Fact]
        public void NestingColourShouldRestoreOriginal() =>
            Green($"green {Red("red")} green")
                .Should()
                .Be("\u001b[32mgreen \u001b[31mred\u001b[0m\u001b[32m green\u001b[0m");

        [Fact]
        public void TestBold() => 
            Bold("some text")
                .Should()
                .Be("\u001b[1msome text\u001b[0m");

        [Fact]
        public void TestDim() => 
            Dim("some text")
                .Should()
                .Be("\u001b[2msome text\u001b[0m");

        [Fact]
        public void NestedBoldRequiresReset() =>
            Red($"{Bold("bold")} text")
                .Should()
                .Be("\u001b[31m\u001b[1mbold\u001b[0m\u001b[31m text\u001b[0m");
                
        [Fact]
        public void Background() =>
            Output.Black().Background.Green("text")
                .Should()
                .Be("\u001b[30m\u001b[42mtext\u001b[0m");
        
        [Fact]
        public void OutputBackground() =>
            Output.Background.Green("text")
                .Should()
                .Be("\u001b[42mtext\u001b[0m");

        [Fact]
        public void AllOutputInSpecifiedColor() => 
            TestConstants<Colors>();
       
        [Fact]
        public void AllDecorationsInSpecifiedColor() => 
            TestConstants<Decorations>();
        
        [Fact]
        public void BrightBlack() => 
            Output.Bright.Black("input")
                .Should()
                .Be("\u001b[30;1minput\u001b[0m");
        
        [Fact]
        public void BrightBlackUnderline() => 
            Output.Bright.Black().Underline().Text("input")
                .Should()
                .Be("\u001b[30;1m\u001b[4minput\u001b[0m");
        
        [Fact]
        public void UnderlineBrightBlack() => 
            Output
                .Underline().Bright.Black().Text("input")
                .Should()
                .Be("\u001b[4m\u001b[30;1minput\u001b[0m");
                
        [Fact]
        public void Null() => 
            Output
                .Underline().Bright.Black().Text(null)
                .Should()
                .Be("\u001b[4m\u001b[30;1m\u001b[0m");


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
            var methods = ColorMethods().ToList();
            
            colors
                .Should()
                .BeSubsetOf(methods.Select(m => m.Name));
            methods
                .ForEach(m => m.Invoke(null, Array.Empty<object>()));
        }
    }
}