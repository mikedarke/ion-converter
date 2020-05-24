using System;
using System.Reflection;
using Amazon.IonDotnet.Tree;
using Amazon.IonDotnet.Tree.Impl;

namespace IonConverter.FieldHandlers {
    public class BooleanHandler : BaseHandler, IFieldHandler {

        public BooleanHandler() : base() {
            _handledTypes = new Type[]{Type.GetType("System.Boolean")};
        }

        public IIonValue ConvertFrom(object value) {
            return Factory.NewBool((bool) value);
        }

        public object ConvertTo(IIonValue value, Type type)
        {
            return value.BoolValue;
        }
    }
}