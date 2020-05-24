using System;
using System.Reflection;
using Amazon.IonDotnet.Tree;
using Amazon.IonDotnet.Tree.Impl;

namespace IonConverter.FieldHandlers {
    public class DecimalHandler : BaseHandler, IFieldHandler {

        public DecimalHandler() : base() {
            _handledTypes = new Type[]{typeof(System.Decimal)};
        }

        public IIonValue ConvertFrom(object value) {
            return Factory.NewDecimal((decimal) value);
        }

        public object ConvertTo(IIonValue value, Type type)
        {
            return value.DecimalValue;
        }
    }
}