using System;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Xunit;

namespace Crayon.Tests
{
    public class OutputChainTests
    {
        [Fact]
        public void Chaining()
        {
            Output.Green().Bold().Text("input").Should().Be("\u001b[32m\u001b[1minput\u001b[0m");
        }

        [Fact]
        public void OutputChainContainsMethodsForAllColors()
        {
            var colors = typeof(Colors).GetRuntimeFields().Select(s => s.Name);
            var methods = typeof(IOutput).GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(s => !s.GetParameters().Any()).Select(s => s.Name);

            colors.Should().BeSubsetOf(methods);
        }

        [Fact]
        public void OutputChainContainsMethodsForAllDecorations()
        {
            var decorations = typeof(Decorations).GetRuntimeFields().Select(s => s.Name);
            var methods = typeof(IOutput).GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(s => !s.GetParameters().Any()).Select(s => s.Name);

            decorations.Should().BeSubsetOf(methods);
        }
        
        [Fact]
        public void AllOutputInSpecifiedColor()
        {
            var colors = typeof(Colors)
                .GetRuntimeFields()
                .Select(x => new { x.Name, Value = x.GetRawConstantValue() });
            
            foreach (var c in colors)
            {
                var o = new OutputChain(99);
                o.GetType().GetMethod(c.Name).Invoke(o, new object[0]);
                o.Text("input").Should().Be($"\u001b[99m\u001b[{c.Value}minput\u001b[0m");
            }
        }
        
        [Fact]
        public void AllOutputInSpecifiedDecoration()
        {
            var decorations = typeof(Decorations)
                .GetRuntimeFields()
                .Select(x => new { x.Name, Value = x.GetRawConstantValue() });
            
            foreach (var c in decorations)
            {
                var o = new OutputChain(99);
                o.GetType().GetMethod(c.Name).Invoke(o, new object[0]);
                o.Text("input").Should().Be($"\u001b[99m\u001b[{c.Value}minput\u001b[0m");
            }
        }

        [Fact]
        public void NestedResetsReplaced()
        {
            Output.Green().Text($"something {Output.Red("red in the middle")} but green again").Should().Be("\u001b[32msomething \u001b[31mred in the middle\u001b[0m\u001b[32m but green again\u001b[0m");
        }
    }
}