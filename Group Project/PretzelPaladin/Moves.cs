using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PretzelPaladin
{
    // David Shaffer
    // 3/6/2023
    // Interface for the moves a character/ enemy can do 
    internal interface Moves
    {
        /// <summary>
        /// Move that attacks the enemy
        /// </summary>
        public double Attack { get; set; }

        /// <summary>
        /// Move that is defensive
        /// </summary>
        public double Defense { get; set; }

        /// <summary>
        /// Property for us to get the amount of damage we have done/ will do
        /// </summary>
        public double AttackDamage { get; }
        
        /// <summary>
        /// Property for us to see what stat is buffed/ what has happened after we do an non damaging move
        /// </summary>
        public double StatBuff { get; }
    }
}
