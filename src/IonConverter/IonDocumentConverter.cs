using System.Reflection;
using Amazon.IonDotnet.Tree;
using System.Collections;
using IonConverter.Exceptions;
using System;

namespace IonConverter {
    public class IonDocumentConverter : IIonDocumentConverter {
        public FieldHandlerRegistry FieldHandlers {get {return _fieldHandlers;}}
        readonly FieldHandlerRegistry _fieldHandlers;

        public IonDocumentConverter() {
            _fieldHandlers = new FieldHandlerRegistry();
        }

        public T ConvertTo<T>(IIonValue doc) where T : new() {
            var type = typeof(T);
            var handler = _fieldHandlers.GetHandler(type);
            if (handler == null) {
                throw new NoHandlerException(type);
            }

            var converted = handler.ConvertTo(doc, type);
            if (converted is T) {
                return (T) converted;
            }

            throw new Exception($"Failed to convert type {type.ToString()}");
        }

        public IIonValue ConvertFrom<T>(T model) {            
            Console.WriteLine($"Properties of {model.GetType().ToString()} are:");

            var handler = _fieldHandlers.GetHandler(typeof(T));
            return handler.ConvertFrom(model);
        }        
    }
}