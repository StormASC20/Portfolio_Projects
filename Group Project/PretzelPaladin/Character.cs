
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
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

        Texture2D characterImage;
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

        public string Name
        {
            get { return name; }
        }

        public int MaxHealth
        {
            get { return maxHealth; }
            set { maxHealth = value; }
        }

        public int CurrentHealth
        {
            get { return currentHealth; }
            set { currentHealth = value; }
        }
        public int AttackMultiplier { get { return attackMultiplier; } set { attackMultiplier = value; } }
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

