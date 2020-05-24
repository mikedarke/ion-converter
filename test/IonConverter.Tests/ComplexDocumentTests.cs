using System;
using System.Collections.Generic;
using IonConverter.Tests.Models;
using Xunit;

namespace IonConverter.Tests
{
    public class ComplexDocumentTests
    {
        [Fact]
        public void BuildsComplexAccountDocument()
        {
            var createdAt = new DateTime(2020, 5, 10);
            var a = new ComplexAccount {
                AccountId = "ABC1",
                Balance = 12.0M,
                IsActive = true,
                CreatedAt = createdAt,
                AccountHolder = new User {
                    Id = 1,
                    Name = "Han Solo"
                },
                Transactions = new List<Transaction> {
                    new Transaction { Id = 123, Amount = 3.0M },
                    new Transaction { Id = 234, Amount = 21.0M }
                }
            };

            var builder = new IonConverter.IonDocumentBuilder();
            var doc = builder.BuildFrom<ComplexAccount>(a);
            Console.WriteLine("Constructed Document:");
            Console.Write(doc.ToPrettyString());
            Assert.Equal(a.AccountId, doc.GetField("AccountId").StringValue);
            Assert.Equal(a.Balance, doc.GetField("Balance").DecimalValue);
            Assert.Equal(a.IsActive, doc.GetField("IsActive").BoolValue);
            Assert.Equal(2, doc.GetField("Transactions").Count);            
            Assert.Equal(createdAt, doc.GetField("CreatedAt").TimestampValue.DateTimeValue);
        }
    }
}
