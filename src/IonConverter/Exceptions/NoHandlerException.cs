using System;

namespace IonConverter.Exceptions {
    public class NoHandlerException : Exception {

        public NoHandlerException(Type t) : base(ModifyMessage($"Unable to find handler to convert {t.ToString()}", "")) {}

        private static string ModifyMessage(string message, string extraInfo)
        {
            return message.ToLowerInvariant() + Environment.NewLine + extraInfo;
        }
    }
}