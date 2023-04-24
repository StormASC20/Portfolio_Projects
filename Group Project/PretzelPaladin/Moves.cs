using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PretzelPaladin
{
    // David Shaffer
    // 3/6/2023
    // Interface for the moves a character/ enemy can do 
    internal class Move
    {
        // Fields --
        private string moveName;
        private int maxDamage;
        private int maxMoveLimit;
        private int moveLimit;
        private int minDamage;
        private int amtDamage;
        // Properties --

        /// <summary>
        /// Name of the Move
        /// </summary>
        public string MoveName { get { return moveName; } }

        /// <summary>
        /// Amount of Damage Inflicted
        /// </summary>
        public int AmountDamage 
        { 
            get 
            {
                Random rng = new Random();
                amtDamage = rng.Next(minDamage, maxDamage);
                return amtDamage;
            } 
        }
 
        public int MaxDamage
        {
            get
            {
                return maxDamage;
            }
        }

        public int MinDamage
        {
            get
            {
                return minDamage;
            }
        }

        public int MoveLimit { get { return moveLimit; } set { moveLimit = value; } }

        public int MaxMoveLimit { get { return maxMoveLimit; } }

        // Constructor --

        /// <summary>
        /// Creates a move from the move file
        /// </summary>
        /// <param name="moveName">Move name</param>
        /// <param name="minDamage">minimum damage it can do</param>
        /// <param name="maxDamage">maximum damage it can do</param>
        /// <param name="moveLimit">move limit of the move</param>
        public Move(string moveName, int minDamage,int maxDamage, int moveLimit)
        {
            this.moveName = moveName;
            this.minDamage = minDamage;
            this.maxDamage = maxDamage;
            this.moveLimit = moveLimit;
            maxMoveLimit = moveLimit;
        }

        /// <summary>
        /// String description of each move
        /// </summary>
        /// <returns>A string...</returns>
        public override string ToString()
        {
            return $"{moveName} -- Damage: " + amtDamage;
        }

    }
}
