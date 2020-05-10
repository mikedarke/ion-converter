using System;
using System.Collections;
using Xunit;

namespace IonConverter.Tests
{
    class WithIntArray {
        public int[] MyNumbers {get; set;}
    }  

    class WithArrayList {
        public ArrayList MyList {get; set;}
    }    
    public class ArrayTests
    {

        [Fact]
        public void CanConvertArrayLists()
        {
            var a = new WithArrayList {
                MyList = new ArrayList {
                    1, 2, 3, 4, 5, 6, 7, 8
                }
            };

            var builder = new IonConverter.IonDocumentBuilder();
            var doc = builder.BuildFrom<WithArrayList>(a);
            Console.WriteLine("Constructed Document:");
            Console.Write(doc.ToPrettyString());
            var listField = doc.GetField("MyList");
            Assert.Equal(8, listField.Count);
            var firstElement = listField.GetElementAt(4);
            Assert.Equal(5, firstElement.BigIntegerValue);
        } 

        [Fact]
        public void CanConvertIntArray()
        {
            var a = new WithIntArray {
                MyNumbers = new int[] {
                    1, 2, 3, 4, 5, 6, 7, 8, 9, 10
                }
            };

            var builder = new IonConverter.IonDocumentBuilder();
            var doc = builder.BuildFrom<WithIntArray>(a);
            Console.WriteLine("Constructed Document:");
            Console.Write(doc.ToPrettyString());
            var listField = doc.GetField("MyNumbers");
            Assert.Equal(10, listField.Count);
            var element = listField.GetElementAt(8);
            Assert.Equal(9, element.BigIntegerValue);
        } 

        [Fact]
        public void CanConvertEmptyArray()
        {
            var a = new WithIntArray {
                MyNumbers = new int[] {}
            };

            var builder = new IonConverter.IonDocumentBuilder();
            var doc = builder.BuildFrom<WithIntArray>(a);
            Console.WriteLine("Constructed Document:");
            Console.Write(doc.ToPrettyString());
            var listField = doc.GetField("MyNumbers");
            Assert.Equal(0, listField.Count);
        }                        
    }
}
