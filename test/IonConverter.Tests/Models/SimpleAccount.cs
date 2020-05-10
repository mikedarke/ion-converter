using System;

namespace IonConverter.Tests.Models {
    public class SimpleAccount {
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
}