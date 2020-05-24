using IonConverter.FieldHandlers;
using Xunit;

namespace IonConverter.Tests {
    internal class Simple {
        public string Name {get; set;}
    }

    public class FieldHandlerRegistryTests {
        [Fact]
        public void PicksStringHandler() {
            var fieldHandlerRegistry = new FieldHandlerRegistry();
            
            var handler = fieldHandlerRegistry.GetHandler(typeof(string));

            Assert.True(handler is StringHandler);
        }

        [Fact]
        public void PicksDecimalHandler() {
            var fieldHandlerRegistry = new FieldHandlerRegistry();
            
            var handler = fieldHandlerRegistry.GetHandler(typeof(decimal));

            Assert.True(handler is DecimalHandler);
        }

        [Fact]
        public void PicksDefaultHandler() {
            var fieldHandlerRegistry = new FieldHandlerRegistry();
            
            var handler = fieldHandlerRegistry.GetHandler(typeof(Simple));

            Assert.True(handler is DefaultHandler);
        }                
    }
}