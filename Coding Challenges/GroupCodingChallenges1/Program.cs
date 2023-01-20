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
    }
}