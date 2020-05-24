using System;
using System.Collections;
using Amazon.IonDotnet.Tree;
using System.Linq;
using System.Reflection;

namespace IonConverter.FieldHandlers {
    public class DictionaryHandler : BaseHandler, IFieldHandler {

        public DictionaryHandler() : base() {
            _handledTypes = new Type[]{typeof(IDictionary)};
            _isScalar = false;
        }

        public override Boolean IsHandledType(Type t) {
            // if (t.GetType().GetGenericArguments().Count() < 2) {
            //     return false;
            // }

            return _handledTypes
                .Where(handledType => t.GetInterfaces()
                .Contains(handledType))
                .Count() > 0;
        }

        public IIonValue ConvertFrom(object value) {
            IIonValue dict = Factory.NewEmptyStruct();
                     
            var enumerableValue = (IDictionary) value;
            BuildDictionary(dict, enumerableValue);
            return dict;
        }

        private void BuildDictionary(IIonValue dict, IDictionary instance) {
            var keyType = instance.Keys.GetType();
            var valueType = instance.Values.GetType();          

            foreach (var key in instance.Keys) {                                
                var handler = FieldHandlers.GetHandler(valueType);
                var item = instance[key];
                IIonValue value = handler.ConvertFrom(item);
                dict.SetField(key.ToString(), value);
            }
        }

        public object ConvertTo(IIonValue value, Type type)
        {
            var genericArgs = type.GetGenericArguments();
            var keyGenericArg = genericArgs[0];
            var valueGenericArg = genericArgs[1];
            var valueHandler = FieldHandlers.GetHandler(valueGenericArg);
            var dict = (IDictionary) Activator.CreateInstance(type);

            var enumerator = value.GetEnumerator();
            while(enumerator.MoveNext()) {
                var itemField = enumerator.Current;
                var key = System.Convert.ChangeType(itemField.FieldNameSymbol.Text, keyGenericArg);
                var itemValue = valueHandler.ConvertTo(enumerator.Current, valueGenericArg);
                dict.Add(key, itemValue);                
            }

            return dict;
        }
    }
}