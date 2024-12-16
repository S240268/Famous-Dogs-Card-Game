using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Famous_Dogs
{
    internal class Program
    {
        private const int size_x = 5;
        private const int size_y = 30;
        static void LoadData(string filename, string[,] topTrumps)
        {
            string line;
            using (StreamReader file = new StreamReader(filename))
            {
                int next = 0;
                while ((line = file.ReadLine()) != null)
                {
                    //Console.WriteLine(line);
                    string[] splitted = line.Split(',');
                    for (int i = 0; i < splitted.Length; i++)
                    {
                        topTrumps[next, i] = splitted[i];
                    }

                    next++;
                }
            }
        }

        static void PrintRandomDog(string[,] stack)
        {
            Random rnd = new Random();
            int index = rnd.Next(0, size_y);
            for (int i = 0; i < size_x - 1; i++)
            {
                Console.Write(stack[index, i] + ", ");
            }

            Console.WriteLine(stack[index, size_x - 1]);
        }

        static bool IsInt(string s)
        {
            if (s.Length == 0) return false;
            foreach (char c in s)
            {
                if (!"1234567890".Contains(c))
                    return false;
            }
            return true;
        }

        static string[] copyNth(int n, string[,] stack)
        {
            string[] m = new string[5];
            for (int i = 0; i < 5; i++)
            {
                m[i] = stack[n, i];
            }
            return m;
        }

        static void PlayGame(string[,] topTrumps)
        {
            int cardSize;
            Console.WriteLine("Enter the size of the stack. Must be between 4 and 30, and must be even:");
            string answer = Console.ReadLine();
            if (!IsInt(answer))
            {
                // Send back to menu
                Console.WriteLine("Not a number!");
                return;
            }
            cardSize = Convert.ToInt32(answer);
            if (cardSize > 30 || cardSize < 4)
            {
                Console.WriteLine("Invalid range.");
                return;
            }
            if (cardSize % 2 == 1)
            {
                Console.WriteLine("Not divisible by 2");
                return;
            }

            Queue<string[]> ComputersStack = new Queue<string[]>();
            Queue<string[]> PlayerStack = new Queue<string[]>();
            Random rnd = new Random();


            for (int i = 0; i < cardSize; i++)
            {
                if (rnd.Next(0, 2) == 0){
                    ComputersStack.Enqueue(copyNth(i, topTrumps));
                } else {
                    PlayerStack.Enqueue(copyNth(i, topTrumps));
                }
            }

            // Balance Stacks
            while (ComputersStack.Count != PlayerStack.Count) {
                if (ComputersStack.Count > PlayerStack.Count) { 
                    PlayerStack.Enqueue(ComputersStack.Dequeue());
                } else
                {
                    ComputersStack.Enqueue(PlayerStack.Dequeue());
                }
            }

            while (ComputersStack.Count != 0 && PlayerStack.Count != 0)
            {
                Console.WriteLine("Your card is: ");
                foreach (string s in PlayerStack.Peek())
                {
                    Console.Write(s + ", ");
                }
                Console.WriteLine();

                Console.WriteLine("What catagory do you want to play on: fitness, intelligence, friendlinees, or drool (f/i/r/d)?");
                // Add breakline to avoid index errors
                char option = (Console.ReadLine() + "\n")[0];
                    
                if (!"fird".Contains(option))
                {
                    Console.WriteLine("Invalid choice. Try again.");
                    continue;
                }

                Console.Write("The Computer's card is: ");
                foreach (string s in ComputersStack.Peek())
                {
                    Console.Write(s + ", ");
                }
                Console.WriteLine();

                int index = "fird".IndexOf(option) + 1;

                // Determine winner, and add tops to bottom of winners stack
                if (Convert.ToInt32(ComputersStack.Peek()[index]) >
                    Convert.ToInt32(PlayerStack.Peek()[index]))
                {
                    Console.WriteLine("Computer won.");
                    ComputersStack.Enqueue(PlayerStack.Dequeue());
                    ComputersStack.Enqueue(ComputersStack.Dequeue());

                }
                else
                {
                    Console.WriteLine("You won!!");
                    PlayerStack.Enqueue(ComputersStack.Dequeue());
                    PlayerStack.Enqueue(PlayerStack.Dequeue());
                }

                Console.WriteLine(
                    $"You have {PlayerStack.Count} cards. The computer has {ComputersStack.Count} cards."
                );

                Console.WriteLine("----------------------------------------");
            }
        }


        static void Menu(string[,] topTrumps) {
            string option = "";
            while (true)
            {
                Console.WriteLine(
                    "What do you want to do? \n" +
                    " > play (p)\n" +
                    " > random dog (r)\n" +
                    " > quit (q)"
                );
                option = Console.ReadLine().ToLower();
                if (option == "q")
                {
                    break;
                }
                else if (option == "r")
                {
                    PrintRandomDog(topTrumps);
                }
                else if (option == "p")
                {
                    PlayGame(topTrumps);
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid option.. Try again.");
                }
            }

        }


        static void Main()
        {
            string filename = "C:\\Users\\S240268\\source\\repos\\Famous Dogs\\Famous Dogs\\dogsData.txt";
            string[,] topTrumps = new string[size_y, size_x];
            LoadData(filename, topTrumps);

            Console.WriteLine("Welcome to the Famous Dogs game!");
            Console.WriteLine("What is your name? ");
            string name = Console.ReadLine();
            Console.WriteLine($"Hello, {name}");

            Menu(topTrumps);

            Console.WriteLine("Goodbye...");
            Console.ReadLine();
        }
    }
}
