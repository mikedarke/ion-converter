using System;
using System.Collections;
using Xunit;

namespace IonConverter.Tests
{
    class WithDateTime {
        public DateTime MyDate {get; set;}
    }    
    public class DateTimeTests
    {

        [Fact]
        public void ConvertsDateTimeToMatchingTimestamp()
        {
            var myDate = new DateTime(2020, 5, 10, 11, 23, 12);
            var a = new WithDateTime {
                MyDate = myDate
            };

            var builder = new IonConverter.IonDocumentBuilder();
            var doc = builder.BuildFrom<WithDateTime>(a);                                    
            Assert.Equal(
                myDate,
                doc.GetField("MyDate").TimestampValue.DateTimeValue
            );
        }                                
    }
}
