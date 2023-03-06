using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PretzelPaladin
{
    internal class Enemy : Character
    {
        public Enemy(string name, int maxHealth, int currentHealth) : base(name, maxHealth, currentHealth)
        {
        }
    }
}
