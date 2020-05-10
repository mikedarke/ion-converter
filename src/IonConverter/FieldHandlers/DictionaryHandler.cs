using System;
using System.Collections;
using Amazon.IonDotnet.Tree;
using System.Linq;

namespace IonConverter.FieldHandlers {
    public class DictionaryHandler : BaseHandler, IFieldHandler {

        public DictionaryHandler() {
            _handledTypes = new Type[]{typeof(IDictionary)};
            _isScalar = false;
        }

        public override Boolean IsHandledType(Type t) {
            return _handledTypes
                .Where(handledType => t.GetInterfaces()
                .Contains(handledType))
                .Count() > 0;
        }

        public IIonValue Convert(object value) {
            IIonValue dict = Builder.Factory.NewEmptyStruct();
                     
            var enumerableValue = (IDictionary) value;
            BuildDictionary(dict, enumerableValue);
            return dict;
        }

        private void BuildDictionary(IIonValue dict, IDictionary instance) {
            var keyType = instance.Keys.GetType();
            Type[] arguments = instance.GetType().GetGenericArguments();
            var valueType = arguments[1];           

            foreach (var key in instance.Keys) {                                
                var handler = Builder.FieldHandlers.GetHandler(valueType);
                var item = instance[key];
                IIonValue value = handler.Convert(item);
                dict.SetField(key.ToString(), value);
            }
        }        
    }
}