
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        //move array here

        public Character(string name, int maxHealth, int currentHealth, int attackMultiplier, int defenseMultiplier)
        {
            this.name = name;
            this.maxHealth = maxHealth;
            this.currentHealth = currentHealth;
            this.attackMultiplier = attackMultiplier;
            this.defenseMultiplier = defenseMultiplier;
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

        // Methods --
        public  void TakeDamage(int amtDamage)
        {
            currentHealth -= amtDamage;
        }


    }
}