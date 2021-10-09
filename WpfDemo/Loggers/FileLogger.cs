using System;
using System.IO;

namespace WpfDemo.Loggers
{
    public sealed class FileLogger : ILogger
    {
        private const string LoggerDirectory = "Logger";
        private const string LoggerFileNamePrefix = "Logger";
        private static readonly Lazy<FileLogger> lazy = new Lazy<FileLogger>(() => new FileLogger());

        public static FileLogger Instance { get { return lazy.Value; } }

        private FileLogger()
        {
        }

        public void Log(string message)
        {
            if (!Directory.Exists(LoggerDirectory))
            {
                Directory.CreateDirectory(LoggerDirectory);
            }

            DateTime dateTime = DateTime.Now;
            string filePath = $"{LoggerDirectory}\\{LoggerFileNamePrefix}_{dateTime.Day}_{dateTime.Month}_{dateTime.Year}.txt";

            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }

            using (StreamWriter streamWriter = File.AppendText(filePath))
            {
                string text = $"{DateTime.Now.ToString()}: {message}";
                streamWriter.WriteLine(text);
            }
        }
    }
}
