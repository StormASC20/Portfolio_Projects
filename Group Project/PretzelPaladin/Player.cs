using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PretzelPaladin
{
   internal class Player:Character
   {

        //fields

        //constructor
        public Player(Texture2D characterImage, string name, int maxHealth, int currentHealth, int attackMultiplier, int defenseMultiplier)
            : base(characterImage, name, maxHealth, currentHealth, attackMultiplier, defenseMultiplier)
        {

        }

        //properties


        //methods

        /// <summary>
        /// Calls an attack by the player,
        /// deals damage based on a few factors, like
        /// current attack buff or debuff
        /// </summary>
        /// <param name="target">target of the attack</param>
        /// <param name="attackValue">damage to be done on the target's health value</param>
        public void Attack(Enemy target, int attackValue)
        {
            int trueAttackValue = attackValue * this.AttackMultiplier;

            target.TakeDamage(attackValue);
        }


    }
}
