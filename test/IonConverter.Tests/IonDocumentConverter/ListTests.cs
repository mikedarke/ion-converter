using Amazon.IonDotnet.Tree.Impl;
using Xunit;
using IonConverter.Tests.Models;
using Amazon.IonDotnet.Tree;

namespace IonConverter.Tests.IonDocumentConverter {

    public class ListTests {
        
        [Fact]
        public void CorrectlyConvertsIonValueWithListOfInt() {
            var factory = new ValueFactory();
            var doc = factory.NewEmptyStruct();
            var list = factory.NewEmptyList();
            list.Add(factory.NewInt(20));
            list.Add(factory.NewInt(30));
            list.Add(factory.NewInt(40));
            doc.SetField("MyList", list);
            var converter = new IonConverter.IonDocumentConverter();
            
            System.Console.WriteLine(doc.ToPrettyString());
            var converted = converter.ConvertTo<WithListOfInts>(doc);

            Assert.NotNull(converted);
            Assert.Equal(3, converted.MyList.Count);
            Assert.Equal(20, converted.MyList[0]);
            Assert.Equal(30, converted.MyList[1]);
            Assert.Equal(40, converted.MyList[2]);
        }

        [Fact]
        public void CorrectlyConvertsIonValueWithListOfUsers() {
            var factory = new ValueFactory();
            var doc = factory.NewEmptyStruct();
            var list = factory.NewEmptyList();
            list.Add(TestUtils.GetUserField(new User {
                Id = 101,
                Name = "Person 1"
            }));
            list.Add(TestUtils.GetUserField(new User {
                Id = 102,
                Name = "Person 2"
            }));            
            doc.SetField("UserList", list);
            var converter = new IonConverter.IonDocumentConverter();
            
            System.Console.WriteLine(doc.ToPrettyString());
            var converted = converter.ConvertTo<WithListOfUsers>(doc);

            Assert.NotNull(converted);
            Assert.Equal(2, converted.UserList.Count);
            Assert.Equal(101, converted.UserList[0].Id);
            Assert.Equal(102, converted.UserList[1].Id);
        }        
    }
}