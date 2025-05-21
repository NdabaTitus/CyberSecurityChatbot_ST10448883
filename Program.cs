using System;
using System.IO;
using System.Media;
using System.Threading;

namespace CyberSecurityChatbot
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Play greeting audio file if available
                string greetingPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "greeting.wav");
                if (File.Exists(greetingPath))
                {
                    new SoundPlayer(greetingPath).PlaySync();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.ResetColor();
                }

                // Load and display ASCII Art
                string asciiPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "ascii_art..txt");
                if (File.Exists(asciiPath))
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(File.ReadAllText(asciiPath));
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.ResetColor();
                }

                // Display chatbot header
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(new string('*', 60));
                Console.WriteLine("*                 Cybersecurity Awareness Bot                  *");
                Console.WriteLine(new string('*', 60));
                Console.ResetColor();

                // Typing effect welcome
                foreach (char c in "Welcome to the SecureBuddy\nLet's stay safe online!\n")
                {
                    Console.Write(c);
                    Thread.Sleep(50);
                }

                // Ask for name
                Console.Write("\nPlease enter your name: ");
                string userName = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(userName))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Name cannot be empty. Please try again.");
                    Console.ResetColor();
                    Console.Write("\nPlease enter your name: ");
                    userName = Console.ReadLine();
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nWelcome, {userName}! I'm here to answer your cybersecurity questions.\n");
                Console.ResetColor();

                // Set the user's name for memory recall
                Part2.SetUserName(userName);

                // Begin the conversation loop
                Part2.StartChat();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[Critical Error] " + ex.Message);
                Console.ResetColor();
            }
        }
    }
}
