using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using MediaManager.Playback;

namespace MediaManager.Logging
{
    internal class Logger : ILogger
    {
        public Logger()
        {
            Log($"Logging started using {ToString()}");
        }

        virtual public void Log(string message)
        {
            var methodFullName = GetMethodFullName();
            Debug.WriteLine($"{methodFullName}() {message}");
        }

        public void Log(object sender, PositionChangedEventArgs positionChangedEventArgs)
        {
            Log($"{positionChangedEventArgs}");
        }
        private string GetMethodFullName()
        {
            StackTrace stackTrace = new StackTrace();
            var frames = stackTrace.GetFrames();
            var frame =  frames?.FirstOrDefault(IsNotALoggingStackFrame);
            var method = frame?.GetMethod();
            var fullName = method == null ? String.Empty : $"{method.DeclaringType.Name}.{method.Name}";
            return fullName;
        }

        private static bool IsNotALoggingStackFrame(StackFrame stackFrame)
        {
            var isALoggerStackFrame = stackFrame
                .GetMethod()
                .DeclaringType
                .GetInterfaces()
                .Contains(typeof(ILogger));
            return !isALoggerStackFrame;
        }
    }
}
