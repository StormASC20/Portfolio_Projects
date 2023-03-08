using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;

namespace PretzelPaladin
{
    internal class Character
    {
        // Fields --
        private string name;
        private int maxHealth;
        private int currentHealth;
        private int attackMultiplier;
        private int defenseMultiplier;

        private List<Move> moves;  
        private Texture2D characterImage;

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

        // Constructor --

        /// <summary>
        /// Creates a new Character
        /// </summary>
        /// <param name="characterImage">Texture of the character</param>
        /// <param name="name">Character's name</param>
        /// <param name="maxHealth">Character's Max/Start health</param>
        /// <param name="currentHealth">Character's current health</param>
        /// <param name="attackMultiplier">Attack multiplier that boosts or reduces damage</param>
        /// <param name="defenseMultiplier">Defense multiplier that reduces or boosts incoming damage</param>
        public Character(Texture2D characterImage, string name, int maxHealth, int currentHealth, int attackMultiplier, int defenseMultiplier)
        {
            this.name = name;
            this.maxHealth = maxHealth;
            this.currentHealth = currentHealth;
            this.attackMultiplier = attackMultiplier;
            this.defenseMultiplier = defenseMultiplier;
            this.characterImage = characterImage;

            StreamReader file = new StreamReader("MoveList");

            string currentLine = file.ReadLine();

            while(currentLine!=null)
            {
                string[] components = currentLine.Split(",");

                Move move = new Move(components[0], int.Parse(components[1]));

                moves.Add(move);

                currentLine = file.ReadLine();
            }
        }

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

