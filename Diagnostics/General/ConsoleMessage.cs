using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using Object = UnityEngine.Object;

namespace UTIRLib.Diagnostics
{
    public class ConsoleMessage
    {
        protected const string EmptyLogMessage = "Empty log";
        protected const int CallStackFramesDefaultOffset = 1;
        protected const int CallStackFramesOffsetConstructMethod = 2;
        protected const int CallStackFramesOffsetConstructor = 2;
        public static readonly ConsoleMessage Empty = new();

        protected StackTrace stackTrace;
        protected StackFrame stackFrame;
        protected string text;
        protected Type callingClassType;
        protected MethodBase callingMethod;

        public string Text => text;
        public Type CallingClassType => callingClassType;
        public MethodBase FirstCallingMethod => callingMethod;

        private ConsoleMessage() => text = "Is empty message.";
        protected ConsoleMessage(string message, int callStackFramesOffset, 
            params object[] formatArgs)
        {
            SetStackTrace(callStackFramesOffset + CallStackFramesDefaultOffset);
            SetCallingMethod();
            SetCallingClassType();

            if (formatArgs.IsNotNullOrEmpty()) {
                message = message.Format(true, formatArgs);
            }

            GenerateMessageText(message);
        }
        protected ConsoleMessage(string message, int callStackFramesOffset, Object context,
            params object[] formatArgs)
        {
            SetStackTrace(callStackFramesOffset + CallStackFramesDefaultOffset);
            SetCallingMethod();
            SetCallingClassType();

            if (formatArgs.IsNotNullOrEmpty()) {
                message = message.Format(true, formatArgs);
            }

            GenerateMessageText(message, context);
        }

        public override string ToString() => text;

        protected void AddDescription(string description) => text += ' ' + description;

        //protected void Construct(string message, int callStackFramesOffset, Object context = null)
        //{
        //    SetStackTrace(callStackFramesOffset + CallStackFramesDefaultOffset);
        //    SetCallingMethod();
        //    SetCallingClassType();

        //    GenerateMessageText(message, context);
        //}

        protected void SetStackTrace(int position)
        {
            stackTrace = new StackTrace();
            stackFrame = stackTrace.GetFrame(position);
        }

        protected void SetCallingMethod() => callingMethod = stackFrame.GetMethod();

        protected void SetCallingClassType() => callingClassType = callingMethod.DeclaringType;

        private string ConvertTypeToName(Type type)
        {
            if (type.IsGenericType) {
                return type.Name[..^2];
            }

            return type.Name;
        }

        private string GetCallingMethodName()
        {
            if (callingMethod.IsGenericMethod) {
                return callingMethod.Name[..^2];
            }

            return callingMethod.Name;
        }

        protected string GetCallingMethodParametersText()
        {
            if (callingMethod == null) {
                UnityEngine.Debug.LogError("Calling method not setted.");
                return string.Empty;
            }
            ParameterInfo[] callingMethodParameteres = callingMethod.GetParameters();
            if (callingMethodParameteres == null || callingMethodParameteres.Length == 0) {
                return string.Empty;
            }

            StringBuilder callingMethodParametersInfoMessage = new();
            int callingMethodParametersCount = callingMethodParameteres.Length;
            for (int i = 0; i < callingMethodParametersCount; i++) {
                if ((i + 1) < callingMethodParametersCount) {
                    callingMethodParametersInfoMessage.Append($"{callingMethodParameteres[i].ParameterType.Name}, "); 
                }
                else {
                    callingMethodParametersInfoMessage.Append($"{callingMethodParameteres[i].ParameterType.Name}");
                }
            }

            return callingMethodParametersInfoMessage.ToString();
        }

        protected void GenerateMessageText(string message, Object context = null)
        {
            text = $"{(context != null ? ConvertContextToText(context) : string.Empty)}" +
                $"{ConvertTypeToName(GetType())}: " +
                $"{ConvertTypeToName(callingClassType)}." +
                $"{GetCallingMethodName()}" +
                $"({GetCallingMethodParametersText()})." +
                $" {message}";
        }

        protected string ConvertContextToText(Object context) => $"Context: {context.name}";
    }
}
