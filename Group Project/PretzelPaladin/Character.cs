using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace PretzelPaladin
{
    internal class Character
    {
        // Fields --
        string name;
        int maxHealth;
        int currentHealth;

        int attackMultiplier;
        int defenseMultiplier;

        private Texture2D characterImage;
        //move array here

        public Character(Texture2D characterImage, string name, int maxHealth, int currentHealth, int attackMultiplier, int defenseMultiplier)
        {
            this.name = name;
            this.maxHealth = maxHealth;
            this.currentHealth = currentHealth;
            this.attackMultiplier = attackMultiplier;
            this.defenseMultiplier = defenseMultiplier;
            this.characterImage = characterImage;
        }

        // Properties --

        /// <summary>
        /// Name of the character
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// Character's Max/Starting health
        /// </summary>
        public int MaxHealth
        {
            get { return maxHealth; }
            set { maxHealth = value; }
        }

        /// <summary>
        /// Current health of the character
        /// </summary>
        public int CurrentHealth
        {
            get { return currentHealth; }
            set { currentHealth = value; }
        }

        /// <summary>
        /// Attack Multiplier that increases damage
        /// </summary>
        public int AttackMultiplier { get { return attackMultiplier; } set { attackMultiplier = value; } }

        /// <summary>
        /// Defense Multiplier that reduces incoming damage
        /// </summary>
        public int DefenseMultiplier { get { return defenseMultiplier; } set { defenseMultiplier = value; } }

        // Methods --

        /// <summary>
        /// Character takes damage from another, the amount of which could be 
        /// possibly reduced by their defense multiplier
        /// </summary>
        /// <param name="amtDamage">Amount of damage inflicted</param>
        public  void TakeDamage(int amtDamage)
        {
            currentHealth -= amtDamage*defenseMultiplier;
        }


    }
}

