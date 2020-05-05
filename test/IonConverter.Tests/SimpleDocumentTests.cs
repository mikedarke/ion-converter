using System;
using Xunit;

namespace IonConverter.Tests
{
    public class Account {
        public string AccountId {
            get;
            set;
        }

        public decimal Balance {
            get;
            set;
        }

        public Boolean IsActive {
            get;
            set;
        }
    }
    public class SimpleDocumentTests
    {
        [Fact]
        public void BuildsSimpleAccountDocument()
        {
            var a = new Account {
                AccountId = "ABC1",
                Balance = 12.0M,
                IsActive = true
            };

            var builder = new IonConverter.IonDocumentBuilder();
            var doc = builder.BuildFrom<Account>(a);
            Console.WriteLine("Constructed Document:");
            Console.Write(doc.ToPrettyString());
            Assert.Equal(a.AccountId, doc.GetField("AccountId").StringValue);
            Assert.Equal(a.Balance, doc.GetField("Balance").DecimalValue);
            Assert.Equal(a.IsActive, doc.GetField("IsActive").BoolValue);
        }
    }
}
