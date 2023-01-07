using System.Drawing;
using ANSIConsole;

namespace Azyeb.Handle;

public static class Error
{
    static Error()
    {
        if (!ANSIInitializer.Init(false)) ANSIInitializer.Enabled = false;
    }
    
    public class ErrorInfo
    {
        public string ErrorLine { get; set; } = string.Empty;
        public int ErrorIndex { get; set; } = -1;
        public int ErrorLen { get; set; } = -1;
        public ErrorLevel ErrorLevel { get; set; } = ErrorLevel.None;
        public string ErrorDesc { get; set; } = string.Empty;

        public void Init()
        {
            if (string.IsNullOrEmpty(ErrorLine)) return;
            if (string.IsNullOrEmpty(ErrorDesc)) return;
            if (ErrorIndex == -1) return;
            if (ErrorLen == -1) return;
            if (ErrorLevel == ErrorLevel.None) return;

            ErrorLine = $"{ErrorLine.Substring(0, ErrorIndex)}{
                ((ErrorLevel == ErrorLevel.Warn) 
                    ? (ErrorLine.Substring(ErrorIndex, ErrorLen).Color(Color.Gold).ToString()) 
                    : (ErrorLevel == ErrorLevel.Error) 
                        ? (ErrorLine.Substring(ErrorIndex, ErrorLen).Color(Color.Red).ToString()) 
                        : "")
            }{ErrorLine.Substring(ErrorIndex + ErrorLen)}\r\n{new string(' ', 1)}\\=> {ErrorDesc}\r\n";
        }
    }

    public enum ErrorLevel
    {
        None,
        Warn,
        Error
    }

    public static void PrintError(ErrorInfo errorInfo)
    {
        errorInfo.Init();
        Console.WriteLine(errorInfo.ErrorLine);
        if (errorInfo.ErrorLevel == ErrorLevel.Error)
        {
            Console.WriteLine("Enter key for exit.");
            Console.ReadKey();
            Environment.Exit(-1);
        }
    }
}