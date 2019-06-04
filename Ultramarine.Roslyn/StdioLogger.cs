using System;
using System.Collections.Generic;
using System.Text;
using Ultramarine.Workspaces;

namespace Ultramarine.Roslyn
{
    public class StdioLogger : ILogger
    {
        public void Error(Exception ex)
        {
            Console.WriteLine($"{DateTime.Now.ToString("T")}\nERROR: { ex.Message }\n{ex.StackTrace}");
        }

        public void Info(string messageFormat, params string[] parameters)
        {
            var message = string.Format(messageFormat, parameters);
            Console.WriteLine($"{DateTime.Now.ToString("T")}\n{message}\n");
        }

        public void Warn(string messageFormat, params string[] parameters)
        {
            var message = string.Format(messageFormat, parameters);
            Console.WriteLine($"{DateTime.Now.ToString("T")}\nWARNING: { message}\n");
        }
    }
}
