using Xunit;
using IonConverter.Tests.Models;
using Amazon.IonDotnet.Tree;
using Amazon.IonDotnet.Tree.Impl;

namespace IonConverter.Tests.IonDocumentConverter {

    public class ConvertSimpleModels {

        private IIonValue CreateSimpleDoc(string accountId, decimal balance, bool isActive) {
            var factory = new ValueFactory();
            var doc = factory.NewEmptyStruct();
            doc.SetField("AccountId", factory.NewString(accountId));
            doc.SetField("Balance", factory.NewDecimal(balance));
            doc.SetField("IsActive", factory.NewBool(isActive));

            return doc;
        }


        [Fact]
        public void BuildsSimpleAccountDocument()
        {
            var doc = CreateSimpleDoc("ABC1", 12.0M, true);
            var converter = new IonConverter.IonDocumentConverter();
            var account = converter.ConvertTo<SimpleAccount>(doc);

            Assert.Equal("ABC1", account.AccountId);
            Assert.Equal(12.0M, account.Balance);
            Assert.Equal(true, account.IsActive);
        }        
    }

}