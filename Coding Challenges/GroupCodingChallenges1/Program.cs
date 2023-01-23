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

            //Storm's Problem #1: Dice Sum

            Console.WriteLine("Problem 1: ");
            Console.WriteLine();

            //Ask the user what number they want to get with the two dice
            Console.Write("Choose your desired sum! ");
            string input = Console.ReadLine();
            int desiredSum = Convert.ToInt32(input);

            //checks to see if the desired sum is between the valid ranges, 2 and 12
            //I mean, you only have two six sided die, you goofball
            while (desiredSum < 2 || desiredSum > 12)
            {
                Console.Write("Invalid sum! Valid sums are between 2 and 12. ");
                input = Console.ReadLine();
                desiredSum = Convert.ToInt32(input);
            }

            Console.WriteLine();
            DiceSum(desiredSum);

            Console.WriteLine();
            Console.WriteLine("Problem 3: ");

            // Testing for Problem 3 -- Palindrome
            Console.WriteLine("\nPalindrome Test: ");

            // Represents a case that should return true
            string testWord1 = "Pop";
            string testWord2 = "Carrot";
            if (IsPalindrome("Pop"))
            {
                Console.WriteLine(testWord1 + " is a palindrome.");
            }
            else
            {
                Console.WriteLine(testWord1 + " is NOT a palindrome.");
            }

            if (IsPalindrome(testWord2))
            {
                Console.WriteLine(testWord2 + " is a palindrome.");
            }
            else
            {
                Console.WriteLine(testWord2 + " is NOT palindrome.");
            }

            Console.WriteLine();

            Console.WriteLine("Problem 4: ");
            Console.WriteLine();

            //Code for problem 4
            int[] array1 = { 3, 8, 10, 1, 9, 14, -3, 0, 14, 207, 56, 98 };
            int[] array2 = { 17, 42, 3, 5, 5, 5, 8, 2, 4, 6, 1, 19 };
            Console.WriteLine("The longest sorted sequence in array1 is " + longestSortedSequence(array1) + " long.");
            Console.WriteLine("The longest sorted sequence in array2 is " + longestSortedSequence(array2) + " long.");

            Console.WriteLine();

            Console.WriteLine("Problem 5: ");
            Console.WriteLine();

            //Code for problem 5 
            int[] nums = { 2, 2, 4, 10, 10, 10, 4, 2, 2, 2, 4 };
            int[] nums2 = { 5, 2, 4, 4, 6, 6, 6, 7, 7, 7, 1, 2 };
            Console.WriteLine("The array 2, 2, 4, 10, 10, 10, 4, 2, 2, 2, 4 has a duplicate chain of " + GetLongestDuplicate(nums));
            Console.WriteLine("The array 5, 2, 4, 4, 6, 6, 6, 7, 7, 7, 1, 2  has a duplicate chain of " + GetLongestDuplicate(nums2));
        }

        /// <summary>
        /// Rolls two six sided die and stops when the desired sum is reached
        /// </summary>
        /// <param name="userSum">the user's desired sum of the die</param>
        public static void DiceSum(int userSum)
        {
            int diceSum = 0;

            //keeps going until the user's sum and dice sum are equal
            while (diceSum != userSum)
            {
                Random random = new Random();
                int dice1 = random.Next(1, 7);
                int dice2 = random.Next(1, 7);

                diceSum = dice1 + dice2;

                Console.WriteLine($"{dice1} and {dice2} = {diceSum}");
            }
        }

        /// <summary>
        /// Problem 3: Determines if a word is a palindrome or not
        /// </summary>
        /// <param name="word">The word in question</param>
        /// <returns>True if thr given word is a palindrome, false otherwise</returns>
        public static bool IsPalindrome(string word)
        {
            string newWord = "";
            char letter;

            // Creates a new string that represents the word spelled backwards
            for (int i = word.Length; i > 0; i--)
            {
                letter = word[i - 1];
                newWord += letter;
            }

            // Compares the new string to the original to see if it was a palindrome
            if (word.ToLower() == newWord.ToLower())
            {
                return true;
            }

            return false;

        }
    


        /// <summary>
        /// Code Author: David Shaffer
        /// Find the longest chain of duplicate values in the array 
        /// </summary>
        /// <param name = "num" > Array of numbers we are looking through</param>
        /// <returns>The number that has the longest chain</returns>
        public static int GetLongestDuplicate(int[] num)
        {

            // Variable for the counter of the longest chain

            int chainCounter = 0;

            // Variable for new chain counter

            int newCounter = 0;
            //        Variable for the number that has the longest chain

            int chainNumber = 0;
            int newChainNumber = 0;

            //For loop to go through the array of numbers
            for (int i = 0; i < num.Length; i++)
            {

                //See if the number we are looking for in the chain is the same
                if (chainNumber == num[i])
                {

                    chainCounter += 1;
                    newCounter = 0;
                }
                //If add to the new counter and reset the chain number we are looking for
                else
                {
                    chainNumber = num[i];
                    newCounter += 1;

                }
                //If the new chain number is larger then change the number we will return and change the counter
                if (newCounter >= chainCounter)
                {
                    newChainNumber = chainNumber;

                    chainCounter = newCounter;

                }

            }
            //Return the number that had the largest chain
            return newChainNumber;

            int[] nums = { 2, 2, 4, 10, 10, 10, 4, 2, 2, 2, 4 };
            int[] nums2 = { 5, 2, 4, 4, 6, 6, 6, 7, 7, 7, 1, 2 };
            Console.WriteLine(GetLongestDuplicate(nums));
            Console.WriteLine(GetLongestDuplicate(nums2));
        }




        /// <summary>
        /// Code Author: Mackenna Roberts
        /// Problem #4: Checks an integer array for its longest sequence of ascending numbers and returns its length
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static int longestSortedSequence(int[] array)
        {
            int longestSequence = 0;
            int tempLongest = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (i == array.Length - 1)
                {
                    break;
                }
                else
                {
                    if (array[i] <= array[i + 1])
                    {
                        tempLongest++;
                    }
                    else
                    {
                        tempLongest++;
                        if (tempLongest > longestSequence)
                        {
                            longestSequence = tempLongest;
                        }
                        tempLongest = 0;
                    }
                }
            }
            return longestSequence;
        }
    }
}
