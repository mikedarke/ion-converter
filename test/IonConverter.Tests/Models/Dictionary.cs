using System.Collections.Generic;

namespace IonConverter.Tests.Models {
    public class WithDictionaryOfIntString {
        public Dictionary<int, string> MyDict  {get; set;}    
    }

    public class WithDictionaryOfStringUser {
        public Dictionary<string, User> Users  {get; set;}    
    }    
       
}