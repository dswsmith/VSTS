using System;
using System.Diagnostics;

namespace FortressSageExporter
{
    class Logger
    {
        private const string eventSource = "Fortress Sage Exporter";
        private const string eventLog = "Application";

        //public enum LogType
        //{
        //    Trace,
        //    Information,
        //    Warning,
        //    Error
        //}

        public Logger()
        {
            if (!EventLog.SourceExists(eventSource))
                EventLog.CreateEventSource(eventSource, eventLog);
        }

        public void Log(string message, EventLogEntryType type)
        {
            EventLog.WriteEntry(eventSource, message, type, 1);

            message = DateTime.Now.ToString("dd/MM/yyyy hh:mm") + " [" + type.ToString() + "] " + message;

            Console.WriteLine(message);
        }
    }
}
