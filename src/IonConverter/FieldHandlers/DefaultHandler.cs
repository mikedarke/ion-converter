using System;
using Amazon.IonDotnet.Tree;
using Amazon.IonDotnet.Tree.Impl;

namespace IonConverter.FieldHandlers {
    public class DefaultHandler : BaseHandler, IFieldHandler {

        public DefaultHandler() {
            _handledTypes = new Type[]{};
            _isScalar = false;
        }

        public IIonValue Convert(object value) {
            IIonValue root = Builder.Factory.NewEmptyStruct();
            Builder.BuildChildren(root, value);
            return root;
        }
    }
}