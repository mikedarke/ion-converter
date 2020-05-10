using System;
using System.Reflection;
using Amazon.IonDotnet.Tree;
using Amazon.IonDotnet.Tree.Impl;

namespace IonConverter {            
    public class IonDocumentBuilder {
        public FieldHandlerRegistry FieldHandlers {get {return _fieldHandlers;}}
        public ValueFactory Factory {get {return _factory;}}

        ValueFactory _factory;
        FieldHandlerRegistry _fieldHandlers;

        public IonDocumentBuilder() {
            _factory = new ValueFactory();
            _fieldHandlers = new FieldHandlerRegistry(this);
        }

        public IIonValue BuildFrom<T>(T model) {            
            Console.WriteLine($"Properties of {model.GetType().ToString()} are:");

            var handler = _fieldHandlers.GetHandler(typeof(T));
            var built = handler.Convert(model);

            return built;
        }

        public void BuildChildren(IIonValue parent, object instance) {

            Type instanceType = instance.GetType();
            PropertyInfo[] myPropertyInfo;
            myPropertyInfo = instance.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

            foreach (var info in myPropertyInfo) {
                var value = GetPropertyValue(info, instance);
                if (value != null) {
                    parent.SetField(info.Name, value);
                }                  
            }
        }

        private IIonValue GetPropertyValue(PropertyInfo info, object instance) {
            Type type = info.PropertyType;
            var handler = _fieldHandlers.GetHandler(type);
            var value = handler.Convert(info.GetValue(instance));         

            return value;
        }
    }
}