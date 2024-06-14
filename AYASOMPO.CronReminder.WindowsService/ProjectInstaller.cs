using System.ComponentModel;
using System.ServiceProcess;

namespace AYASOMPO.CronReminder.WindowsService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        private ServiceProcessInstaller processInstaller;
        private ServiceInstaller serviceInstaller;

        public ProjectInstaller()
        {
            processInstaller = new ServiceProcessInstaller();
            serviceInstaller = new ServiceInstaller();

            // Service will run under system account
            processInstaller.Account = ServiceAccount.LocalSystem;

            serviceInstaller.StartType = ServiceStartMode.Automatic;

            serviceInstaller.ServiceName = "CronReminderService";

            Installers.Add(serviceInstaller);
            Installers.Add(processInstaller);
        }
    }
}
