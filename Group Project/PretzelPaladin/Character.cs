using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PretzelPaladin
{
    internal class Character 
    {
        public string name;
        public int maxHealth;
        public int currentHealth;
        public Texture2D image;
        //move array here

        public Character(string name, int maxHealth, int currentHealth, Texture2D image)        {
            this.name = name;
            this.maxHealth = maxHealth;
            this.currentHealth = currentHealth;
            this.image = image;
        }
        
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

        public virtual void Draw(SpriteBatch sb)
        {
            //PP and enemies will always have the same x & y positions during fights so it'll be better to
            //establish that within those classes
        }

    }
}
