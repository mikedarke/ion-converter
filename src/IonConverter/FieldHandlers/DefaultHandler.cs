using System;
using System.Reflection;
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
            BuildChildren(root, value);
            return root;
        }

        public object ConvertTo(IIonValue value, Type type)
        {
            var props = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var instance = Activator.CreateInstance(type);

            foreach (var prop in props) {
                if (prop.CanWrite)  {
                    var field = value.GetField(prop.Name);
                    var fieldHandler = FieldHandlers.GetHandler(prop.GetType());
                    if (field != null) {
                        var fieldValue = fieldHandler.ConvertTo(field, prop.GetType());
                        type.GetProperty(prop.Name).SetValue(instance, fieldValue);
                    }
                }
            }

            return instance;
        }

        private void BuildChildren(IIonValue parent, object instance) {
            PropertyInfo[] propertyInfo = instance.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

            foreach (var info in propertyInfo) {
                var value = GetPropertyValue(info, instance);
                if (value != null) {
                    parent.SetField(info.Name, value);
                }                  
            }
        }

        private IIonValue GetPropertyValue(PropertyInfo info, object instance) {
            Type type = info.PropertyType;
            var handler = FieldHandlers.GetHandler(type);
            var value = handler.Convert(info.GetValue(instance));         

            return value;
        }        
    }
}