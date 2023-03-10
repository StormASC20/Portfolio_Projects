using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;

namespace PretzelPaladin
{
    public enum GameState
    {
        MainMenu,
        Game,
        Pause,
        GameOver
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private SpriteFont menuFont;
        private SpriteFont regularSizeFont;
        private SpriteFont subHeaderFont;
        private GameState state;

        //private Texture2D pretzelButton;
        private Texture2D foodCourt;
        private Texture2D rectangleTexture;
        private Texture2D startImg;
        private Texture2D attackImg;
        private Texture2D defendImg;

        private Button startbutton;
        private Button attack;
        private Button defend;

        
        private MouseState msState;
        private MouseState prevMouseState;

        private List<Move> moves;

        private int screenWidth;
        private int screenHeight;
        private Rectangle rectLocation;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            // THIS IS SCARY, DON'T USE (yet)
            //_graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
           //_graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
           //_graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            state = GameState.MainMenu;
            _graphics.PreferredBackBufferHeight = 700;
            _graphics.PreferredBackBufferWidth = 1100;

            screenHeight = _graphics.PreferredBackBufferHeight;
            screenWidth = _graphics.PreferredBackBufferWidth;

            moves = new List<Move>();
            FileReading("MoveList.txt");

            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch     = new SpriteBatch(GraphicsDevice);
            menuFont         = this.Content.Load<SpriteFont>("MenuFont");
            regularSizeFont  = this.Content.Load<SpriteFont>("NormalFontSize");
            subHeaderFont    = this.Content.Load<SpriteFont>("Subheader");

            foodCourt        = this.Content.Load<Texture2D>("foodCourt");
            startImg         = this.Content.Load<Texture2D>("startButton");
            attackImg        = this.Content.Load<Texture2D>("attackButton");
            defendImg        = this.Content.Load<Texture2D>("defendButton");
            rectangleTexture = this.Content.Load<Texture2D>("Rectangle");

            startbutton      = new Button((_graphics.PreferredBackBufferWidth / 3), _graphics.PreferredBackBufferHeight / 2, 200, 100, startImg);
            attack           = new Button((screenWidth) - 325, screenHeight-200, 200, 100, attackImg);
            //defend = new Button((screenWidth / 2) + 75, (screenHeight / 2) + 125, 200, 100, defendImg);

            rectLocation     = new Rectangle((screenWidth / 2 + 50), screenHeight / 2, screenWidth / 2, screenHeight);
            

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.M))
                Exit();

            KeyboardState kbState = Keyboard.GetState();

            msState = Mouse.GetState();

            screenHeight = _graphics.PreferredBackBufferHeight;
            screenWidth = _graphics.PreferredBackBufferWidth;

            switch(state)
            {
                case GameState.MainMenu:
                    {
                        if(startbutton.IsPressed())
                        {
                            state = GameState.Game;
                        }
                        if (kbState.IsKeyDown(Keys.G))
                        {
                            state = GameState.GameOver;
                        }
                        if(kbState.IsKeyDown(Keys.Escape))
                        {
                            state = GameState.Pause;
                        }

                        break;
                    }
                case GameState.Game:
                    {
                        // Pauses the Game if the user presses the ESCAPE key
                        if(kbState.IsKeyDown(Keys.Escape))
                        {
                            state = GameState.Pause;
                        }
                        
                        if(attack.IsPressed())
                        {
                            attack.Enabled = false;
                        }

                        break;
                    }
                case GameState.Pause:
                    {
                        if (kbState.IsKeyDown(Keys.Enter))
                        {
                            state = GameState.Game;
                        }
                        break;
                    }
                case GameState.GameOver:
                    {
                        if (kbState.IsKeyDown(Keys.Space))
                        {
                            state = GameState.MainMenu;
                        }
                        if (kbState.IsKeyDown(Keys.Escape))
                        {
                            state = GameState.Pause;
                        }
                        break;
                    }
            }

            prevMouseState = Mouse.GetState();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Beige);

            _spriteBatch.Begin();

            switch (state)
            {
                case GameState.MainMenu:
                    {
                        _spriteBatch.DrawString(
                            menuFont,
                            "PRETZEL PALADIN",
                            new Vector2(_graphics.PreferredBackBufferWidth / 6, _graphics.PreferredBackBufferHeight / 4),
                            Color.SaddleBrown);
                        startbutton.Draw(_spriteBatch, Color.White);
                        break;
                    }
                case GameState.Game:
                    {
                        int yOffset = 95;

                        _spriteBatch.Draw(rectangleTexture, 
                            rectLocation,
                            Color.White);

                        attack.Draw(_spriteBatch, Color.White);

                        // Displays moves after Attack button is pressed
                        if(attack.Enabled==false)
                        {
                            _spriteBatch.DrawString(subHeaderFont, "Moves:", new Vector2(rectLocation.X+30,rectLocation.Y+37), Color.DarkRed);

                            //foreach(Move m in moves)
                            //{
                            //    _spriteBatch.DrawString(regularSizeFont, m.ToString(), new Vector2(rectLocation.X + 40, rectLocation.Y + yOffset), Color.DarkRed);
                            //    yOffset += 20;
                            //}

                            // Creates 4 Attack buttons
                            Button topLeftMove = new Button(rectLocation.X+70,rectLocation.Y+yOffset,rectLocation.Width/3,rectLocation.Height/6, rectangleTexture);
                            Button topRightMove = new Button(rectLocation.X + 275, rectLocation.Y + yOffset, rectLocation.Width / 3, rectLocation.Height / 6, rectangleTexture);
                            Button bottomLeftMove = new Button(rectLocation.X + 70, rectLocation.Y + yOffset+130, rectLocation.Width / 3, rectLocation.Height / 6, rectangleTexture);
                            Button bottomRightMove = new Button(rectLocation.X + 275, rectLocation.Y + yOffset+130, rectLocation.Width / 3, rectLocation.Height / 6, rectangleTexture);

                            // Draws button to screen
                            topLeftMove.Draw(_spriteBatch, Color.Red);
                            topRightMove.Draw(_spriteBatch, Color.Red);
                            bottomLeftMove.Draw(_spriteBatch, Color.Red);
                            bottomRightMove.Draw(_spriteBatch, Color.Red);
                        }

                        //defend.Draw(_spriteBatch);  
                        break;
                    }
                case GameState.Pause:
                    {
                        _spriteBatch.DrawString(
                            regularSizeFont,
                            "YOU PAUSED 'CAUSE UR SCURRED",
                            new Vector2(_graphics.PreferredBackBufferWidth / 4, _graphics.PreferredBackBufferHeight / 2),
                            Color.SaddleBrown);
                        break;
                    }
                case GameState.GameOver:
                    {

                        _spriteBatch.DrawString(
                            menuFont,
                            "YOU SUCK CHUMP",
                            new Vector2(_graphics.PreferredBackBufferWidth / 6, _graphics.PreferredBackBufferHeight / 4),
                            Color.SaddleBrown);
                        break;
                    }
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Recognizes a Single Mouse Click
        /// </summary>
        /// <returns>Returns true if the player just left-clicked once, false otherwise</returns>
        public bool SingleClick()
        {
            if(msState.LeftButton==ButtonState.Pressed&&prevMouseState.LeftButton==ButtonState.Released)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Reads in Moves from MoveList file
        /// </summary>
        /// <param name="fileName">Name of the file</param>
        public void FileReading(string fileName)
        {
            StreamReader file = new StreamReader("../../../Content/"+fileName);

            string currentLine;

            while((currentLine = file.ReadLine())!= null)
            {
                string[] components = currentLine.Split(",");

                moves.Add(new Move((components[0]), int.Parse(components[1])));
            }

            file.Close();
        }
    }
}