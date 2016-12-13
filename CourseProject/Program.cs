using System;
using System.Management;
using System.Diagnostics;

namespace CourseProject
{
    class Program
    {
        static void Main(string[] args)
        {
            string menu;
            TaskManager taskManager = new TaskManager();
            ManagementObjectSearcher InfoProc = new ManagementObjectSearcher("root\\CIMV2",
               "SELECT * FROM Win32_Processor");
            ManagementObjectSearcher InfoVideo = new ManagementObjectSearcher("root\\CIMV2",
                "SELECT * FROM Win32_VideoController");
            ManagementObjectSearcher InfoMemory = new ManagementObjectSearcher("root\\CIMV2",
                 "SELECT * FROM Win32_PhysicalMemory");

            Console.ForegroundColor = ConsoleColor.Green;
            printHelp();

            while (true)
            {
                try
                {
                    menu = Console.ReadLine();
                    if (menu.StartsWith("exit"))
                    {
                        break;
                    }
                    if (menu.StartsWith("clear"))
                    {
                        Console.Clear();
                    }
                    if (menu.StartsWith("list"))
                    {
                        taskManager.printAllProcesses();
                    }
                    if (menu.Contains("help"))
                    {
                        printHelp();
                    }
                    if (menu.StartsWith("system info"))
                    {
                        Console.WriteLine("\nProcessor: \n");
                        foreach (ManagementObject processor in InfoProc.Get())
                        {
                            Console.WriteLine(" Name:              {0}\n Number of Cores:   {1}\n Processor ID:      {2}",
                                processor["Name"], processor["NumberOfCores"], processor["ProcessorId"]);
                        }

                        Console.WriteLine("\nVideo Controllers: \n");
                        foreach (ManagementObject video in InfoVideo.Get())
                        {
                            Console.WriteLine(" Adapter RAM:       {0}\n Caption:           {1}\n Description:       {2}\n Video Processor:   {3}\n",
                                video["AdapterRAM"], video["Caption"], video["Description"], video["VideoProcessor"]);
                        }
                        Console.WriteLine("RAM: \n");
                        foreach (ManagementObject memory in InfoMemory.Get())
                        {
                            Console.WriteLine(" Bank Label: {0} | Capacity: {1} Gb | Speed: {2}",
                                memory["BankLabel"], Math.Round(Convert.ToDouble(memory["Capacity"]) / 1024 / 1024 / 1024, 2), memory["Speed"]);
                        }
                        Console.WriteLine();

                        Console.WriteLine("Ram Usage: \n");
                        taskManager.info();
                    }
                    if (menu.StartsWith("kill id"))
                    {
                        taskManager.kill(Convert.ToInt32(menu.Substring(8)));
                    } else
                    {
                        if (menu.StartsWith("kill"))
                        {
                            taskManager.killByName(menu.Substring(5));
                        }
                    }
                } catch
                {
                }
            }
        }

        public static void printHelp()
        {
            Console.Clear();
            Console.WriteLine("Help:");
            Console.WriteLine("list - prints all processes");
            Console.WriteLine("kill [process name] - kills process by name");
            Console.WriteLine("system info - prints information obout yout pc");
            Console.WriteLine("help - prints this help");
            Console.WriteLine("exit - quit");
        }
    }
}
