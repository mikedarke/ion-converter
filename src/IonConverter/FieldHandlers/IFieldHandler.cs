using System;
using Amazon.IonDotnet.Tree;
using Amazon.IonDotnet.Tree.Impl;

namespace IonConverter.FieldHandlers {
    public interface IFieldHandler {
        IonDocumentBuilder Builder {get; set;}
        Type[] HandledTypes {get;}
        IIonValue Convert(object value);

        Boolean IsScalar {get;}

        Boolean IsHandledType(Type t);
    }
}