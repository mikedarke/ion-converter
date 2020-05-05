using System;
using Amazon.IonDotnet.Tree;
using Amazon.IonDotnet.Tree.Impl;

namespace IonConverter.FieldHandlers {
    public class BooleanHandler : IFieldHandler {
        public Type GetHandledType() {
            return Type.GetType("System.Boolean");
        }

        public Func<object, IIonValue> GetHandler(ValueFactory factory) {
            return (object value) => factory.NewBool((bool) value);
        }
    }
}