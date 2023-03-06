using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PretzelPaladin
{
   internal class Player:Character
   {

        //fields

        //constructor
        public Player(string name, int maxHealth, int currentHealth, int attackMultiplier, int defenseMultiplier)
            : base(name, maxHealth, currentHealth, attackMultiplier, defenseMultiplier)
        {

        }

        //properties


        //methods

        public void Attack(Enemy target, int attackValue)
        {
            target.

            return attackValue;
        }


    }
}
