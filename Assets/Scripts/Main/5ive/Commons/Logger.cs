using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace Main._5ive.Commons {
    public class Logger {
        private static Logger instance;

        private static FileStream fileStream;

        private const string FileName = "5ive.log";

        private Logger() {
        }

        public static Logger GetLogger() {
            if (instance == null) {
                fileStream = File.Open(FileName, FileMode.Append);
                instance = new Logger();
            }

            return instance;
        }

        public void Log(Priority priority, string message) {
            if (priority.Equals(Priority.HIGH)) {
                Debug.Log(message);
            }

            Log(message);
        }

        public void LogLow(string message) {
            Log(message);
        }

        public void LogHigh(string message) {
            Debug.Log(message);
            Log(message);
        }

        private void Log(string message) {
            byte[] bytes = Encoding.UTF8.GetBytes(message + Environment.NewLine);
            fileStream.Write(bytes, 0, bytes.Length);
        }

        public void Close() {
            fileStream.Close();
        }
    }

    public enum Priority {
        LOW,
        HIGH
    }
}