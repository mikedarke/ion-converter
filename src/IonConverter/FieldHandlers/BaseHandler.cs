using System;
using Amazon.IonDotnet.Tree;
using Amazon.IonDotnet.Tree.Impl;

namespace IonConverter.FieldHandlers {
    public abstract class BaseHandler {
        protected Type[] _handledTypes;
        
        public FieldHandlerRegistry FieldHandlers {get; set;}

        public IValueFactory Factory;

        protected bool _isScalar = true;
        public bool IsScalar {
            get {return _isScalar;}
        }
        public Type[] HandledTypes { get{
            return _handledTypes;
        }}

        public BaseHandler() {
            Factory = new ValueFactory();
        }

        public virtual Boolean IsHandledType(Type t) {
            return Array.IndexOf(_handledTypes, t) > -1;
        }
        
    }
}