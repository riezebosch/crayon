using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Xunit;

namespace Crayon.Tests
{
    public class OutputChainTests
    {
        public OutputChainTests() => 
            Output.Enable();

        [Fact]
        public static void Chaining() => Output
            .Green()
            .Bold()
            .Text("input")
            .Should()
            .Be("\u001b[32m\u001b[1minput\u001b[0m");

        [Fact]
        public void OutputChainContainsMethodsForAllColors()
        {
            var colors = ConstantsNames<Colors>();
            var methods = ColorMethods();

            colors.Should().BeSubsetOf(methods);
        }

        [Fact]
        public void OutputChainContainsMethodsForAllDecorations()
        {
            var decorations = ConstantsNames<Decorations>();
            var methods = ColorMethods();

            decorations.Should().BeSubsetOf(methods);
        }

        [Fact]
        public void AllOutputInSpecifiedColor()
        {
            var colors = ConstantsFrom<Colors>();
            foreach (var (name, value) in colors)
            {
                var o = new OutputChain(99);
                o.GetType().GetMethod(name).Invoke(o, new object[0]);
                o.Text("input").Should().Be($"\u001b[99m\u001b[{value}minput\u001b[0m");
            }
        }


        [Fact]
        public void AllOutputInSpecifiedDecoration()
        {
            var decorations = ConstantsFrom<Decorations>();
            foreach (var (name, value) in decorations)
            {
                var o = new OutputChain(99);
                o.GetType().GetMethod(name).Invoke(o, new object[0]);
                o.Text("input").Should().Be($"\u001b[99m\u001b[{value}minput\u001b[0m");
            }
        }

        [Fact]
        public void NestedResetsReplaced() => 
            Output
                .Green()
                .Text($"something {Output.Red("red in the middle")} but green again")
                .Should()
                .Be("\u001b[32msomething \u001b[31mred in the middle\u001b[0m\u001b[32m but green again\u001b[0m");

        [Fact]
        public void FromRgb() => 
            Output
                .FromRgb(55, 115, 155).
                Text("from rgb!")
                .Should()
                .Be("\u001b[38;2;55;115;155mfrom rgb!\u001b[0m");

        [Fact]
        public void BoldFromRgb() => Output.Bold().FromRgb(55, 115, 155).Text("from rgb!").Should().Be("\u001b[1m\u001b[38;2;55;115;155mfrom rgb!\u001b[0m");
        
        private static IEnumerable<string> ColorMethods() =>
            typeof(IOutput)
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(s => !s.GetParameters().Any()).Select(s => s.Name);

        private static IEnumerable<string> ConstantsNames<T>() =>
            ConstantsFrom<T>().Select(x => x.name);
        
        private static IEnumerable<(string name, object value)> ConstantsFrom<T>() =>
            typeof(T)
                .GetRuntimeFields()
                .Select(x => (x.Name, x.GetRawConstantValue()));
    }
}