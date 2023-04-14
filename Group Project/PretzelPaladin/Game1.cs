using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

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
        private Texture2D sbarroSamuraiTexture;
        private Texture2D pretzelPaladinBackTextture;
        private Texture2D pretzelCursor;

        private Button startbutton;
        private Button attack;
        private Button defend;
        private Button lastPressed;

        private Move lastMove;
        private Move enemyMove;
        private Move bossMove;
        private bool attackPressed;
        private Stopwatch timer;
        private bool playerTurn;
        private Random rng;


        private MouseState msState;
        private MouseState prevMouseState;

        private List<Move> moves;
        private List<Enemy> enemies;

        private int screenWidth;
        private int screenHeight;
        private Rectangle rectLocation;
        private int yOffset;

        Button topLeftMove;
        Button topRightMove;
        Button bottomLeftMove;
        Button bottomRightMove;

        Enemy enemy;
        Enemy enemy2;
        Enemy enemy3;
        Player player;
        Enemy boss;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            // THIS IS SCARY, DON'T USE (yet)
            //_graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
           //_graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
           //_graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
            //IsMouseVisible = true;
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

            enemies = new List<Enemy>();

            enemy = new Enemy(rectangleTexture, "Sbarro Samurai", 100, 100, 1, 1);
            enemy2 = new Enemy(rectangleTexture, "Sbarro Samurai", 50, 50, 1, 1);
            enemy3 = new Enemy(rectangleTexture, "Sbarro Samurai", 25, 25, 1, 1);
            player = new Player(rectangleTexture, "Pretzel Paladin", 100, 100, 1, 1);

            boss = new Enemy(rectangleTexture, "Sbarro Samurai", 300, 300, 1, 2);

            lastMove = new Move(" ", 0,0);
            enemyMove = new Move(" ", 0,0);
            bossMove = new Move(" ", 0, 0);
            topLeftMove = new Button();
            topRightMove = new Button();
            bottomLeftMove = new Button();
            bottomRightMove=new Button();

            attackPressed = false;
            timer = new Stopwatch();
            playerTurn = true;
            rng = new Random();

            //enemies.Add(enemy);
            //enemies.Add(enemy2);
            enemies.Add(enemy3);

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
            pretzelCursor = this.Content.Load<Texture2D>("pretzel");

            pretzelPaladinConceptImg   = this.Content.Load<Texture2D>("PretzelPaladin");
            sbarroSamuraiTexture       = this.Content.Load<Texture2D>("Sbarro Samurai");
            pretzelPaladinBackTextture = this.Content.Load<Texture2D>("PretzelPaladin Back Image");

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
                        startbutton.Enabled = true;

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
                        // Limits the attack button to only be clickable once
                        if(attackPressed==true)
                        {
                            attack.Enabled = false;
                            attackPressed = false;
                        }

                        if (attack.Enabled == false && playerTurn == true)
                        {
                            topLeftMove.Enabled = true;
                            topRightMove.Enabled = true;
                            bottomLeftMove.Enabled = true;
                            bottomRightMove.Enabled = true;
                        }

                        // Pauses the Game if the user presses the ESCAPE key
                        if (kbState.IsKeyDown(Keys.Escape))
                        {
                            state = GameState.Pause;
                        }
                        
                        if(attack.IsPressed())
                        {
                            attack.Enabled = false;
                        }

                        for (int i = 0; i < enemies.Count; i++)
                        {


                            // Inflicts Damage to enemy based on move chosen
                            if (topLeftMove.IsPressed())
                            {
                                lastPressed = topLeftMove;
                                lastMove = topLeftMove.Move;
                                enemies[i].TakeDamage(topLeftMove.Damage);

                                topLeftMove.Enabled = false;
                                topRightMove.Enabled = false;
                                bottomLeftMove.Enabled = false;
                                bottomRightMove.Enabled = false;

                                timer.Restart();

                                playerTurn = false;
                                timer.Restart();
                            }
                            else if (topRightMove.IsPressed())
                            {
                                lastPressed = topRightMove;
                                lastMove = topRightMove.Move;
                                enemies[i].TakeDamage(topRightMove.Damage);

                                topLeftMove.Enabled = false;
                                topRightMove.Enabled = false;
                                bottomLeftMove.Enabled = false;
                                bottomRightMove.Enabled = false;

                                timer.Restart();

                                playerTurn = false;
                                timer.Restart();

                            }
                            else if (bottomLeftMove.IsPressed())
                            {
                                lastPressed = bottomLeftMove;
                                lastMove = bottomLeftMove.Move;
                                enemies[i].TakeDamage(bottomLeftMove.Damage);

                                topLeftMove.Enabled = false;
                                topRightMove.Enabled = false;
                                bottomLeftMove.Enabled = false;
                                bottomRightMove.Enabled = false;

                                timer.Restart();

                                playerTurn = false;
                                timer.Restart();

                            }
                            else if (bottomRightMove.IsPressed())
                            {
                                lastPressed = bottomRightMove;
                                lastMove = bottomRightMove.Move;
                                enemies[i].TakeDamage(bottomRightMove.Damage);

                                topLeftMove.Enabled = false;
                                topRightMove.Enabled = false;
                                bottomLeftMove.Enabled = false;
                                bottomRightMove.Enabled = false;

                                timer.Restart();

                                playerTurn = false;
                                timer.Restart();
                            }

                            if (playerTurn == false)
                            {
                                enemyMove = moves[rng.Next(0, moves.Count)];
                            }

                            if(playerTurn ==false && timer.ElapsedMilliseconds>=2000)
                            {
                                enemyMove = moves[rng.Next(0, moves.Count)];

                                    player.TakeDamage(enemyMove.AmountDamage);

                                playerTurn = true;
                            }
                        }
                     
                        // Boss fight when all enemies are dead
                        if (enemies.Count <= 0 && boss.CurrentHealth >= 0)
                        {
                            // Inflicts Damage to enemy based on move chosen
                            if (topLeftMove.IsPressed())
                            {
                                lastPressed = topLeftMove;
                                lastMove = topLeftMove.Move;
                                boss.TakeDamage(topLeftMove.Damage);

                                topLeftMove.Enabled = false;
                                topRightMove.Enabled = false;
                                bottomLeftMove.Enabled = false;
                                bottomRightMove.Enabled = false;

                                timer.Restart();

                                playerTurn = false;
                                timer.Restart();
                            }
                            else if (topRightMove.IsPressed())
                            {
                                lastPressed = topRightMove;
                                lastMove = topRightMove.Move;
                                boss.TakeDamage(topRightMove.Damage);

                                topLeftMove.Enabled = false;
                                topRightMove.Enabled = false;
                                bottomLeftMove.Enabled = false;
                                bottomRightMove.Enabled = false;

                                timer.Restart();

                                playerTurn = false;
                                timer.Restart();

                            }
                            else if (bottomLeftMove.IsPressed())
                            {
                                lastPressed = bottomLeftMove;
                                lastMove = bottomLeftMove.Move;
                                boss.TakeDamage(bottomLeftMove.Damage);

                                topLeftMove.Enabled = false;
                                topRightMove.Enabled = false;
                                bottomLeftMove.Enabled = false;
                                bottomRightMove.Enabled = false;

                                timer.Restart();

                                playerTurn = false;
                                timer.Restart();

                            }
                            else if (bottomRightMove.IsPressed())
                            {
                                lastPressed = bottomRightMove;
                                lastMove = bottomRightMove.Move;
                                boss.TakeDamage(bottomRightMove.Damage);

                                topLeftMove.Enabled = false;
                                topRightMove.Enabled = false;
                                bottomLeftMove.Enabled = false;
                                bottomRightMove.Enabled = false;

                                timer.Restart();

                                playerTurn = false;
                                timer.Restart();
                            }

                            if (playerTurn == false)
                            {
                                bossMove = moves[rng.Next(0, moves.Count)];
                            }

                            if (playerTurn ==false && timer.ElapsedMilliseconds>=2000)
                            {
                                bossMove = moves[rng.Next(0, moves.Count)];

                                player.TakeDamage(bossMove.AmountDamage);

                                playerTurn = true;
                            }
                        }

                        // When the player or enemy's health go below or equal to 0 the game is over 
                        if (player.CurrentHealth <= 0)
                        {
                            endResult = Result.Defeat;
                            state = GameState.GameOver;
                        }
                        if (enemies.Count >= 1)
                        {

                            for (int i = 0; i < enemies.Count; i++)
                            {
                                if (enemies[i].CurrentHealth <= 0)
                                {
                                    enemies.RemoveAt(i);
                                }
                            }
                        }
                        // See if there are no enemies left
                        if (enemies.Count <= 0 && boss.CurrentHealth <= 0)
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

            MouseState mState = Mouse.GetState();
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

                        _spriteBatch.Draw(pretzelPaladinBackTextture,
                            new Rectangle(50, 200, 350, 500),
                            Color.White);
                       
                        //_spriteBatch.Draw(sbarroSamuraiTexture,
                        //    new Rectangle(700, -25, 300, 400),
                        //    Color.White);

                        _spriteBatch.Draw(rectangleTexture,
                            rectLocation,
                            Color.White);

                        _spriteBatch.DrawString(regularSizeFont,
                                  $"Player Health: {player.CurrentHealth}/{player.MaxHealth}",
                                  new Vector2(100, 70),
                                  Color.Firebrick);

                        if (attack.Enabled)
                        {
                            attack.Draw(_spriteBatch, Color.White);
                        }
                        

                        // Displays moves after Attack button is pressed
                        if(attack.Enabled==false&&playerTurn)
                        {
                            _spriteBatch.DrawString(subHeaderFont, "Moves:", new Vector2(rectLocation.X+30,rectLocation.Y+37), Color.DarkRed);

                            // Creates 4 Attack buttons

                            if(topLeftMove.Enabled&&topRightMove.Enabled&&bottomRightMove.Enabled&&bottomLeftMove.Enabled)
                            {
                                topLeftMove = new Button(rectLocation.X + 70, rectLocation.Y + yOffset, rectLocation.Width / 3, rectLocation.Height / 6, rectangleTexture, moves[0]);
                                topRightMove = new Button(rectLocation.X + 275, rectLocation.Y + yOffset, rectLocation.Width / 3, rectLocation.Height / 6, rectangleTexture, moves[1]);
                                bottomLeftMove = new Button(rectLocation.X + 70, rectLocation.Y + yOffset + 130, rectLocation.Width / 3, rectLocation.Height / 6, rectangleTexture, moves[2]);
                                bottomRightMove = new Button(rectLocation.X + 275, rectLocation.Y + yOffset + 130, rectLocation.Width / 3, rectLocation.Height / 6, rectangleTexture, moves[3]);

                               // Draws button to screen
                               topLeftMove.Enabled = true;
                               topRightMove.Enabled = true;
                               bottomLeftMove.Enabled = true;
                               bottomRightMove.Enabled = true;

                               topLeftMove.DrawWithText(_spriteBatch, Color.Red, subHeaderFont, rectangleTexture);
                               topRightMove.DrawWithText(_spriteBatch, Color.Red, subHeaderFont, rectangleTexture);
                               bottomLeftMove.DrawWithText(_spriteBatch, Color.Red, subHeaderFont, rectangleTexture);
                               bottomRightMove.DrawWithText(_spriteBatch, Color.Red, subHeaderFont, rectangleTexture);
                                
                            }
                            
  
                            if (topLeftMove.IsPressed()&&topLeftMove.Enabled)
                            {
                                lastPressed = topLeftMove;

                                lastMove = topLeftMove.Move;
                                _spriteBatch.DrawString(
                                       regularSizeFont,
                                       $"{player.Name} dealt {topLeftMove.Move.AmountDamage} to {enemy.Name}",
                                       new Vector2(100, 50),
                                        Color.Firebrick);
                            }
                            else if (topRightMove.IsPressed()&&topRightMove.Enabled)
                            {
                                lastPressed = topRightMove;

                                lastMove = topRightMove.Move;
                                _spriteBatch.DrawString(
                                       regularSizeFont,
                                       $"{player.Name} dealt {topRightMove.Move.AmountDamage} to {enemy.Name}",
                                       new Vector2(100, 50),
                                        Color.Firebrick);
                            }
                            else if (bottomLeftMove.IsPressed() && bottomLeftMove.Enabled)
                            {
                                lastPressed = bottomLeftMove;

                                lastMove = bottomLeftMove.Move;
                                _spriteBatch.DrawString(
                                       regularSizeFont,
                                       $"{player.Name} dealt {bottomLeftMove.Move.AmountDamage} to {enemy.Name}",
                                       new Vector2(100, 50),
                                        Color.Firebrick);
                            }
                            else if (bottomRightMove.IsPressed() && bottomRightMove.Enabled)
                            {
                                lastPressed = bottomRightMove;

                                lastMove = bottomRightMove.Move;
                                _spriteBatch.DrawString(
                                       regularSizeFont,
                                       $"{player.Name} dealt {bottomRightMove.Move.AmountDamage} to {enemy.Name}",
                                       new Vector2(100, 50),
                                        Color.Firebrick);
                            }

                            if (lastPressed != null && lastMove != null && playerTurn==true)
                            {
                                _spriteBatch.DrawString(
                                    regularSizeFont,
                                    $"Paladin used {lastPressed.Text}",
                                    new Vector2(100, 30),
                                    Color.Firebrick);
                                _spriteBatch.DrawString(
                                           regularSizeFont,
                                           $"{player.Name} dealt {lastMove.AmountDamage} damage to {enemy.Name}",
                                           new Vector2(100, 50),
                                           Color.Firebrick);

                                topLeftMove.Enabled = false;
                                topRightMove.Enabled = false;
                                bottomLeftMove.Enabled = false;
                                bottomRightMove.Enabled = false;
                                playerTurn = false;
                            }

                            if (playerTurn == false && enemy.CurrentHealth != 100)
                            {
                                _spriteBatch.DrawString(
                                    regularSizeFont,
                                    $"Enemy used {enemyMove.MoveName}",
                                    new Vector2(850, 30),
                                    Color.DarkRed);
                                _spriteBatch.DrawString(
                                    regularSizeFont,
                                    $"{enemy.Name} dealt {enemyMove.AmountDamage} damage to",
                                    new Vector2(850, 50),
                                    Color.DarkRed);
                                _spriteBatch.DrawString(
                                    regularSizeFont,
                                    $"{player.Name}",
                                    new Vector2(850, 70),
                                    Color.DarkRed);

                                playerTurn = true;
                            }
                        }

                        if (playerTurn == false && boss.CurrentHealth != 300)
                        {
                            _spriteBatch.DrawString(
                                regularSizeFont,
                                $"Enemy used {bossMove.MoveName}",
                                new Vector2(850, 30),
                                Color.DarkRed);
                            _spriteBatch.DrawString(
                                regularSizeFont,
                                $"{enemy.Name} dealt {bossMove.AmountDamage} damage to",
                                new Vector2(850, 50),
                                Color.DarkRed);
                            _spriteBatch.DrawString(
                                regularSizeFont,
                                $"{player.Name}",
                                new Vector2(850, 70),
                                Color.DarkRed);

                            playerTurn = true;
                        }

                        // Draw only one enemy per level
                        if (enemies.Count >= 1)
                        {
                            for (int i = 0; i < 1; i++)
                            {

                                _spriteBatch.Draw(sbarroSamuraiTexture,
                                    new Rectangle(700, -25, 300, 400),
                                    Color.White);
                                _spriteBatch.DrawString(regularSizeFont,
                                 $"Enemy Health: {enemies[i].CurrentHealth}/{enemies[i].MaxHealth}",
                                 new Vector2(100, 90),
                                    Color.Firebrick);
                            }
                        }

                        // When we are in the boss level
                        if (enemies.Count <= 0 && boss.CurrentHealth >= 0)
                        {
                                _spriteBatch.Draw(sbarroSamuraiTexture,
                                    new Rectangle(700, -25, 300, 400),
                                    Color.White);
                                _spriteBatch.DrawString(regularSizeFont,
                                 $"Boss Health: {boss.CurrentHealth}/{boss.MaxHealth}",
                                 new Vector2(100, 90),
                                    Color.Firebrick);
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
                            "YOU SUCK CHUMP.",
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

            Color pretzelCursorColor = Color.White;

            if (mState.LeftButton == ButtonState.Pressed)
            {
                pretzelCursorColor = Color.Red;
            }

            _spriteBatch.Draw(
                pretzelCursor,
                new Rectangle(mState.X, mState.Y, 75, 75),
                pretzelCursorColor);

            _spriteBatch.End();

            base.Draw(gameTime);
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

                moves.Add(new Move((components[0]), int.Parse(components[1]), int.Parse(components[2])));
            }

            file.Close();
        }
    }
}