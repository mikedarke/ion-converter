using System;
using System.Collections.Generic;
using Amazon.IonDotnet.Tree;
using System.Linq;
using Amazon.IonDotnet.Tree.Impl;

namespace IonConverter {
    public class FieldHandlerRegister {
        Dictionary<Type, Func<object, IIonValue>> _registry;

        public FieldHandlerRegister() {
            _registry = new Dictionary<Type, Func<object, IIonValue>>();
            RegisterAllHandlers();
        }      

        public void Add(Type type, Func<object, IIonValue> converter) {
            _registry.Add(type, converter);
        }

        public Func<object, IIonValue> GetByType(Type type) {
            return _registry.GetValueOrDefault(type);
        }

        void RegisterAllHandlers() {
            var factory = new ValueFactory();
            GetAllHandlers().ForEach(handler => {
                Add(handler.GetHandledType(), handler.GetHandler(factory));
            });
        }         

        List<IFieldHandler> GetAllHandlers()
        {
           return AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(IFieldHandler).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(x => {
                    Console.WriteLine($"IFieldHandler: {x.FullName}");
                    var instance = (IFieldHandler) x.GetConstructors().First().Invoke(null);
                    return instance;
                })
                .ToList();
        }         
    }
}