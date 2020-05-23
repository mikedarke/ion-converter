using System;

namespace IonConverter.FieldHandlers {
    public class BaseHandler {
        protected Type[] _handledTypes;
        
        public FieldHandlerRegistry FieldHandlers {get; set;}

        protected bool _isScalar = true;
        public bool IsScalar {
            get {return _isScalar;}
        }
        public Type[] HandledTypes { get{
            return _handledTypes;
        }}
        public IonDocumentBuilder Builder {get; set;}

        public virtual Boolean IsHandledType(Type t) {
            return Array.IndexOf(_handledTypes, t) > -1;
        }
        
    }
}