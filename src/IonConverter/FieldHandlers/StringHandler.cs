using System;
using Amazon.IonDotnet.Tree;
using Amazon.IonDotnet.Tree.Impl;

namespace IonConverter.FieldHandlers {
    public class StringHandler : BaseHandler, IFieldHandler {
        public StringHandler() {
            _handledTypes = new Type[]{
                typeof(System.String)
            };
        }

        public IIonValue Convert(object value) {
            return Builder.Factory.NewString((string) value);
        }
    }
}