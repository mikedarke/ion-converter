using Amazon.IonDotnet.Tree;
using Amazon.IonDotnet.Tree.Impl;
using IonConverter.Tests.Models;

namespace IonConverter.Tests {
    public class TestUtils {
        internal static IIonValue GetUserField(User user) {
            var factory = new ValueFactory();
            var userDoc = factory.NewEmptyStruct();
            userDoc.SetField("Id", factory.NewInt(user.Id));
            userDoc.SetField("Name", factory.NewString(user.Name));
            return userDoc;
        }

    }
}