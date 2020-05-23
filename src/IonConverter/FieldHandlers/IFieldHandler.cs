using System;
using System.Reflection;
using Amazon.IonDotnet.Tree;
using Amazon.IonDotnet.Tree.Impl;

namespace IonConverter.FieldHandlers {
    public interface IFieldHandler {
        FieldHandlerRegistry FieldHandlers {get; set;}
        Type[] HandledTypes {get;}
        IIonValue Convert(object value);

        object ConvertTo(IIonValue value, Type type);

        Boolean IsScalar {get;}

        Boolean IsHandledType(Type t);
    }
}