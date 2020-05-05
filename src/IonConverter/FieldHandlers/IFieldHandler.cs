using System;
using Amazon.IonDotnet.Tree;
using Amazon.IonDotnet.Tree.Impl;

namespace IonConverter.FieldHandlers {
    interface IFieldHandler {        
        Func<object, IIonValue> GetHandler(ValueFactory factory);

        Type GetHandledType();
    }
}