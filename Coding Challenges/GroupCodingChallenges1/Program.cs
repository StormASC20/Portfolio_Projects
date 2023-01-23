// Mackenna, David, Storm, and Ben
// 1/20/2023
// Practice with merging files within a shared repo

// This is Ben's comment
// Slay
//im the shabble wobble eeble dobble weeble dobble dee: Mackenna
//bapada boopy; Storm
// David Shaffer

namespace GroupCodingChallenges1
{
    internal class Program
    {
        

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            int[] num = { 4, 4, 4, 3, 3, 2, 1, 10 };
            Console.WriteLine(GetLongestDuplicate(num));

        }

        /// <summary>
        /// Find the longest chain of duplicate values in the array 
        /// </summary>
        /// <param name="num">Array of numbers we are looking through </param>
        /// <returns>The number that has the longest chain</returns>
        public static int GetLongestDuplicate(int[] num)
        {

            // Variable for the counter of the longest chain
            int chainCounter = 0;

            // Variable for new chain counter
            int newCounter = 0;
            // Variable for the number that has the longest chain
            int chainNumber = 0;
            int newChainNumber = 0;

            // For loop to go through the array of numbers 
            for(int i = 0; i < num.Length; i++)
            {
                chainNumber = num[i];

                if (chainNumber == num[i])
                {
                    
                    chainCounter += 1;
                    
                }
                else
                {
                    newChainNumber = num[i];
                    newCounter += 1;
                }
                if(newCounter >= chainCounter)
                {
                    chainNumber = newChainNumber;
                    chainCounter = newCounter;
                }

            }
            return chainNumber;
        }
    }

    // Dice roll - Storm 

    // Longest number chain - David 

    



    // Problem #4: Longest Sorted Sequence - Mckenna

    // Problem 3 - Ben 
}