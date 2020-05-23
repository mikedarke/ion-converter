using System;
using System.Reflection;
using Amazon.IonDotnet.Tree;
using Amazon.IonDotnet.Tree.Impl;

namespace IonConverter.FieldHandlers {
    public class IntHandler : BaseHandler, IFieldHandler {

        public IntHandler() {
            _handledTypes = new Type[]{
                typeof(System.Int16),
                typeof(System.Int32),
                typeof(System.Int64),
            };
        }

        public IIonValue Convert(object value) {
            return Builder.Factory.NewInt((int) value);
        }

        public object ConvertTo(IIonValue value, Type type) {
            return value.IntValue;
        }        
    }
}