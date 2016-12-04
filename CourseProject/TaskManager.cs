using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace CourseProject
{
    class TaskManager
    {
        public TaskManager()
        {
            
        }
        public Process[] getAllProcesses()
        {
            return Process.GetProcesses();
        }

        public void printAllProcesses()
        {
            int num = 0;
            foreach (Process instance in getAllProcesses()) {
                Console.WriteLine("{0} {1} {2}", num, instance.ProcessName, instance.Id);
                num+=1;
            }
        }
        public void test(string procName)
        {
            foreach (Process process in getAllProcesses().Where(x => x.ProcessName == procName))
            {
                using (PerformanceCounter pcProcess = new PerformanceCounter("Process", "% Processor Time", process.ProcessName))
                using (PerformanceCounter memProcess = new PerformanceCounter("Memory", "Available MBytes"))
                {
                    pcProcess.NextValue();
                    Thread.Sleep(100);
                    Console.WriteLine("");
                    Console.ForegroundColor = ConsoleColor.Red;
                    float cpuUseage = pcProcess.NextValue();
                    Console.WriteLine("Process: '{0}' CPU Usage: {1}%", process.ProcessName, cpuUseage);
                    Console.ForegroundColor = ConsoleColor.Green;
                    float memUseage = memProcess.NextValue();
                    Console.WriteLine("Process: '{0}' RAM Free: {1}MB", process.ProcessName, memUseage);
                }
            }
        } 
    }
}
