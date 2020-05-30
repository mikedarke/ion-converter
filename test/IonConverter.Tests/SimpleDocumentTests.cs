using System;
using IonConverter.Tests.Models;
using Xunit;

namespace IonConverter.Tests
{
    public class SimpleDocumentTests
    {
        [Fact]
        public void BuildsSimpleAccountDocument()
        {
            var a = new SimpleAccount {
                AccountId = "ABC1",
                Balance = 12.0M,
                IsActive = true
            };

            var builder = new IonConverter.IonDocumentConverter();
            var doc = builder.ConvertFrom<SimpleAccount>(a);
            Console.WriteLine("Constructed Document:");
            Console.Write(doc.ToPrettyString());
            Assert.Equal(a.AccountId, doc.GetField("AccountId").StringValue);
            Assert.Equal(a.Balance, doc.GetField("Balance").DecimalValue);
            Assert.Equal(a.IsActive, doc.GetField("IsActive").BoolValue);
        }
    }
}
