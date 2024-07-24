using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;

namespace AYASOMPO.CronReminder.WindowsService
{
    public partial class Service1 : ServiceBase
    {
        private Timer _timer;
        private List<string> _urls;
        private int _interval;

        public Service1()
        {
            InitializeComponent();
            _urls = new List<string>();
        }

        protected override void OnStart(string[] args)
        {
            _urls.AddRange(new List<string> { "http://localhost:8086", "http://localhost:8087", "http://localhost:4200" });
            _interval = 10; // Interval in minutes

            _timer = new Timer(OnTimedEvent, null, TimeSpan.Zero, TimeSpan.FromMinutes(_interval));
        }

        private async void OnTimedEvent(object state)
        {
            if (_urls != null)
            {
                foreach (var url in _urls)
                {
                    await MakeRequest(url);
                }
            }
        }

        private async Task MakeRequest(string url)
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest(url, Method.Get);
            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
                LogMessage($"OK\nRequest made to {url} at {DateTime.Now:HH:mm:ss.fff}");
            else
                LogMessage($"Error: {response.StatusCode}\nRequest made to {url} at {DateTime.Now:HH:mm:ss.fff}");
        }

        protected override void OnStop()
        {
            _timer.Dispose();
        }

        private void LogMessage(string message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string logFilePath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\SchedulerService_" + DateTime.Now.Date.ToShortDateString().Replace('/', '.') + ".txt";

            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(logFilePath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(logFilePath));
                }

                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine(message);
                }
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("CronReminderService", $"Logging Error: {ex.Message}", EventLogEntryType.Error);
            }
        }
    }
}
