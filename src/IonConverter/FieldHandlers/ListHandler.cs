using System;
using System.Collections;
using Amazon.IonDotnet.Tree;
using System.Linq;
using System.Reflection;

namespace IonConverter.FieldHandlers {
    public class ListHandler : BaseHandler, IFieldHandler {

        public ListHandler() : base() {
            _handledTypes = new Type[]{typeof(IList)};
            _isScalar = false;
        }

        public override Boolean IsHandledType(Type t) {
            return _handledTypes
                .Where(handledType => t.GetInterfaces()
                .Contains(handledType))
                .Count() > 0;
        }

        public IIonValue ConvertFrom(object value) {
            IIonValue list = Factory.NewEmptyList();
            var enumerableValue = (IEnumerable) value;
            BuildIonList(list, enumerableValue);
            return list;
        }

        public object ConvertTo(IIonValue value, Type type) {
            var listGenericArg = type.GetGenericArguments()[0];            
            var handler = FieldHandlers.GetHandler(listGenericArg);
            var list = (IList) Activator.CreateInstance(type);

            var enumerator = value.GetEnumerator();
            while(enumerator.MoveNext()) {
                list.Add(handler.ConvertTo(enumerator.Current, listGenericArg));                
            }

            return list;
        }          

        private void BuildIonList(IIonValue list, IEnumerable instance) {            
            foreach (var item in instance) {
                var itemType = item.GetType();
                var handler = FieldHandlers.GetHandler(itemType);
                IIonValue listItem = handler.ConvertFrom(item);
                list.Add(listItem);
            }
        }
    }
}