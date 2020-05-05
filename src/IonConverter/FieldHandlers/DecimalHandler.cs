using System;
using Amazon.IonDotnet.Tree;
using Amazon.IonDotnet.Tree.Impl;

namespace IonConverter.FieldHandlers {
    public class DecimalHandler : IFieldHandler {
        public Type GetHandledType() {
            return Type.GetType("System.Decimal");
        }

        public Func<object, IIonValue> GetHandler(ValueFactory factory) {
            return (object value) => factory.NewDecimal((decimal) value);
        }
    }
}