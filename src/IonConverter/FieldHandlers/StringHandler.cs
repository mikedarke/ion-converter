using System;
using Amazon.IonDotnet.Tree;
using Amazon.IonDotnet.Tree.Impl;

namespace IonConverter.FieldHandlers {
    public class StringHandler : IFieldHandler {

        public Type GetHandledType() {
            return Type.GetType("System.String");
        }

        public Func<object, IIonValue> GetHandler(ValueFactory factory) {
            return (object value) => factory.NewString((string) value);
        }
    }
}