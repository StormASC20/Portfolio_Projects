// Mackenna, David, Storm, and Ben
// 1/20/2023
// Practice with merging files within a shared repo
// This is Ben's comment
// Slay
//im the shabble wobble eeble dobble weeble dobble dee: Mackenna
//bapada boopy; Storm

// 



// David Shaffer
namespace GroupCodingChallenges1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            int[] array1 = { 3, 8, 10, 1, 9, 14, -3, 0, 14, 207, 56, 98 };
            int[] array2 = { 17, 42, 3, 5, 5, 5, 8, 2, 4, 6, 1, 19 };
            Console.WriteLine("The longest sorted sequence in array1 is " + longestSortedSequence(array1) + " long.");
            Console.WriteLine("The longest sorted sequence in array2 is " + longestSortedSequence(array2) + " long.");
        }

        public static int longestSortedSequence(int[] array)
        {
            int longestSequence = 0;
            int tempLongest = 0;
            for(int i = 0; i < array.Length; i++)
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