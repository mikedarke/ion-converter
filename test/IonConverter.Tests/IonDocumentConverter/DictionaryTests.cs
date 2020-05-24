using Amazon.IonDotnet.Tree.Impl;
using Xunit;
using IonConverter.Tests.Models;
using Amazon.IonDotnet.Tree;

namespace IonConverter.Tests.FieldHandlers {

    public class DictionaryTests {
        
        [Fact]
        public void CorrectlyConvertsIonValueWithDictionaryIntString() {
            var factory = new ValueFactory();
            var doc = factory.NewEmptyStruct();
            var dict = factory.NewEmptyStruct();
            dict.SetField("1", factory.NewString("test"));
            dict.SetField("2", factory.NewString("test2"));            
            doc.SetField("MyDict", dict);
            var converter = new IonConverter.IonDocumentConverter();
            
            System.Console.WriteLine(doc.ToPrettyString());
            var converted = converter.ConvertTo<WithDictionaryOfIntString>(doc);

            Assert.NotNull(converted);
            Assert.Equal(2, converted.MyDict.Count);
            Assert.True(converted.MyDict.ContainsKey(1));  
            Assert.True(converted.MyDict.ContainsKey(2));            
        } 

        [Fact]
        public void CorrectlyConvertsIonValueWithDictionaryStringUser() {
            var factory = new ValueFactory();
            var doc = factory.NewEmptyStruct();
            var dict = factory.NewEmptyStruct();
            dict.SetField("test1", TestUtils.GetUserField(new User {
                Id = 102,
                Name = "Person 2"
            }));
            dict.SetField("test2", TestUtils.GetUserField(new User {
                Id = 102,
                Name = "Person 2"
            }));            
            doc.SetField("Users", dict);
            var converter = new IonConverter.IonDocumentConverter();
            
            System.Console.WriteLine(doc.ToPrettyString());
            var converted = converter.ConvertTo<WithDictionaryOfStringUser>(doc);

            Assert.NotNull(converted);
            Assert.Equal(2, converted.Users.Count);
            Assert.True(converted.Users.ContainsKey("test1"));  
            Assert.True(converted.Users.ContainsKey("test2"));            
        }                
    }
}