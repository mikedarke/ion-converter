using Xunit;
using IonConverter.Tests.Models;
using Amazon.IonDotnet.Tree;
using Amazon.IonDotnet.Tree.Impl;
using System;
using Amazon.IonDotnet;
using System.Collections.Generic;

namespace IonConverter.Tests.IonDocumentConverter {

    public class ConvertComplexModelTests {

        private IIonValue CreateComplexDoc(ComplexAccount model) {
            var factory = new ValueFactory();
            var doc = factory.NewEmptyStruct();
            doc.SetField("AccountId", factory.NewString(model.AccountId));
            doc.SetField("Balance", factory.NewDecimal(model.Balance));
            doc.SetField("IsActive", factory.NewBool(model.IsActive));

            doc.SetField("CreatedAt", factory.NewTimestamp(new Timestamp(model.CreatedAt)));

            var list = factory.NewEmptyList();
            model.Transactions.ForEach(t => {
                var it = CreateTransaction(t, factory);
                list.Add(it);
            });
            doc.SetField("Transactions", list);

            return doc;
        }

        private IIonValue CreateTransaction(Transaction t, ValueFactory factory) {
            var it = factory.NewEmptyStruct();
            it.SetField("Id", factory.NewInt(t.Id));
            it.SetField("Amount", factory.NewDecimal(t.Amount));

            return it;
        }


        [Fact]
        public void BuildsComplexAccountDocument()
        {
            var account = new ComplexAccount {
                AccountId = "ABC1",
                Balance = 12.0M,
                IsActive = true,
                Transactions = new List<Transaction>{
                    new Transaction { Id = 1, Amount = 10.0M },
                    new Transaction { Id = 2, Amount = 21.0M },
                }
            };
            var doc = CreateComplexDoc(account);
            var converter = new IonConverter.IonDocumentConverter();
            var convertedAccount = converter.ConvertTo<ComplexAccount>(doc);

            Assert.Equal(12.0M, convertedAccount.Balance);
            Assert.Equal(true, convertedAccount.IsActive);
            Assert.Equal(2, convertedAccount.Transactions.Count);
            Assert.Equal(21.0M, convertedAccount.Transactions[1].Amount);
        }        
    }

}