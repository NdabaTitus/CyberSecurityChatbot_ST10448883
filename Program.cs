using System;
using System.IO;
using System.Media;
using System.Threading;

namespace CyberSecurityChatbot
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Play Voice Greeting
                string greetingPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "greeting.wav");
                if (File.Exists(greetingPath))
                {
                    SoundPlayer player = new SoundPlayer(greetingPath);
                    player.PlaySync();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[Error] Greeting audio file not found at: " + greetingPath);
                    Console.ResetColor();
                }

                // Display ASCII Art
                string asciiPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "ascii_art..txt");

                Console.WriteLine($"[DEBUG] Trying to load ASCII Art from: {asciiPath}"); // Debug info

                if (File.Exists(asciiPath))
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    string asciiArt = File.ReadAllText(asciiPath);
                    Console.WriteLine(asciiArt);
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[Error] ASCII Art file not found at: " + asciiPath);
                    Console.ResetColor();
                    Environment.Exit(1); // Stop program if ASCII art is missing
                }

                // Decorative Header
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(new string('*', 60));
                Console.WriteLine("*                 Cybersecurity Awareness Bot                  *");
                Console.WriteLine(new string('*', 60));
                Console.ResetColor();

                // Typing effect intro
                foreach (char c in "Let's stay safe online!\n")
                {
                    Console.Write(c);
                    Thread.Sleep(50); // Typing effect
                }

                // Ask for user's name
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

                // Display question options
                Console.WriteLine("You can ask me questions like:");
                Console.WriteLine("- How are you?");
                Console.WriteLine("- What's your purpose?");
                Console.WriteLine("- What can I ask you about?");
                Console.WriteLine("- What is phishing?");
                Console.WriteLine("- How can I protect my passwords?");
                Console.WriteLine("- What are safe browsing habits?");
                Console.WriteLine("\nType your question below:");

                // Start conversation loop
                string userQuestion = Console.ReadLine();

                while (true)
                {
                    if (string.IsNullOrWhiteSpace(userQuestion))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("You entered nothing. Please type a valid question.");
                        Console.ResetColor();
                    }
                    else
                    {
                        string questionLower = userQuestion.ToLower();

                        if (questionLower.Contains("how are you"))
                        {
                            Console.WriteLine("I'm just a bot, but I'm doing great! Thanks for asking.");
                        }
                        else if (questionLower.Contains("purpose"))
                        {
                            Console.WriteLine("My purpose is to help you stay safe online by sharing cybersecurity tips!");
                        }
                        else if (questionLower.Contains("what can i ask"))
                        {
                            Console.WriteLine("You can ask me about password safety, phishing scams, and safe browsing habits.");
                        }
                        else if (questionLower.Contains("phishing"))
                        {
                            Console.WriteLine("Phishing is a type of cyber attack where malicious actors attempt to trick you into revealing personal or financial information, often via email or fake websites.");
                        }
                        else if (questionLower.Contains("passwords"))
                        {
                            Console.WriteLine("To protect your passwords, use strong, unique passwords for each account, enable two-factor authentication, and avoid reusing passwords across sites.");
                        }
                        else if (questionLower.Contains("safe browsing"))
                        {
                            Console.WriteLine("Safe browsing habits include avoiding suspicious websites, not clicking on unknown links, keeping your browser updated, and using a VPN when necessary.");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("I didn't quite understand that. Could you please rephrase your question?");
                            Console.ResetColor();
                        }
                    }

                    // Ask again
                    Console.WriteLine("\nAsk another question or type 'exit' to leave:");
                    userQuestion = Console.ReadLine();

                    if (userQuestion != null && userQuestion.Trim().ToLower() == "exit")
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("\nThank you for chatting, stay cyber-safe!");
                        Console.ResetColor();
                        break;
                    }
                }
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
