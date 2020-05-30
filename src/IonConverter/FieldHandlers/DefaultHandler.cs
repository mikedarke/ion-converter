using System;
using System.Reflection;
using System.Linq;
using Amazon.IonDotnet.Tree;
using IonConverter.Extensions;

namespace IonConverter.FieldHandlers {
    public class DefaultHandler : BaseHandler, IFieldHandler {   

        public DefaultHandler() : base() {
            _handledTypes = new Type[]{};
            _isScalar = false;
        }

        public IIonValue ConvertFrom(object value) {
            IIonValue root = Factory.NewEmptyStruct();
            BuildChildren(root, value);
            return root;
        }

        public object ConvertTo(IIonValue value, Type type)
        {
            var props = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);            
            var instance = Activator.CreateInstance(type);

            var members = type.GetMembers(BindingFlags.Public | BindingFlags.Instance)
                .Where(m => (m.MemberType == MemberTypes.Field || m.MemberType == MemberTypes.Property) );
            foreach (MemberInfo member in members) {                            
                var field = value.GetField(member.Name);
                var memberType = member.GetUnderlyingType();
                var fieldHandler = FieldHandlers.GetHandler(memberType);
                if (field != null) {
                    var fieldValue = fieldHandler.ConvertTo(field, memberType);
                    if (member.MemberType == MemberTypes.Field) {
                        type.GetField(member.Name).SetValue(instance, fieldValue);
                        continue;
                    } 

                    if (member.MemberType == MemberTypes.Property) {
                        if (((PropertyInfo) member).CanWrite) {
                            type.GetProperty(member.Name).SetValue(instance, fieldValue);
                        }                        
                    }
                }
            }

            return instance;
        }

        private void BuildChildren(IIonValue parent, object instance) {
            PropertyInfo[] propertyInfo = instance.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

            foreach (var info in propertyInfo) {
                var value = GetPropertyValue(info, instance);
                if (value != null) {
                    parent.SetField(info.Name, value);
                }                  
            }
        }

        private IIonValue GetPropertyValue(PropertyInfo info, object instance) {
            Type type = info.PropertyType;
            var handler = FieldHandlers.GetHandler(type);
            var instanceValue = info.GetValue(instance);
            if (instanceValue == null) {
                return null;
            }
            var value = handler.ConvertFrom(instanceValue);         

            return value;
        }        
    }
}