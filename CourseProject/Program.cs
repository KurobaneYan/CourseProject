using System;
using System.Diagnostics;

namespace CourseProject
{
    class Program
    {
        static void Main(string[] args)
        {
            string menu;
            TaskManager taskManager = new TaskManager();

            printHelp();

            while(true)
            {
                menu = Console.ReadLine();
                if (menu.StartsWith("q"))
                {
                    break;
                }
                if (menu.StartsWith("list"))
                {
                    Console.Clear();
                    taskManager.printAllProcesses();
                }
                if (menu.Contains("help"))
                {
                    printHelp();
                }
                if (menu.StartsWith("kill"))
                {
                    Console.Clear();
                    Console.WriteLine(menu.Substring(5));
                    try
                    {
                        Process.GetProcessById(Convert.ToInt32(menu.Substring(5))).Kill();
                        Console.WriteLine("Process {0} killed", menu.Substring(5));
                    } catch
                    {
                        Console.WriteLine("Error trying to kill process");
                    }
                }
                if (menu.Contains("test"))
                {
                    taskManager.test(menu.Substring(5));
                }
            }
        }

        public static void printHelp()
        {
            Console.Clear();
            Console.WriteLine("Help:");
            Console.WriteLine("list - prints all processes");
            Console.WriteLine("kill [process id] - kills process by id");
            Console.WriteLine("help - prints this help");
            Console.WriteLine("q - quit");
        }
    }
}
