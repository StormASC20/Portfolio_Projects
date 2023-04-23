using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PretzelPaladin
{
   internal class Player:Character
   {

        // Fields --

        // Constructor --

        /// <summary>
        /// Creates a new Player
        /// </summary>
        /// <param name="characterImage">Texture for the player</param>
        /// <param name="name">Player's Name</param>
        /// <param name="maxHealth">Starting/Maximum Health</param>
        /// <param name="currentHealth">Current health of the player</param>
        /// <param name="attackMultiplier">Multiplier that boosts attack damage</param>
        /// <param name="defenseMultiplier">Multiplier that reduces incoming damage</param>
        public Player(Texture2D characterImage, string name, int maxHealth, int currentHealth, int attackMultiplier, int defenseMultiplier)
            : base(characterImage, name, maxHealth, currentHealth, attackMultiplier, defenseMultiplier)
        {

        }

        // Properties --


        // Methods --

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
