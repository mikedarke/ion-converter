using System;
using System.Collections;
using Amazon.IonDotnet.Tree;
using System.Linq;
using System.Reflection;

namespace IonConverter.FieldHandlers {
    public class ListHandler : BaseHandler, IFieldHandler {

        public ListHandler() {
            _handledTypes = new Type[]{typeof(IList)};
            _isScalar = false;
        }

        public override Boolean IsHandledType(Type t) {
            if (t.GetGenericArguments().Count() == 0) {
                return false;
            }

            return _handledTypes
                .Where(handledType => t.GetInterfaces()
                .Contains(handledType))
                .Count() > 0;
        }

        public IIonValue Convert(object value) {
            IIonValue list = Builder.Factory.NewEmptyList();
            var enumerableValue = (IEnumerable) value;
            BuildIonList(list, enumerableValue);
            return list;
        }

        public object ConvertTo(IIonValue value, Type type) {
            var listGenericArg = type.GetGenericArguments()[0];            
            var handler = Builder.FieldHandlers.GetHandler(listGenericArg);
            var genericListType = type.MakeGenericType(listGenericArg);
            var list = (IList) Activator.CreateInstance(genericListType);

            var enumerator = value.GetEnumerator();
            while(enumerator.MoveNext()) {
                list.Add(handler.ConvertTo(enumerator.Current, listGenericArg.GetType()));                
            }

            return list;
        }          

        private void BuildIonList(IIonValue list, IEnumerable instance) {            
            foreach (var item in instance) {
                var itemType = item.GetType();
                var handler = Builder.FieldHandlers.GetHandler(itemType);
                IIonValue listItem = handler.Convert(item);
                list.Add(listItem);
            }
        }
    }
}