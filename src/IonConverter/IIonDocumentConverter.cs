using Amazon.IonDotnet.Tree;

namespace IonConverter {
    public interface IIonDocumentConverter {
        IIonValue ConvertFrom<T>(T source);
        T ConvertTo<T>(IIonValue doc) where T : new();
    }
}