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
    // Class for the moves a character/ enemy can do 
    internal class Move
    {
        // Fields --
        private string moveName;
        private string description;
        private int amtDamage;

        // Properties --

        /// <summary>
        /// Name of the Move
        /// </summary>
        public string MoveName { get { return moveName; } }

        /// <summary>
        /// Amount of Damage Inflicted
        /// </summary>
        public int AmountDamage { get { return amtDamage; } }

        /// <summary>
        /// The description of the move that may contain special traits
        /// </summary>
        public string Description { get { return description; } }

        // Constructor --

        /// <summary>
        /// Creates a new move
        /// </summary>
        /// <param name="moveName">Name of the move</param>
        /// <param name="amtDamage">Amount of damage the move deals</param>
        /// <param name="moveLimit">Number of times you can use the move</param>
        public Move(string moveName, int amtDamage, string description)
        {
            this.moveName = moveName;
            this.amtDamage = amtDamage;
            this.description = description;
        }

        /// <summary>
        /// String description of each move
        /// </summary>
        /// <returns>A string...</returns>
        public override string ToString()
        {
            // Mainly used for debugging purposes
            return $"{moveName} -- Damage: " + amtDamage;
        }

    }
}
