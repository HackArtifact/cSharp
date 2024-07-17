using System;
using System.IO;
using System.Collections.Generic;

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
            var surname = new List<string>();   //column 5
            var acc1 = new List<string>();   //column 6
            var acc2 = new List<string>();   //column 7
            var acc3 = new List<string>();   //column 8
            var acc4 = new List<string>();   //column 9
            var cardlessPin = new List<string>();   //column 10

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
                }
            }

            Console.WriteLine("Welcome To FILMER ATM!\nPlease enter your atm number or cardless withdraw pin:");
            string user = Console.ReadLine();
            int position = -1;

            if (user.Length == 16)
            {
                for (int i = 0; i < cardNumber.Count; i++) {
                    if (cardNumber[i] == user) {
                        position = i; //card number found.
                        break;
                    }
                }

                if (position != -1) {   //card number found || user exists
                    Console.WriteLine(Greetings(position, name, surname, balance));
                    while (true) {
                        Console.WriteLine("1. Your Balance.\n2. Withdraw.\n3. Open Account.\n4. Close Account\n5. Change PIN.\n6. Cardless PIN\n7. Logout.");
                        user = Console.ReadLine();
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

        static string Greetings(int position, List<string> name, List<string> surname, List<string> balance)
        {
            string fullName = $"{name[position]} {surname[position]}";
            string balanceAmount = balance[position];

            return $"Hello, {fullName}! Your current balance is R{balanceAmount}";
        }

        static int ClosingBalance(int position, List<string> balance) {
            return Convert.ToInt32(balance);
        }
        static string Withdraw(int position, List<string> balance, List<string> acc1, List<string> acc2, List<string> acc3, List<string> acc4)
        {
            Console.WriteLine("Which account would you like to withdraw from?\nAccount 1, Account 2, Account 3, Account 4 [Please only enter the number.]");
            string ans = Console.ReadLine();
            // Check which account to withdraw from.
            // Check if account is open (not NULL).
            // If not NULL then update account amount and balance amount.
            return $"";
        }
    }
}
