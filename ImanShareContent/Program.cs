using System.ServiceProcess;

namespace ImanShareContent
{
    /// <summary>
    /// 
    /// </summary>
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new ShareContent()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
