using System;
using System.Reflection;
using Amazon.IonDotnet.Tree;
using Amazon.IonDotnet.Tree.Impl;

namespace IonConverter {            
    public class IonDocumentBuilder {
        public FieldHandlerRegistry FieldHandlers {get {return _fieldHandlers;}}
        public ValueFactory Factory {get {return _factory;}}

        readonly ValueFactory _factory;
        readonly FieldHandlerRegistry _fieldHandlers;

        public IonDocumentBuilder() {
            _factory = new ValueFactory();
            _fieldHandlers = new FieldHandlerRegistry();
        }

        public IIonValue BuildFrom<T>(T model) {            
            Console.WriteLine($"Properties of {model.GetType().ToString()} are:");

            var handler = _fieldHandlers.GetHandler(typeof(T));
            return handler.ConvertFrom(model);
        }
    }
}