// Mackenna, David, Storm, and Ben
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