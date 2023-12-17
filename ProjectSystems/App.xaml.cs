using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectSystems
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            InitializerLogger();
        }

        public void InitializerLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("./../../../logWPF.txt")
                .CreateLogger();
        }
    }
}
