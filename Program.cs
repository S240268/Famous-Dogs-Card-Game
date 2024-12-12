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
            foreach (char c in s) {
                if (!"1234567890".Contains(c))
                    return false;
            }
            return true;
        }

        static void ShuffleStack(string[,] stack) {
            // Add later
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
            if (cardSize > 30 || cardSize < 4) {
                Console.WriteLine("Invalid range.");
                return;
            }
            if (cardSize % 2 == 1) {
                Console.WriteLine("Not divisible by 2");
                return;
            }
            
            // Do later
            ShuffleStack(topTrumps);
            Queue<string[]> ComputersStack = new Queue<string[]>();
            Queue<string[]> PlayerStack = new Queue<string[]>();

            for (int i = 0; i < cardSize; i++) {
                if (i % 2 == 0) {
                    ComputersStack.Enqueue(copyNth(i, topTrumps));
                } else {
                    PlayerStack.Enqueue(copyNth(i, topTrumps));
                }
            }

            while (ComputersStack.Count != 0 && PlayerStack.Count != 0)
            {
                Console.WriteLine("Your card is: ");
                foreach (string s in PlayerStack.Peek()) {
                    Console.Write(s + ", ");
                }
                Console.WriteLine();
                
                Console.WriteLine("What catagory do you want to play on: fitness, intelligence, friendlinees, or drool (f/i/r/d)?");
                char option = Console.ReadLine()[0];
                if (!"fird".Contains(option)) {
                    Console.WriteLine("Invalid choice. Try again.");
                    continue;
                }

                Console.Write("The Computer's card is: ");
                foreach (string s in ComputersStack.Peek()) {
                    Console.Write(s + ", ");
                }
                Console.WriteLine();

                int index = 0;
                switch (option)
                {
                    case 'f':
                        index = 1;
                        break;
                    case 'i':
                        index = 2;
                        break;
                    case 'r':
                        index = 3;
                        break;
                    case 'd':
                        index = 4;
                        break;
                }

                if (Convert.ToInt32(ComputersStack.Peek()[index]) >
                    Convert.ToInt32(PlayerStack.Peek()[index]))
                {
                    Console.WriteLine("Computer won.");
                    ComputersStack.Enqueue(PlayerStack.Dequeue());
                }
                else
                {
                    Console.WriteLine("You won!!");
                    PlayerStack.Enqueue(ComputersStack.Dequeue());
                }

                Console.WriteLine(
                    $"You have {PlayerStack.Count} cards. The computer has {ComputersStack.Count} cards."
                );
            }
        }

        static void Main(string[] args)
        {
            string filename = "dogsData.txt";
            string[,] topTrumps = new string[size_y, size_x];
            LoadData(filename, topTrumps);

            Console.WriteLine("Welcome to the Famous Dogs game!");
            Console.WriteLine("What is your name? ");
            string name = Console.ReadLine();
            Console.WriteLine($"Hello, {name}");

            string option = "";
            // Only loop if the option is random or not allowed
            while (true)
            {
                Console.WriteLine(
                    "What do you want to do? \n" +
                    " > play (p)\n" +
                    " > random dog (r)\n" +
                    " > quit (q)"
                );
                option = Console.ReadLine().ToLower();
                if (option == "q") {
                    break;
                } else if (option == "r") {
                    PrintRandomDog(topTrumps);
                } else if (option == "p") {
                    PlayGame(topTrumps);
                    break;
                } else {
                    Console.WriteLine("Invalid option.. Try again.");
                }
            }

            Console.WriteLine("Goodbye...");
            Console.ReadLine();
        }
    }
}