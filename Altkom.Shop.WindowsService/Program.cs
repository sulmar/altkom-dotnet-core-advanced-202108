using System;
using System.IO;
using System.Timers;
using Topshelf;

namespace Altkom.Shop.WindowsService
{
    class Program
    {
        // dotnet add package Topshelf
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            HostFactory.Run(p =>
            {
                p.Service<LoggerService>();
                p.SetDisplayName("Altkom Service");
                p.SetDescription("Opis usługi");
                p.StartAutomatically();
            });

            
        }
    }

    public class LoggerService : ServiceControl
    {
        private Timer timer;

        public LoggerService()
        {
            timer = new Timer();
            
        }

        public bool Start(HostControl hostControl)
        {
            Log("Started!");

            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            Log("Stopped.");

            return true;
        }

        private void Log(string message)
        {
            File.AppendAllText(@"c:\temp\log.txt", message);
        }
    }
}
