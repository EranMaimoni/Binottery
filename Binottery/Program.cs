using Binottery.Model;
using log4net;
using log4net.Config;
using System.IO;
using System.Reflection;

namespace Binottery
{
    class Program
    {

        static void Main(string[] args)
        {
            // Load log4net configuration
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            GameEngine ge = new GameEngine();
            ge.Start();
        }
    }
}
