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
                    SoundPlayer player = new SoundPlayer(greetingPath);
                    player.PlaySync();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[Error] Greeting audio file not found at: " + greetingPath);
                    Console.ResetColor();
                }

                // Load and display ASCII Art
                string asciiPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "ascii_art..txt");
                Console.WriteLine($"[DEBUG] Trying to load ASCII Art from: {asciiPath}");

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
                    Environment.Exit(1); // Exit if important resources are missing
                }

                // Display chatbot header
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(new string('*', 60));
                Console.WriteLine("*                 Cybersecurity Awareness Bot                  *");
                Console.WriteLine(new string('*', 60));
                Console.ResetColor();

                // Typing effect for welcome message
                foreach (char c in "Welcome to the SecureBuddy\nLet's stay safe online!\n")
                {
                    Console.Write(c);
                    Thread.Sleep(50);
                }

                // Ask for user's name
                Console.Write("\nPlease enter your name: ");
                string userName = Console.ReadLine();

                // Validate user's input
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

                // Questions user can ask
                Console.WriteLine("You can ask me questions like:");
                Console.WriteLine("- How are you?");
                Console.WriteLine("- What's your purpose?");
                Console.WriteLine("- What can I ask you about?");
                Console.WriteLine("- What is phishing?");
                Console.WriteLine("- How can I protect my passwords?");
                Console.WriteLine("- What are safe browsing habits?");
                Console.WriteLine("- What is malware?");
                Console.WriteLine("- How can I recognize a secure website?");
                Console.WriteLine("- What should I do if I think my account is hacked?");
                Console.WriteLine("- What is two-factor authentication?");
                Console.WriteLine("- What are some common signs of a phishing email?");
                Console.WriteLine("\nType your question below:");

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

                        // Match user input with available answers
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
                            Console.WriteLine("You can ask me about password safety, phishing scams, malware, safe browsing, and more.");
                        }
                        else if (questionLower.Contains("phishing"))
                        {
                            Console.WriteLine("Phishing is a type of cyber attack where malicious actors attempt to trick you into revealing personal or financial information, often via email or fake websites.");
                        }
                        else if (questionLower.Contains("password"))
                        {
                            Console.WriteLine("To protect your passwords, use strong, unique passwords for each account, enable two-factor authentication, and avoid reusing passwords across sites.");
                        }
                        else if (questionLower.Contains("safe browsing"))
                        {
                            Console.WriteLine("Safe browsing habits include avoiding suspicious websites, not clicking on unknown links, keeping your browser updated, and using a VPN when necessary.");
                        }
                        else if (questionLower.Contains("malware"))
                        {
                            Console.WriteLine("Malware is malicious software designed to harm, exploit, or otherwise compromise computers or networks. Keep your antivirus updated and avoid downloading from untrusted sources.");
                        }
                        else if (questionLower.Contains("secure website"))
                        {
                            Console.WriteLine("A secure website uses HTTPS, has a valid SSL certificate (look for the lock icon next to the URL), and belongs to trusted organizations.");
                        }
                        else if (questionLower.Contains("account hacked"))
                        {
                            Console.WriteLine("If you think your account is hacked, change your passwords immediately, enable two-factor authentication, and notify the service provider.");
                        }
                        else if (questionLower.Contains("two-factor"))
                        {
                            Console.WriteLine("Two-factor authentication adds an extra layer of security by requiring a second form of verification, like a code sent to your phone, along with your password.");
                        }
                        else if (questionLower.Contains("sign of a"))
                        {
                            Console.WriteLine("Signs of a phishing email include poor spelling/grammar, urgent requests for personal info, suspicious links, and sender addresses that look fake or unusual.");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("I didn't quite understand that. Could you please rephrase your question?");
                            Console.ResetColor();
                        }
                    }

                    // Prompt for another question or exit
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
                // Handle any unexpected errors
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[Critical Error] " + ex.Message);
                Console.ResetColor();
            }
        }
    }
}
