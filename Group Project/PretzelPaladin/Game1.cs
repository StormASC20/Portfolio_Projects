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

    public enum Result
    {
        Victory,
        Defeat
    }
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private SpriteFont menuFont;
        private SpriteFont regularSizeFont;
        private SpriteFont subHeaderFont;
        private GameState state;
        private Result endResult;
        //private Texture2D pretzelButton;
        private Texture2D foodCourt;
        private Texture2D rectangleTexture;
        private Texture2D startImg;
        private Texture2D attackImg;
        private Texture2D defendImg;
        private Texture2D pretzelPaladinConceptImg;

        private Button startbutton;
        private Button attack;
        private Button defend;
        Button lastPressed;
        Move lastMove;


        private MouseState msState;
        private MouseState prevMouseState;

        private List<Move> moves;

        private int screenWidth;
        private int screenHeight;
        private Rectangle rectLocation;
        private int yOffset;

        Button topLeftMove;
        Button topRightMove;
        Button bottomLeftMove;
        Button bottomRightMove;

        Enemy enemy;
        Player player;

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
            endResult = Result.Victory;
            _graphics.PreferredBackBufferHeight = 700;
            _graphics.PreferredBackBufferWidth = 1100;

            screenHeight = _graphics.PreferredBackBufferHeight;
            screenWidth = _graphics.PreferredBackBufferWidth;

            moves = new List<Move>();
            FileReading("MoveList.txt");
            lastPressed = null;

            enemy = new Enemy(rectangleTexture, "Test Enemy", 100, 100, 1, 1);
            player = new Player(rectangleTexture, "Test Player", 100, 100, 1, 1);
            lastMove = new Move(" ", 0);

            topLeftMove = new Button();
            topRightMove = new Button();
            bottomLeftMove = new Button();
            bottomRightMove=new Button();

            

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

            pretzelPaladinConceptImg = this.Content.Load<Texture2D>("PretzelPaladin");

            startbutton      = new Button((_graphics.PreferredBackBufferWidth / 2)-100, (_graphics.PreferredBackBufferHeight / 3)+170, 200, 100, startImg);
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

                        // Inflicts Damage to enemy based on move chosen
                        if(topLeftMove.IsPressed())
                        {
                            for(int i=0; i<moves.Count; i++)
                            {
                                if(moves[i].MoveName==topLeftMove.Text)
                                {
                                    lastMove = moves[i];
                                    enemy.TakeDamage(moves[i].AmountDamage);
                                    break;
                                }
                            }
                        }
                        else if(topRightMove.IsPressed())
                        {
                            for (int i = 0; i < moves.Count; i++)
                            {
                                if (moves[i].MoveName == topRightMove.Text)
                                {
                                    lastMove = moves[i];
                                    enemy.TakeDamage(moves[i].AmountDamage);
                                    break;
                                }
                            }
                        }
                        else if (bottomLeftMove.IsPressed())
                        {
                            for (int i = 0; i < moves.Count; i++)
                            {
                                if (moves[i].MoveName == bottomLeftMove.Text)
                                {
                                    lastMove = moves[i];
                                    enemy.TakeDamage(moves[i].AmountDamage);
                                    break;
                                }
                            }
                        }
                        else if (bottomRightMove.IsPressed())
                        {
                            for (int i = 0; i < moves.Count; i++)
                            {
                                if (moves[i].MoveName == bottomRightMove.Text)
                                {
                                    lastMove = moves[i];
                                    enemy.TakeDamage(moves[i].AmountDamage);
                                    break;
                                }
                            }
                        }
                        // When the player or enemies health go below or equal to 0 the game is over 
                        if (player.CurrentHealth <= 0)
                        {
                            endResult = Result.Defeat;
                            state = GameState.GameOver;
                        }
                        if (enemy.CurrentHealth <= 0)
                        {
                            endResult = Result.Victory;
                            state = GameState.GameOver;
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
                        if(player.CurrentHealth <= 0)
                        {
                            endResult = Result.Defeat;
                        }
                        if(enemy.CurrentHealth <= 0)
                        {
                            endResult = Result.Victory;
                        }
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
                        _spriteBatch.Draw(
                            pretzelPaladinConceptImg,
                            new Rectangle(610,100,screenWidth/2,screenHeight),
                            Color.White);

                        _spriteBatch.Draw(
                            pretzelPaladinConceptImg,
                            new Rectangle(-50, 100, screenWidth / 2, screenHeight),
                            Color.White);

                        _spriteBatch.DrawString(
                            menuFont,
                            "PRETZEL PALADIN",
                            new Vector2(270,100),
                            Color.SaddleBrown);

                        startbutton.Draw(_spriteBatch, Color.White);
                        break;
                    }
                case GameState.Game:
                    {

                        yOffset = 95;

                        _spriteBatch.Draw(rectangleTexture, 
                            rectLocation,
                            Color.White);

                        _spriteBatch.DrawString(regularSizeFont,
                                  $"Player Health: {player.CurrentHealth}/{player.MaxHealth}",
                                  new Vector2(100, 70),
                                  Color.Firebrick);
                        _spriteBatch.DrawString(regularSizeFont,
                            $"Enemy Health: {enemy.CurrentHealth}/{enemy.MaxHealth}",
                            new Vector2(100, 90),
                            Color.Firebrick);

                        if (attack.Enabled)
                        {
                            attack.Draw(_spriteBatch, Color.White);
                        }
                        

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
                            topLeftMove = new Button(rectLocation.X + 70, rectLocation.Y + yOffset, rectLocation.Width / 3, rectLocation.Height / 6, rectangleTexture, moves[0].MoveName);
                            topRightMove = new Button(rectLocation.X + 275, rectLocation.Y + yOffset, rectLocation.Width / 3, rectLocation.Height / 6, rectangleTexture, moves[1].MoveName);
                            bottomLeftMove = new Button(rectLocation.X + 70, rectLocation.Y + yOffset + 130, rectLocation.Width / 3, rectLocation.Height / 6, rectangleTexture, moves[2].MoveName);
                            bottomRightMove = new Button(rectLocation.X + 275, rectLocation.Y + yOffset + 130, rectLocation.Width / 3, rectLocation.Height / 6, rectangleTexture, moves[3].MoveName);


                            // Draws button to screen
                            topLeftMove.DrawWithText(_spriteBatch, Color.Red, subHeaderFont, rectangleTexture);
                            topRightMove.DrawWithText(_spriteBatch, Color.Red, subHeaderFont, rectangleTexture);
                            bottomLeftMove.DrawWithText(_spriteBatch, Color.Red, subHeaderFont, rectangleTexture);
                            bottomRightMove.DrawWithText(_spriteBatch, Color.Red, subHeaderFont, rectangleTexture);

                            if (topLeftMove.IsPressed())
                            {
                                lastPressed = topLeftMove;

                                for (int i = 0; i < moves.Count; i++)
                                {
                                    if (moves[i].MoveName == topLeftMove.Text)
                                    {
                                        _spriteBatch.DrawString(
                                            regularSizeFont,
                                            $"{player.Name} dealt {moves[i].AmountDamage} to {enemy.Name}", 
                                            new Vector2(100, 50),
                                            Color.Firebrick);
                                        break;
                                    }
                                }
                            }
                            else if (topRightMove.IsPressed())
                            {
                                lastPressed = topRightMove;

                                for (int i = 0; i < moves.Count; i++)
                                {
                                    if (moves[i].MoveName == topRightMove.Text)
                                    {
                                        lastMove = moves[i];
                                        break;
                                    }
                                }
                            }
                            else if (bottomLeftMove.IsPressed())
                            {
                                lastPressed = bottomLeftMove;

                                for (int i = 0; i < moves.Count; i++)
                                {
                                    if (moves[i].MoveName == bottomLeftMove.Text)
                                    {
                                        lastMove = moves[i];
                                        break;
                                    }
                                }
                            }
                            else if (bottomRightMove.IsPressed())
                            {
                                lastPressed = bottomRightMove;

                                for (int i = 0; i < moves.Count; i++)
                                {
                                    if (moves[i].MoveName == bottomRightMove.Text)
                                    {
                                        lastMove = moves[i];
                                        break;
                                    }
                                }
                            }

                            if (lastPressed != null)
                            {
                                _spriteBatch.DrawString(
                                    regularSizeFont,
                                    $"Paladin used {lastPressed.Text}",
                                    new Vector2(100, 30),
                                    Color.Firebrick);
                                _spriteBatch.DrawString(
                                           regularSizeFont,
                                           $"{player.Name} dealt {lastMove.AmountDamage} to {enemy.Name}",
                                           new Vector2(100, 50),
                                           Color.Firebrick);
                            }
                            


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
                        if (endResult == Result.Defeat)
                        {
                            _spriteBatch.DrawString(
                            menuFont,
                            "YOU SUCK CHUMP",
                            new Vector2(_graphics.PreferredBackBufferWidth / 6, _graphics.PreferredBackBufferHeight / 4),
                            Color.SaddleBrown);
                        }
                        if(endResult == Result.Victory)
                        {
                            _spriteBatch.DrawString(
                            menuFont,
                            "Congratulations You Win",
                            new Vector2(_graphics.PreferredBackBufferWidth / 6, _graphics.PreferredBackBufferHeight / 4),
                            Color.SaddleBrown);
                        }
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
            if(msState.LeftButton==ButtonState.Pressed && prevMouseState.LeftButton==ButtonState.Released)
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