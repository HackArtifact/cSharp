using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Simple Calculator

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            int num1;
            int num2;
            string symbol;
            double ans;

            Console.WriteLine("Your Calculator, Built In C#");

            Console.Write("Enter First Number:");
            num1 = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter Your Operation:");
            Console.WriteLine("HINT >> * is muliply, / is divide, + is add, - is subtract, ^ is power to, % is square root");
            symbol = Console.ReadLine();

            if (symbol == "%")
            {
                ans = Math.Sqrt(num1);
            }
            else
            {
                Console.WriteLine("Enter Second Number:");
                num2 = Convert.ToInt32(Console.ReadLine());

                if (symbol == "+")
                {
                    ans = num1 + num2;
                }
                else if (symbol == "-")
                {
                    ans = num1 - num2;
                }
                else if (symbol == "/")
                {
                    ans = num1 / num2;
                }
                else if (symbol == "*")
                {
                    ans = num1 * num2;
                }
                else if (symbol == "^")
                {
                    ans = Math.Pow(num1, num2);
                }
                else
                {
                    ans = 0.0;
                }

            }

            Console.WriteLine("Result is: " + Convert.ToString(ans));
        }
    }
}