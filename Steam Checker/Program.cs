using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Runtime.InteropServices;
using System.Linq;

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
        public static string read;
        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
         public static extern int MessageBox(IntPtr h, string m, string c, int type);
         static void Main()
        {
            Console.WriteLine("Created by czn#2264" + Environment.NewLine);
            Console.Title = "czn#2264's steam id checker";
            Check();
            Check2();

            Console.Clear();


            Water();


            Console.WriteLine("Type your selected mode [1,2,3]");
            Console.WriteLine("1 - 3 Char Automatic");
            Console.WriteLine("2 - 4 Char Automatic");
            Console.WriteLine("3 - Custom Checker");
            read = Console.ReadLine();
            if (read.ToLower().Equals("1"))
                ThreeChar();
            else if (read.ToLower().Equals("2"))
                FourChar();
            else if (read.ToLower().Equals("3"))
                CustomURL();
            else
            {
                Console.WriteLine("Enter a valid number");
                Process.GetCurrentProcess().Kill();
            }

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

        public static void ThreeChar()
        {
            Console.WriteLine("Mode : 3 Char");

            var alphabet = "abcdefghijklmnopqrstuvwxyz";
            var q = alphabet.Select(x => x.ToString());
            int size = 3;
            for (int i = 0; i < size - 1; i++)
                q = q.SelectMany(x => alphabet, (x, y) => x + y);

            foreach (var item in q)
            {
                try
                {

                    string site = myWebClient.DownloadString("https://steamcommunity.com/id/" + item);

                    if (site.Contains("The specified profile could not be found."))
                    {
                        using (StreamWriter sw = File.AppendText(@"aval.txt"))
                        {
                            sw.WriteLine($"{item} is avaliable");
                        }
                        avalnum++;
                        Console.Title = $"[Avaliable = {avalnum}] | [Unavaliable = {notnum}] | czn#2264";
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"{item} is avaliable!");
                    }
                    else
                    {
                        notnum++;
                        Console.Title = $"[Avaliable = {avalnum}] | [Unavaliable = {notnum}]  czn#2264";
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"{item} already exists.");
                    }

                }
                catch (Exception ex) { Console.WriteLine(ex + Environment.NewLine + "Create an issue with the error."); }
            }
        }

        public static void FourChar()
        {
            Console.WriteLine("Mode : 4 Char");

            var alphabet = "abcdefghijklmnopqrstuvwxyz";
            var q = alphabet.Select(x => x.ToString());
            int size = 4;
            for (int i = 0; i < size - 1; i++)
                q = q.SelectMany(x => alphabet, (x, y) => x + y);

            foreach (var item in q)
            {
                try
                {

                    string site = myWebClient.DownloadString("https://steamcommunity.com/id/" + item);

                    if (site.Contains("The specified profile could not be found."))
                    {
                        using (StreamWriter sw = File.AppendText(@"aval.txt"))
                        {
                            sw.WriteLine($"{item} is avaliable");
                        }
                        avalnum++;
                        Console.Title = $"[Avaliable = {avalnum}] | [Unavaliable = {notnum}] | czn#2264";
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"{item} is avaliable!");
                    }
                    else
                    {
                        notnum++;
                        Console.Title = $"[Avaliable = {avalnum}] | [Unavaliable = {notnum}]  czn#2264";
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"{item} already exists.");
                    }

                }
                catch (Exception ex) { Console.WriteLine(ex + Environment.NewLine + "Create an issue with the error."); }
            }
        }

        public static void CustomURL()
        {
            Console.WriteLine("Mode : Custom");
            Check();
            string[] lines = File.ReadAllLines(@"inputs.txt");
            if (new FileInfo(@"inputs.txt").Length == 0)
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
        }

        public static void Water()
        {
            var lol = (@"                  _  _  ____  ____   __   _  _   ");
            var lol2 = (@"  ___ _____ __  _| || ||___ \|___ \ / /_ | || |  ");
            var lol3 = (@" / __|_  / '_ \|_  ..  _|__) | __) | '_ \| || |_ ");
            var lol4 = (@"| (__ / /| | | |_      _/ __/ / __/| (_) |__   _|");
            var lol5 = (@" \___/___|_| |_| |_||_||_____|_____|\___/   |_|  ");
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (lol.Length / 2)) + "}", lol));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (lol2.Length / 2)) + "}", lol2));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (lol3.Length / 2)) + "}", lol3));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (lol4.Length / 2)) + "}", lol4));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (lol5.Length / 2)) + "}", lol5));
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
