using System;
using System.Collections;
using System.Collections.Generic;
using Amazon.IonDotnet.Tree;
using Amazon.IonDotnet.Tree.Impl;

namespace IonConverter.FieldHandlers {
    public class ListHandler : BaseHandler, IFieldHandler {

        public ListHandler() {
            _handledTypes = new Type[]{typeof(IEnumerable)};
            _isScalar = false;
        }

        public IIonValue Convert(object value) {
            IIonValue root = Builder.Factory.NewEmptyList();
            var enumerableValue = (IEnumerable) value;
            Builder.BuildList(root, enumerableValue);
            return root;
        }
    }
}