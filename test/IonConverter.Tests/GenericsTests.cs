using System;
using System.Collections;
using System.Collections.Generic;
using Amazon.IonDotnet.Tree.Impl;
using IonConverter.Tests.Models;
using Xunit;

namespace IonConverter.Tests
{
    class WithGenericList {
        public List<string> MyList {get; set;}
    }

    class WithStringDictionary {
        public Dictionary<string, string> MyDict {get; set;}
    }

    class WithDecimalDictionary {
        public Dictionary<decimal, int> MyDict {get; set;}
    }        
   
    public class GenericTests
    {
        [Fact]
        public void CanConvertGenericLists()
        {
            var a = new WithGenericList {
                MyList = new List<string> {
                    "Item 1",
                    "Item 2",
                    "Item 3",
                    "Item 4"
                }
            };

            var builder = new IonConverter.IonDocumentBuilder();
            var doc = builder.BuildFrom<WithGenericList>(a);
            var listField = doc.GetField("MyList");
            Assert.Equal(4, listField.Count);
            var firstElement = listField.GetElementAt(0);
            Assert.Equal("Item 1", firstElement.StringValue);
        }

        [Fact]
        public void CanConvertStringDictionary()
        {
            var a = new WithStringDictionary {
                MyDict = new Dictionary<string, string> {
                    {"test1", "Item 1"},
                    {"test2", "Item 2"},
                    {"test3", "Item 3"},
                    {"test4", "Item 4"}
                }
            };

            var builder = new IonConverter.IonDocumentBuilder();
            var doc = builder.BuildFrom<WithStringDictionary>(a);
            var listField = doc.GetField("MyDict");            
            var itemField = listField.GetField("test2");

            Assert.Equal(4, listField.Count);
            Assert.Equal("Item 2", itemField.StringValue);
        }

        [Fact]
        public void CanConvertDecimalKeyDictionary()
        {
            var a = new WithDecimalDictionary {
                MyDict = new Dictionary<decimal, int> {
                    {1.0M, 1002},
                    {123.3M, 1001},
                    {3423.543M, 1004},
                    {52.983M, 1003}
                }
            };

            var builder = new IonConverter.IonDocumentBuilder();
            var doc = builder.BuildFrom<WithDecimalDictionary>(a);
            var listField = doc.GetField("MyDict");
            var itemField = listField.GetField(3423.543M.ToString());
            Assert.Equal(1004, itemField.BigIntegerValue);
        }                
      
    }
}
