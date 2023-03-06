using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PretzelPaladin
{
    internal class Enemy:Character
    {
        //fields
        private Random random;

        //constructor
        public Enemy(Texture2D characterImage, string name, int maxHealth, int currentHealth, int attackMultiplier, int defenseMultiplier)
            : base(characterImage, name, maxHealth, currentHealth, attackMultiplier, defenseMultiplier)
        {
            random = new Random();
        }

        //properties


        //methods
        
    }
}
