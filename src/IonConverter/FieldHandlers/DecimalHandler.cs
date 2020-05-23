using System;
using System.Reflection;
using Amazon.IonDotnet.Tree;
using Amazon.IonDotnet.Tree.Impl;

namespace IonConverter.FieldHandlers {
    public class DecimalHandler : BaseHandler, IFieldHandler {

        public DecimalHandler() {
            _handledTypes = new Type[]{typeof(System.Decimal)};
        }

        public IIonValue Convert(object value) {
            return Builder.Factory.NewDecimal((decimal) value);
        }

        public object ConvertTo(IIonValue value, Type type)
        {
            return value.DecimalValue;
        }
    }
}