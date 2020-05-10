using System;
using System.Collections.Generic;

namespace IonConverter.Tests.Models {
    public class ComplexAccount {
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

        public DateTime CreatedAt {get; set;}

        public List<Transaction> Transactions {get; set;}

        public User AccountHolder {get; set;}
    }
}