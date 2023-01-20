﻿// Mackenna, David, Storm, and Ben
// 1/20/2023
// Practice with merging files within a shared repo
//im the shabble wobble eeble dobble weeble dobble dee
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

            // Testing for Problem 3 -- Palindrome
            Console.WriteLine("\nPalindrome Test: ");

            // Represents a case that should return true
            string testWord1 = "Pop";
            string testWord2 = "Carrot";
            if(IsPalindrome("Pop"))
            {
                Console.WriteLine(testWord1 + " is a palindrome.");
            }
            else
            {
                Console.WriteLine(testWord1 + " is NOT a palindrome.");
            }

            if(IsPalindrome(testWord2))
            {
                Console.WriteLine(testWord2 + " is a palindrome.");
            }
            else
            {
                Console.WriteLine(testWord2 + " is NOT palindrome.");
            }

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
            for(int i=word.Length; i>0; i--)
            {
                letter = word[i-1];
                newWord += letter;
            }

            // Compares the new string to the original to see if it was a palindrome
            if(word.ToLower()==newWord.ToLower())
            {
                return true;
            }

            return false;
        }
    }
}