using System;
using System.IO;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using ClosedXML.Excel;

namespace ATM
{
    class Program
    {
        static void Main(string[] args)
        {
            var cardNumber = new List<string>(); //column 1
            var cardPin = new List<string>();    //column 2
            var balance = new List<string>();    //column 3
            var name = new List<string>();       //column 4
            var surname = new List<string>();    //column 5
            var acc1 = new List<string>();       //column 6
            var acc2 = new List<string>();       //column 7
            var acc3 = new List<string>();       //column 8
            var acc4 = new List<string>();       //column 9
            var cardlessPin = new List<string>();   //column 10
            var cardlessAmount = new List<string>();   //column 11

            using (var rd = new StreamReader("cardInfo.csv"))
            {
                while (!rd.EndOfStream)
                {
                    var splits = rd.ReadLine().Split(';');
                    cardNumber.Add(splits[0]);
                    cardPin.Add(splits[1]);
                    balance.Add(splits[2]);
                    name.Add(splits[3]);
                    surname.Add(splits[4]);
                    acc1.Add(splits[5]);
                    acc2.Add(splits[6]);
                    acc3.Add(splits[7]);
                    acc4.Add(splits[8]);
                    cardlessPin.Add(splits[9]);
                    cardlessAmount.Add(splits[10]);
                }
            }

            Console.WriteLine("Welcome To FILMER ATM!\nPlease enter your atm number or cardless withdraw pin:");
            string user = null;

            // Hiding sensitive information
            while (true)
            {
                var key = System.Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                    break;
                user += key.KeyChar;
            }

            int position = -1;

            if (user.Length == 16)
            {
                for (int i = 0; i < cardNumber.Count; i++)
                {
                    // Console.WriteLine(cardNumber[i]);
                    if (cardNumber[i] == user)
                    {
                        position = i; //card number found.
                        break;
                    }
                }

                if (position != -1)
                {   //card number found || user exists
                    Console.WriteLine(PIN(position, cardPin));
                    while (true)
                    {
                        Console.WriteLine(Greetings(position, name, surname));
                        Console.WriteLine("1. Your Balance.\n2. Withdraw.\n3. Open Account.\n4. Close Account\n5. Change PIN.\n6. Cardless PIN\n7. Logout.");
                        user = Console.ReadLine();
                        if (user == "1")
                        {
                            Console.WriteLine(Balance(position, balance, acc1, acc2, acc3, acc4));
                            ClearScreen();
                        }
                        else if (user == "6")
                        {
                            Console.WriteLine(CardlessP(position, cardlessPin, cardlessAmount));
                        }
                        else if (user == "7")
                        {
                            Console.WriteLine("You are now logged out!");
                            break;
                        }
                    }
                }
            }
            else if (user.Length == 8)
            {
                // Make account have cardless pin column and generate pin when logged in.
                // Deletes pin once used.
            }
            else
            {
                Console.WriteLine("Invalid card number or cardless pin.");
            }
        }

        // Enter PIN to account
        static string PIN(int position, List<string> cardPin)
        {
            Console.WriteLine("Card Number Found!\nPlease enter your PIN:");
            string input = null;
            while (true)
            {
                var key = System.Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                    break;
                input += key.KeyChar;
            }

            if (input != cardPin[position])
            {
                Console.WriteLine("Access DENIED!");
                Environment.Exit(0);
            }
            return "Access Granted!\n";
        }

        // Greetings Message
        static string Greetings(int position, List<string> name, List<string> surname)
        {
            string fullName = $"{name[position]} {surname[position]}";

            return $"Hello, {fullName}!\nWhat would you like to do?";
        }

        // Show balance of account until pressing a button
        static string Balance(int position, List<string> balance, List<string> acc1, List<string> acc2, List<string> acc3, List<string> acc4)
        {
            string balanceAmount = $"Your current balance is R{balance[position]}";
            string accounts = $"\nAccount 1 holds R{acc1[position]}";

            if (acc2 != null)
            {
                accounts += $"\nAccount 2 holds R{acc2[position]}";
            }
            else
            {
                accounts += $"\nAccount 2 is closed";
            }
            if (acc3 != null)
            {
                accounts += $"\nAccount 3 holds R{acc3[position]}";
            }
            else
            {
                accounts += $"\nAccount 3 is closed";
            }
            if (acc4 != null)
            {
                accounts += $"\nAccount 4 holds R{acc4[position]}";
            }
            else
            {
                accounts += $"\nAccount 4 is closed";
            }

            return balanceAmount + accounts;
        }

        // Creating Cardless PIN
        static string CardlessP(int position, List<string> cardlessPin, List<string> cardlessAmount)
        {
            // Checking if PIN Exists

            // Creating PIN
            Console.WriteLine("Would you like to create a cardless pin?\nYes(y) or No(n)");
            string user = Console.ReadLine();

            if (user.ToLower() == "y")
            {
                Console.WriteLine("Please enter cash limit:");
                cardlessAmount[position] = Console.ReadLine();

                // Generate a random cardless pin
                Random pin = new Random();
                cardlessPin[position] = pin.Next(1000, 10000).ToString();

                // Update the CSV file
                var fileLines = new List<string>();

                // Read all lines from the file
                using (var rd = new StreamReader("cardInfo.csv"))
                {
                    while (!rd.EndOfStream)
                    {
                        fileLines.Add(rd.ReadLine());
                    }
                }

                // Modify the specific line in the file
                for (int i = 0; i < fileLines.Count; i++)
                {
                    if (i == position)
                    {
                        var splits = fileLines[i].Split(';');
                        splits[9] = cardlessPin[position]; // Update cardless pin (column 10)
                        splits[10] = cardlessAmount[position]; // Update cardless amount (column 11)
                        fileLines[i] = string.Join(";", splits);
                    }
                }

                // Write the modified content back to the file
                using (var writer = new StreamWriter("cardInfo.csv"))
                {
                    foreach (string line in fileLines)
                    {
                        writer.WriteLine(line);
                    }
                }

                Console.WriteLine($"Your cardless pin is {cardlessPin[position]} for the amount of R{cardlessAmount[position]}");
                ClearScreen();
            }

            return "Your PIN has been created!";
        }


        // Closing balance after transfers/withdraws
        static int ClosingBalance(int position, List<string> balance)
        {
            return Convert.ToInt32(balance);
        }

        // Withdrawing money from account
        static string Withdraw(int position, List<string> balance, List<string> acc1, List<string> acc2, List<string> acc3, List<string> acc4)
        {
            Console.WriteLine("Which account would you like to withdraw from?\nAccount 1, Account 2, Account 3, Account 4 [Please only enter the number.]");
            string ans = Console.ReadLine();
            // Check which account to withdraw from.
            // Check if account is open (not NULL).
            // If not NULL then update account amount and balance amount.
            return $"";
        }

        static void ClearScreen()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
