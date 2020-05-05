using System;
using System.Reflection;
using Amazon.IonDotnet.Tree;
using Amazon.IonDotnet.Tree.Impl;

namespace IonConverter {            
    public class IonDocumentBuilder {
        ValueFactory _factory;
        FieldHandlerRegister _fieldHandlers;
        FieldHandlerRegister FieldHandlers {get;}

        public IonDocumentBuilder() {
            _factory = new ValueFactory();
            _fieldHandlers = new FieldHandlerRegister();
        }

        public IIonValue BuildFrom<T>(T model) {            
            PropertyInfo[] myPropertyInfo;
            // Get the properties of 'Type' class object.
            myPropertyInfo = model.GetType().GetProperties();
            Console.WriteLine($"Properties of {model.GetType().ToString()} are:");

            var rootFunc = _fieldHandlers.GetByType(model.GetType());
            IIonValue root;
            if (rootFunc != null) {
                root = rootFunc(model);
            } else {
                root = _factory.NewEmptyStruct();
            }        

            foreach (var info in myPropertyInfo) {
                var func = _fieldHandlers.GetByType(info.PropertyType);
                if (func != null) {
                    var value = func(info.GetValue(model));
                    root.SetField(info.Name, value);                    
                }
            }

            return root;
        }
    }
}