using System;

// Some thoughts. I don't like the multiple if statements and it feels like I can use a list to make this more efficient.

namespace DiceRoll
{
    class Program
    {
        static void Main(string[] args)
        {
            int user = 0, one = 0, two = 0, three = 0, four = 0, five = 0, six = 0, dice, times;

            Random rand = new Random();

            Console.WriteLine("How many dice would you like to roll?");
            times = Convert.ToInt32(Console.ReadLine());

            if (times > 0){
                Console.WriteLine("Auto Roll Enabled\nPlease type in 'm' for Manual Roll");
                string auto = Console.ReadLine();

                for (int i = 0; i < times; i++){
                    if (auto == "m") {
                        Console.WriteLine("Press any key to roll dice:");
                        Console.ReadLine();
                    }
                    
                    dice = rand.Next(1,7);
                    if (dice == 1){
                        one++;
                    } else if (dice == 2){
                        two++;
                    } else if (dice == 3){
                        three++;
                    } else if (dice == 4){
                        four++;
                    } else if (dice == 5){
                        five++;
                    } else{
                        six++;
                    }

                    user += dice;

                    Console.WriteLine("Roll number " + (i+1) + ": " + dice);
                }
                Console.WriteLine("Times a number was rolled:\n1: " + one + "\n2: " + two + "\n3: " + three + "\n4: " + four + "\n5: " + five + "\n6: " + six);
                Console.WriteLine("You rolled a total number: " + user);
                
            }
            else {
                Console.WriteLine("Invalid amount. Must be greater than 0");
            }
        }
    }
}