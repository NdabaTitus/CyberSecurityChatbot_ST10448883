using System;
using System.Collections.Generic;

namespace CyberSecurityChatbot
{
    public static class Part2
    {
        // Stores the name of the user interacting with the bot
        private static string userName = "";

        // Stores the user's favorite or most mentioned topic
        private static string favoriteTopic = "";

        // Random generator used for picking random responses
        private static readonly Random rand = new Random();

        // Keywords used for basic sentiment detection
        private static readonly List<string> worriedKeywords = new() { "worried", "concerned", "anxious", "scared" };
        private static readonly List<string> curiousKeywords = new() { "curious", "interested", "want to know" };
        private static readonly List<string> frustratedKeywords = new() { "frustrated", "annoyed", "upset", "confused" };

        // Dictionary mapping cybersecurity topics to a list of helpful responses
        private static readonly Dictionary<string, List<string>> topicResponses = new()
        {
            ["password"] = new()
            {
                "Use strong and unique passwords. Avoid using names or birthdates.",
                "Try a password manager to create and store secure passwords.",
                "Enable two-factor authentication to protect your login credentials."
            },
            ["phishing"] = new()
            {
                "Phishing is when someone pretends to be a trusted source to steal your information.",
                "Avoid clicking on links from unknown senders or suspicious emails.",
                "Check the sender’s address and spelling mistakes — they’re red flags!"
            },
            ["privacy"] = new()
            {
                "Keep your social media profiles private to avoid oversharing.",
                "Limit the amount of personal information shared online.",
                "Always review app permissions and data sharing settings."
            },
            ["malware"] = new()
            {
                "Malware is software designed to harm your device or steal your data.",
                "Use antivirus and anti-malware tools, and keep them updated.",
                "Avoid downloading unknown files or clicking shady links."
            },
            ["scam"] = new()
            {
                "Watch out for deals that are too good to be true — they're likely scams.",
                "Scammers may impersonate banks or companies. Always verify before responding.",
                "Never give away sensitive info like OTPs or card numbers over the phone or email."
            },
            ["hacked"] = new()
            {
                "If you suspect you've been hacked, change your passwords immediately.",
                "Check your devices for unauthorized access or apps.",
                "Enable security features like two-factor authentication and alert notifications."
            },
            ["protect"] = new()
            {
                "Keep your software and operating system updated to fix vulnerabilities.",
                "Use strong passwords, antivirus software, and enable firewalls.",
                "Be cautious when connecting to public Wi-Fi networks."
            },
            ["report"] = new()
            {
                "Report phishing or scam attempts to your local cybercrime unit.",
                "Most platforms like Gmail or Facebook have built-in reporting tools.",
                "Reporting helps authorities track and stop future threats."
            },
            ["vpn"] = new()
            {
                "A VPN helps keep your internet activity private by encrypting your connection.",
                "Use a reputable VPN, especially when browsing on public networks.",
                "VPNs also help bypass geo-blocked content and censorship."
            }
        };

        // Sets the user's name (called externally before chat starts)
        public static void SetUserName(string name)
        {
            userName = name;
        }

        // Starts the main chatbot loop where user can interact
        public static void StartChat()
        {
            Console.WriteLine("Ask me any cybersecurity question or say 'exit' to quit.\n");

            while (true)
            {
                Console.Write($"{userName}: ");
                string userInput = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(userInput))
                {
                    Console.WriteLine("Please enter a question or type 'exit' to quit.");
                    continue;
                }

                string questionLower = userInput.ToLower();

                // Exit condition
                if (questionLower == "exit")
                {
                    Console.WriteLine("Goodbye! Stay safe online!");
                    break;
                }

                try
                {
                    // Try basic FAQ responses first
                    BasicFAQResponses(questionLower);
                }
                catch (Exception ex) when (ex.Message == "NoBasicFAQ")
                {
                    // Check if user is sharing a topic of interest
                    if (questionLower.Contains("interested in"))
                    {
                        foreach (var topic in topicResponses.Keys)
                        {
                            if (questionLower.Contains(topic))
                            {
                                favoriteTopic = topic;
                                Console.WriteLine($"Great! I'll remember that you're interested in {favoriteTopic}.");
                                goto NextIteration; // Skip remaining logic in this loop
                            }
                        }

                        Console.WriteLine("That's interesting! Can you tell me more about what cybersecurity topics you like?");
                        goto NextIteration;
                    }

                    // Try to detect user sentiment and respond accordingly
                    string detectedSentiment = DetectSentiment(questionLower);
                    if (!string.IsNullOrEmpty(detectedSentiment))
                    {
                        RespondToSentiment(detectedSentiment);
                    }

                    // Attempt to match the question to a known topic and respond
                    bool foundTopic = false;
                    foreach (var topic in topicResponses.Keys)
                    {
                        if (questionLower.Contains(topic))
                        {
                            var responses = topicResponses[topic];
                            Console.WriteLine(responses[rand.Next(responses.Count)]);

                            // Remind user of their favorite topic if this is different
                            if (!string.IsNullOrEmpty(favoriteTopic) && favoriteTopic != topic)
                            {
                                Console.WriteLine($"By the way, since you're interested in {favoriteTopic}, you might want to review tips about it too.");
                            }

                            foundTopic = true;
                            break;
                        }
                    }

                    // If no known topic matched, fallback messages
                    if (!foundTopic)
                    {
                        if (!string.IsNullOrEmpty(favoriteTopic))
                        {
                            Console.WriteLine($"As someone interested in {favoriteTopic}, you might want to explore more on that topic.");
                        }
                        else
                        {
                            Console.WriteLine("I'm not sure I understand. Try asking about passwords, phishing, VPNs, scams, or how to protect your data.");
                        }
                    }
                }

            // Labeled statement to jump to next user input
            NextIteration:
                continue;
            }
        }

        // Handles known FAQ-style questions using hardcoded phrase checks
        private static void BasicFAQResponses(string input)
        {
            if (input.Contains("how are you"))
                Console.WriteLine("I'm just a bot, but I'm doing great! Thanks for asking.");
            else if (input.Contains("purpose"))
                Console.WriteLine("My purpose is to help you stay safe online by sharing cybersecurity tips!");
            else if (input.Contains("what can i ask"))
                Console.WriteLine("You can ask about password safety, phishing, malware, scams, VPNs, reporting cybercrime, and more.");
            else if (input.Contains("safe browsing"))
                Console.WriteLine("Safe browsing means avoiding suspicious websites, keeping your browser updated, and using a VPN.");
            else if (input.Contains("secure website"))
                Console.WriteLine("Look for HTTPS and a lock icon in the address bar. These indicate secure websites.");
            else if (input.Contains("account hacked"))
                Console.WriteLine("If your account is hacked, change your passwords and enable two-factor authentication immediately.");
            else if (input.Contains("two-factor"))
                Console.WriteLine("Two-factor authentication adds a second layer of security to your login process.");
            else if (input.Contains("sign of a"))
                Console.WriteLine("Signs of a phishing email include urgent language, spelling errors, and strange URLs.");
            else
                throw new Exception("NoBasicFAQ"); // If no match is found, throw to trigger fallback logic
        }

        // Detects basic user sentiment based on keyword lists
        private static string DetectSentiment(string input)
        {
            foreach (var word in worriedKeywords)
                if (input.Contains(word)) return "worried";

            foreach (var word in curiousKeywords)
                if (input.Contains(word)) return "curious";

            foreach (var word in frustratedKeywords)
                if (input.Contains(word)) return "frustrated";

            return null;
        }

        // Prints a supportive message based on detected user sentiment
        private static void RespondToSentiment(string sentiment)
        {
            switch (sentiment)
            {
                case "worried":
                    Console.WriteLine("It's normal to feel worried. Cybersecurity can be scary, but you're taking the right steps by learning!");
                    break;
                case "curious":
                    Console.WriteLine("Curiosity is the first step toward digital safety. Keep asking questions!");
                    break;
                case "frustrated":
                    Console.WriteLine("Cybersecurity can be overwhelming. Don't worry — I'm here to guide you through it.");
                    break;
            }
        }
    }
}


