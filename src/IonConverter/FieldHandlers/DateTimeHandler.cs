using System;
using System.Reflection;
using Amazon.IonDotnet;
using Amazon.IonDotnet.Tree;
using Amazon.IonDotnet.Tree.Impl;

namespace IonConverter.FieldHandlers {
    public class DateTimeHandler : BaseHandler, IFieldHandler {

        public DateTimeHandler() : base() {
            _handledTypes = new Type[]{typeof(System.DateTime)};
        }

        public IIonValue ConvertFrom(object value) {
            var t = new Timestamp((DateTime) value);
            return Factory.NewTimestamp(t);
        }

        public object ConvertTo(IIonValue value, Type type)
        {
            return value.TimestampValue.DateTimeValue;
        }
    }
}