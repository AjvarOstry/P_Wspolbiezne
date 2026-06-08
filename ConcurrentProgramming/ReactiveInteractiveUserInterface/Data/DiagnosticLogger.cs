using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;

namespace TP.ConcurrentProgramming.Data
{
    public class DiagnosticLogger : IDisposable
    {
        private readonly BlockingCollection<string> _logQueue = new BlockingCollection<string>(1000); 
        private int _droppedLogs = 0;
        private readonly StreamWriter _writer;

        public DiagnosticLogger(string filePath)
        {
            _writer = new StreamWriter(filePath, append: false) { AutoFlush = true };
            Task.Run(ProcessLogs); 
        }

        public void Log(string message)
        {
            if (!_logQueue.TryAdd($"{DateTime.UtcNow:HH:mm:ss.fff} | {message}"))
            {
                _droppedLogs++;
            }
        }

        private void ProcessLogs()
        {
            foreach (var log in _logQueue.GetConsumingEnumerable())
            {
                if (_droppedLogs > 0)
                {
                    _writer.WriteLine($"[DROPPED {_droppedLogs} LOGS]");
                    _droppedLogs = 0;
                }
                _writer.WriteLine(log);
            }
        }

        public void Dispose()
        {
            _logQueue.CompleteAdding();
            _writer.Dispose();
        }
    }
}