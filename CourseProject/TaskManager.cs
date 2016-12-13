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

        public void kill(int id)
        {
            try
            {
                Process.GetProcessById(id).Kill();
                Console.WriteLine("Process with id = {0} is killed", id);
            }
            catch
            {
                Console.WriteLine("Error trying to kill process");
            }
        }
        public void killByName(string name)
        {
            int counter = 0;
            foreach (Process process in getAllProcesses().Where(x => x.ProcessName == name))
            {
                try
                {
                    process.Kill();
                    counter += 1;
                } catch
                {
                    Console.WriteLine("Error trying to kill process {0}", name);
                }
            }
            Console.WriteLine("{0} processes with name {1} are killed", counter, name);
        }
        public void info()
        {
            using (PerformanceCounter memProcess = new PerformanceCounter("Memory", "Available MBytes"))
            {
                float memUseage = memProcess.NextValue();
                Console.WriteLine(" RAM Free: {0}MB", memUseage);
            }
        }
    }
}
