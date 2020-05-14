using System.Reflection;
using Amazon.IonDotnet.Tree;
using System.Linq;
using System;

namespace IonConverter {
    public class IonDocumentConverter {
        public T ConvertTo<T>(IIonValue doc) where T : new() {
            var type = typeof(T);
            var props = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var instance = new T();

            foreach (var prop in props) {
                if (prop.CanWrite)  {
                    var field = doc.GetField(prop.Name);
                    if (field != null) {
                        ConvertField<T>(field, prop, instance);
                    }
                }
            }

            return instance;
        }

        private void ConvertField<T>(IIonValue field, PropertyInfo propInfo, T instance) {
            var propType = propInfo.PropertyType;

            if (propType == typeof(int)) {
                propInfo.SetValue(instance, field.BigIntegerValue);
                return;
            }

            if (propType == typeof(decimal)) {
                propInfo.SetValue(instance, field.DecimalValue);
                return;
            }

            if (propType == typeof(double)) {
                propInfo.SetValue(instance, field.DoubleValue);
                return;
            } 

            if (propType == typeof(string)) {
                propInfo.SetValue(instance, field.StringValue);
                return;
            } 

            if (propType == typeof(bool)) {
                propInfo.SetValue(instance, field.BoolValue);
                return;
            }                                                           
        }
    }
}