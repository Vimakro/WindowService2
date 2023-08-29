using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Timer = System.Timers.Timer;

namespace IncrementService
{
    internal class IncrementService : ServiceBase
    {
        private Timer timer;
        private int counter;


        private DateTime sDataTime;
        private int timerInterval;


        public IncrementService()
        {
            this.ServiceName = "IncrementService";
            this.CanStop = true;
            this.CanPauseAndContinue = true;
            this.AutoLog = true;


            this.sDataTime = DateTime;
            this.timer = timerInterval;
        }

        protected override void OnStart(string[] args)
        {
            if (args.Length >= 1)
            {
                DateTime.TryParse(args[0], out sDataTime);
                int.TryParse(args[1], out timerInterval);
            }

            counter = 0;
            string path = @"E:\Increment.txt";
            if (!File.Exists(path)){File.Create(path).Close()};

            timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += new ElapsedEventHandler(OnTimer);
            timer.Start();

        }


        protected override void OnStop()
        {
            timer.Stop();
        }


        private void OnTimer(object sender, ElapsedEventArgs e) 
        {

            if (DateTime.Now >= sDataTime)
            {
                counter++;
                string path = @"E:\Increment.txt";
                using (StreamWriter sw = File.AppendText(path)){sw.WriteLine(counter)};
            }

        }
    }
}
