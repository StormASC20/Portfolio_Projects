using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PretzelPaladin
{
    internal class Character
    {
        private string name;
        private int maxHealth;
        private int currentHealth;
        //move array here

        public Character(string name, int maxHealth, int currentHealth)
        {
            this.name = name;
            this.maxHealth = maxHealth;
            this.currentHealth = currentHealth;
        }
        
        public string Name
        {
            get { return name; }
            set { name = value; }
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


    }
}
