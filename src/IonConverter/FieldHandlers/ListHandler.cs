using System;
using System.Collections;
using Amazon.IonDotnet.Tree;
using System.Linq;

namespace IonConverter.FieldHandlers {
    public class ListHandler : BaseHandler, IFieldHandler {

        public ListHandler() {
            _handledTypes = new Type[]{typeof(IList)};
            _isScalar = false;
        }

        public override Boolean IsHandledType(Type t) {
            return _handledTypes
                .Where(handledType => t.GetInterfaces()
                .Contains(handledType))
                .Count() > 0;
        }

        public IIonValue Convert(object value) {
            IIonValue list = Builder.Factory.NewEmptyList();
            var enumerableValue = (IEnumerable) value;
            BuildList(list, enumerableValue);
            return list;
        }

        private void BuildList(IIonValue list, IEnumerable instance) {            
            foreach (var item in instance) {
                var itemType = item.GetType();
                var handler = Builder.FieldHandlers.GetHandler(itemType);
                IIonValue listItem = handler.Convert(item);
                list.Add(listItem);
            }
        }
    }
}