using System;
using System.Collections.Generic;
using Amazon.IonDotnet.Tree;
using System.Linq;
using Amazon.IonDotnet.Tree.Impl;
using IonConverter.FieldHandlers;

namespace IonConverter {
    public class FieldHandlerRegistry {
        List<IFieldHandler> _registry;
        IFieldHandler _defaultHandler;

        public FieldHandlerRegistry() {
            _registry = new List<IFieldHandler>();          
            RegisterAllHandlers();
        }     

        public void Add(IFieldHandler handler) {
            _registry.Add(handler);
        }

        public IFieldHandler GetHandler(Type type) {
            var scalarHandler = _registry
                .Where(h => h.IsHandledType(type) && h.IsScalar == true)
                .FirstOrDefault();

            if (scalarHandler != null) {
                return scalarHandler;             
            }

            var compositeHandler = _registry
                .Where(h => h.IsHandledType(type) && h.IsScalar == false)
                .FirstOrDefault();

            if (compositeHandler != null) {
                return compositeHandler;
            }

            return _defaultHandler;
        }

        void RegisterAllHandlers() {
            GetAllHandlers().ForEach(handler => Add(handler));
            _defaultHandler = new DefaultHandler();
            _defaultHandler.FieldHandlers = this;
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
                .Where(handler => handler.HandledTypes.Count() > 0)
                .ToList();
        }       
    }
}