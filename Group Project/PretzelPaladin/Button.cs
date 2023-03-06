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
    internal class Button
    {
        private MouseState ms;
        private MouseState lastMS;
        private int x;
        private int y;
        private int width;
        private int height;
        private Rectangle rect;

        public Button(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            rect = new Rectangle(x, y, width, height);
        }

        public bool IsPressed()
        {
            if(SinglePress() && ms.X > x && ms.X < x + width && ms.Y > y && ms.Y < y + height)
            {
                return true;
            }
            else 
            { 
                return false; 
            }
        }

        public bool SinglePress()
        {
            lastMS = ms;
            ms = new MouseState();
            if(ms.LeftButton == ButtonState.Released && lastMS.LeftButton == ButtonState.Pressed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
