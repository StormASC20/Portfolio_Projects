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
    /// <summary>
    /// Mackenna Roberts
    /// 3/6/23
    /// Class designed to make a button
    /// </summary>
    internal class Button
    {
        private int x;
        private int y;
        private int width;
        private int height;
        private Rectangle rect;
        private Texture2D image;
        private MouseState ms;
        private MouseState lastMS;
        
        public Button(int x, int y, int width, int height, Texture2D image)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            rect = new Rectangle(x, y, width, height);
            this.image = image;
        }

        // Properties
        public Rectangle Location { get { return rect; } set { rect = value; } }

        public bool IsPressed()
        {
            ms = Mouse.GetState();
            if(ms.LeftButton == ButtonState.Pressed && ms.X > x && ms.X < x + width && ms.Y > y && ms.Y < y + height)
            {
                return true;
            }
            else 
            { 
                return false; 
            }
        }

        /*public bool SinglePress()
        {
            lastMS = ms;
            ms = Mouse.GetState();
            if (ms.LeftButton == ButtonState.Released && lastMS.LeftButton == ButtonState.Pressed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }*/

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(image,
                rect,
                Color.White);
        }
    }
}
