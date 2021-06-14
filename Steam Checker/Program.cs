using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Runtime.InteropServices;

namespace Steam_Checker
{
    public class Program
    {
        public static string chars;
        public static char[] stringChars;
        public static Random random;
        public static string finalString;
        public static WebClient myWebClient = new WebClient();
        public static int avalnum = 0;
        public static int notnum = 0;
        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
         public static extern int MessageBox(IntPtr h, string m, string c, int type);
    static void Main()
        {
            Console.WriteLine("Created by czn#2264" + Environment.NewLine);
            Console.Title = "czn#2264's steam id checker";
            Check();
            Check2();
            string[] lines = File.ReadAllLines(@"inputs.txt");

            if(new FileInfo(@"inputs.txt").Length == 0)
            {
                MessageBox((IntPtr)0, "Please put words into inputs.txt", "czn's ID Checker", 0);
                Process.GetCurrentProcess().Kill();

            }

            foreach (string line in lines)
            {
                try
                {
                    string site = myWebClient.DownloadString("https://steamcommunity.com/id/" + line);

                    if (line.Length < 3)
                        continue;
                    // 
                    if (line.Length > 32)
                        continue;

                    if (line.Contains("."))
                        continue;

                    if (site.Contains("The specified profile could not be found."))
                    {
                        using (StreamWriter sw = File.AppendText(@"aval.txt"))
                        {
                            sw.WriteLine($"{line} is avaliable");
                        }
                        avalnum++;
                        Console.Title = $"[Avaliable = {avalnum}] | [Unavaliable = {notnum}] | czn#2264";
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"{line} is avaliable!");
                    }
                    else
                    {
                        notnum++;
                        Console.Title = $"[Avaliable = {avalnum}] | [Unavaliable = {notnum}]  czn#2264";
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"{line} already exists.");
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex + Environment.NewLine + "Create an issue with the error."); }
            }
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(Environment.NewLine + "Finished lookup. You can close the window now.");
            Console.Read();
        }

        public static void Check()
        {
            if (File.Exists(@"inputs.txt"))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Inputs.txt exists.");
                Console.ForegroundColor = ConsoleColor.Cyan;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                File.Create(@"inputs.txt");
                Console.WriteLine("inputs.txt doesn't exist. Its been created.");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Process.Start("citizens steam id checker.exe", Environment.CommandLine.ToString());
                MessageBox((IntPtr)0, "Inputs.txt created. Put your ids in there.", "czn's ID Checker", 0);
                Process.GetCurrentProcess().Kill();
            }
        }

        public static void Check2()
        {
            if (File.Exists(@"aval.txt"))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Aval.txt exists.");
                Console.ForegroundColor = ConsoleColor.Cyan;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                File.Create(@"aval.txt");
                Console.WriteLine("Aval.txt doesn't exist. Its been created.");
                Console.ForegroundColor = ConsoleColor.Cyan;
            }
        }
    }
}
