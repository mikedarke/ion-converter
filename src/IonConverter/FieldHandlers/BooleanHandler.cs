using System;
using System.Reflection;
using Amazon.IonDotnet.Tree;
using Amazon.IonDotnet.Tree.Impl;

namespace IonConverter.FieldHandlers {
    public class BooleanHandler : BaseHandler, IFieldHandler {

        public BooleanHandler() {
            _handledTypes = new Type[]{Type.GetType("System.Boolean")};
        }

        public IIonValue Convert(object value) {
            return Builder.Factory.NewBool((bool) value);
        }

        public object ConvertTo(IIonValue value, Type type)
        {
            return value.BoolValue;
        }
    }
}