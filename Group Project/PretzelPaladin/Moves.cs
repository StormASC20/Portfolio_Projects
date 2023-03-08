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

        // Constructor --

        /// <summary>
        /// Creates a new Move that a character can use
        /// </summary>
        /// <param name="moveName">Name of the move</param>
        /// <param name="amtDamage">Amount of damage dealt by the move</param>
        public Move(string moveName, int amtDamage)
        {
            this.moveName = moveName;
            this.amtDamage = amtDamage;
        }
   
    }
}
