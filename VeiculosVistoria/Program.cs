using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace VeiculosVistoria
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new FormBusca());
        }
    }
}