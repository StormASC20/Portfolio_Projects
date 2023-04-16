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
        private float timer;
        private bool playerTurn;
        private Random rng;
        private Stopwatch actualTimer;


        private MouseState msState;
        private MouseState prevMouseState;

        private List<Move> moves;
        private List<Enemy> enemies;

        private int screenWidth;
        private int screenHeight;
        private Rectangle rectLocation;
        private int yOffset;

        private int ppX;
        private int ppY;
        private int ssX;
        private int ssY;
        private int enemyWait;
        private int constantX;
        private int psuedoTimer;

        private int enemyPsuedoTimer;
        private int enemyConstantX;

        private Color ppC;
        private Color ssC;
        private bool animating;
        private bool movingForwards;
        private bool enemyAnimating;
        private bool enemyMovingForwards;
        private bool right;
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

            movingForwards = true;
            enemyMovingForwards = true;
            animating = false;
            enemyAnimating = false;
            constantX = 100;
            psuedoTimer = 0;

            enemyConstantX = 100;
            enemyPsuedoTimer = 0;
            enemyWait = 0;

            boss = new Enemy(rectangleTexture, "Sbarro Samurai", 300, 300, 1, 2);

            lastMove = new Move(" ", 0,0);
            enemyMove = new Move(" ", 0,0);
            bossMove = new Move(" ", 0, 0);
            topLeftMove = new Button();
            topRightMove = new Button();
            bottomLeftMove = new Button();
            bottomRightMove=new Button();

            attackPressed = false;
            timer = 0f;
            playerTurn = true;
            rng = new Random();
            actualTimer = new Stopwatch();

            

            ppX = 50;
            ppY = 200;
            ssX = 700;
            ssY = -25;
            ppC = Color.White;
            ssC = Color.White;
            right = true;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch     = new SpriteBatch(GraphicsDevice);
            menuFont         = this.Content.Load<SpriteFont>("MenuFont");
            regularSizeFont  = this.Content.Load<SpriteFont>("NormalFontSize");
            subHeaderFont    = this.Content.Load<SpriteFont>("Subheader");

            foodCourt        = this.Content.Load<Texture2D>("food court background");
            startImg         = this.Content.Load<Texture2D>("startButton");
            attackImg        = this.Content.Load<Texture2D>("attackButton");
            defendImg        = this.Content.Load<Texture2D>("defendButton");
            rectangleTexture = this.Content.Load<Texture2D>("rectangle image");
            pretzelCursor = this.Content.Load<Texture2D>("pretzel");

            pretzelPaladinConceptImg   = this.Content.Load<Texture2D>("PretzelPaladin");
            sbarroSamuraiTexture       = this.Content.Load<Texture2D>("Sbarro Samurai");
            pretzelPaladinBackTextture = this.Content.Load<Texture2D>("PretzelPaladin Back Image");

            enemy = new Enemy(sbarroSamuraiTexture, "Sbarro Samurai", 100, 100, 1, 1);
            enemy2 = new Enemy(pretzelCursor, "Pretzel", 100, 100, 1, 1);
            enemy3 = new Enemy(pretzelPaladinConceptImg, "Parallel Paladin", 110, 110, 1, 1);
            player = new Player(rectangleTexture, "Pretzel Paladin", 500, 500, 1, 1);

            enemies.Add(enemy);
            enemies.Add(enemy2);
            enemies.Add(enemy3);

            startbutton      = new Button((_graphics.PreferredBackBufferWidth / 2)-100, (_graphics.PreferredBackBufferHeight / 3)+170, 200, 100, startImg);
            attack           = new Button((screenWidth) - 325, screenHeight-200, 200, 100, attackImg);
            
            //defend = new Button((screenWidth / 2) + 75, (screenHeight / 2) + 125, 200, 100, defendImg);

            rectLocation     = new Rectangle((screenWidth / 2 + 30), screenHeight / 2 + 30, screenWidth / 2, screenHeight);


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
                        if (attackPressed == true)
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

                        if (attack.IsPressed())
                        {
                            attack.Enabled = false;
                        }
                        // Inflicts Damage to enemy based on move chosen
                        if (playerTurn == true)
                        {

                            if (topLeftMove.IsPressed())
                            {

                                lastPressed = topLeftMove;
                                lastMove = topLeftMove.Move;
                                enemies[0].TakeDamage(topLeftMove.Damage);
                                playerTurn = false;

                                animating = true;
                            }

                            else if (topRightMove.IsPressed())
                            {
                                lastPressed = topRightMove;
                                lastMove = topRightMove.Move;
                                enemies[0].TakeDamage(topRightMove.Damage);
                                playerTurn = false;
                                animating = true;
                            }

                            else if (bottomLeftMove.IsPressed())
                            {
                                lastPressed = bottomLeftMove;
                                lastMove = bottomLeftMove.Move;
                                enemies[0].TakeDamage(bottomLeftMove.Damage);
                                playerTurn = false;
                                animating = true;
                            }

                            else if (bottomRightMove.IsPressed())
                            {
                                lastPressed = bottomRightMove;
                                lastMove = bottomRightMove.Move;
                                enemies[0].TakeDamage(bottomRightMove.Damage);
                                playerTurn = false;
                                animating = true;
                            }
                        }

                        else if (playerTurn == false && animating == false)
                        {
                            enemyWait++;

                            if(enemyWait > 10)
                            {
                                enemyAnimating = true;
                                enemyMove = moves[rng.Next(0, moves.Count)];
                                player.TakeDamage(enemyMove.AmountDamage);
                                enemyWait = 0;
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
                        if (enemies.Count <= 0)
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
                        else
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

            if (animating && playerTurn == false)
            {
                
                if (psuedoTimer < constantX && movingForwards)
                {
                    psuedoTimer += 10;
                    ppX += 10;
                }

                if(psuedoTimer >= constantX && movingForwards)
                {
                    ssC = Color.Red;
                    movingForwards = false;
                }

                if (!movingForwards)
                {
                    psuedoTimer -= 10;
                    ppX -= 10;
                }

                if(psuedoTimer <= 0)
                {
                    animating = false;
                    movingForwards = true;
                    ssC = Color.White;
                }

            }

            else if (enemyAnimating)
            {
                if (enemyPsuedoTimer < enemyConstantX && enemyMovingForwards)
                {
                    enemyPsuedoTimer += 10;
                    ssX -= 10;
                }

                if (enemyPsuedoTimer >= enemyConstantX && enemyMovingForwards)
                {
                    ppC= Color.Red;
                    enemyMovingForwards = false;
                }

                if (!enemyMovingForwards)
                {
                    enemyPsuedoTimer -= 10;
                    ssX += 10;
                }

                if (enemyPsuedoTimer <= 0)
                {
                    enemyAnimating = false;
                    enemyMovingForwards = true;
                    ppC = Color.White;
                }

            }

            switch (state)
                {
                    case GameState.MainMenu:
                        {
                            _spriteBatch.Draw(
                                pretzelPaladinConceptImg,
                                new Rectangle(610, 100, screenWidth / 2, screenHeight),
                                Color.White);

                            _spriteBatch.Draw(
                                pretzelPaladinConceptImg,
                                new Rectangle(-50, 100, screenWidth / 2, screenHeight),
                                Color.White);

                            _spriteBatch.DrawString(
                                menuFont,
                                "PRETZEL PALADIN",
                                new Vector2(270, 100),
                                Color.SaddleBrown);

                            startbutton.Draw(_spriteBatch, Color.White);
                            break;
                        }
                    case GameState.Game:
                        {

                            yOffset = 95;
                        _spriteBatch.Draw(foodCourt, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                            _spriteBatch.Draw(pretzelPaladinBackTextture,
                                new Rectangle(ppX, ppY, 350, 500),
                                ppC);

                            _spriteBatch.Draw(enemies[0].CharacterImage,
                                new Rectangle(ssX, ssY, 300, 450),
                                ssC);

                            _spriteBatch.Draw(rectangleTexture,
                                rectLocation,
                                Color.White);

                            _spriteBatch.DrawString(regularSizeFont,
                                      $"Player Health: {player.CurrentHealth}/{player.MaxHealth}",
                                      new Vector2(100, 70),
                                      Color.Indigo);


                            if (attack.Enabled)
                            {
                                attack.Draw(_spriteBatch, Color.White);
                            }


                            // Displays moves after Attack button is pressed
                            if (attack.Enabled == false && playerTurn)
                            {
                                _spriteBatch.DrawString(subHeaderFont, "Moves:", new Vector2(rectLocation.X + 30, rectLocation.Y + 20), Color.DarkRed);

                                // Creates 4 Attack buttons

                                if (topLeftMove.Enabled && topRightMove.Enabled && bottomRightMove.Enabled && bottomLeftMove.Enabled)
                                {
                                    topLeftMove = new Button(rectLocation.X + 60, rectLocation.Y + 60, rectLocation.Width / 3, rectLocation.Height / 6, rectangleTexture, moves[0]);
                                    topRightMove = new Button(rectLocation.X + 295, rectLocation.Y + 60, rectLocation.Width / 3, rectLocation.Height / 6, rectangleTexture, moves[1]);
                                    bottomLeftMove = new Button(rectLocation.X + 60, rectLocation.Y + 200, rectLocation.Width / 3, rectLocation.Height / 6, rectangleTexture, moves[2]);
                                    bottomRightMove = new Button(rectLocation.X + 295, rectLocation.Y + 200, rectLocation.Width / 3, rectLocation.Height / 6, rectangleTexture, moves[3]);

                                    // Draws button to screen
                                    topLeftMove.Enabled = true;
                                    topRightMove.Enabled = true;
                                    bottomLeftMove.Enabled = true;
                                    bottomRightMove.Enabled = true;

                                    topLeftMove.DrawWithText(_spriteBatch, Color.Firebrick, subHeaderFont, rectangleTexture);
                                    topRightMove.DrawWithText(_spriteBatch, Color.Firebrick, subHeaderFont, rectangleTexture);
                                    bottomLeftMove.DrawWithText(_spriteBatch, Color.Firebrick, subHeaderFont, rectangleTexture);
                                    bottomRightMove.DrawWithText(_spriteBatch, Color.Firebrick, subHeaderFont, rectangleTexture);

                                }


                                if (topLeftMove.IsPressed() && topLeftMove.Enabled)
                                {
                                    lastPressed = topLeftMove;

                                    lastMove = topLeftMove.Move;
                                    _spriteBatch.DrawString(
                                           regularSizeFont,
                                           $"{player.Name} dealt {topLeftMove.Move.AmountDamage} to {enemies[0].Name}",
                                           new Vector2(100, 50),
                                            Color.Firebrick);

                                }
                                else if (topRightMove.IsPressed() && topRightMove.Enabled)
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
                                           $"{player.Name} dealt {bottomLeftMove.Move.AmountDamage} to {enemies[0].Name}",
                                           new Vector2(100, 50),
                                            Color.Firebrick);
                                }
                                else if (bottomRightMove.IsPressed() && bottomRightMove.Enabled)
                                {
                                    lastPressed = bottomRightMove;

                                    lastMove = bottomRightMove.Move;
                                    _spriteBatch.DrawString(
                                           regularSizeFont,
                                           $"{player.Name} dealt {bottomRightMove.Move.AmountDamage} to {enemies[0].Name}",
                                           new Vector2(100, 50),
                                            Color.Firebrick);
                                }

                                if (lastPressed != null && lastMove != null && playerTurn == true)
                                {
                                    _spriteBatch.DrawString(
                                        regularSizeFont,
                                        $"Paladin used {lastPressed.Text}",
                                        new Vector2(100, 30),
                                        Color.Indigo);
                                    _spriteBatch.DrawString(
                                               regularSizeFont,
                                               $"{player.Name} dealt {lastMove.AmountDamage} damage to {enemies[0].Name}",
                                               new Vector2(100, 50),
                                               Color.Indigo);

                                    topLeftMove.Enabled = false;
                                    topRightMove.Enabled = false;
                                    bottomLeftMove.Enabled = false;
                                    bottomRightMove.Enabled = false;
                                    playerTurn = false;
                                }

                                if (playerTurn == false)
                                {
                                    _spriteBatch.DrawString(
                                    regularSizeFont,
                                    $"Enemy used {enemyMove.MoveName}",
                                    new Vector2(850, 30),
                                    Color.Black);

                                    _spriteBatch.DrawString(
                                        regularSizeFont,
                                        $"{enemies[0].Name} dealt {enemyMove.AmountDamage} damage to",
                                        new Vector2(850, 50),
                                        Color.Black);

                                    _spriteBatch.DrawString(
                                        regularSizeFont,
                                        $"{player.Name}",
                                        new Vector2(850, 70),
                                        Color.Black);

                                    playerTurn = true;
                                    actualTimer.Stop();
                                }
                            }

                            // Draw only one enemy per level
                            if (enemies.Count >= 1)
                            {
                               _spriteBatch.DrawString(regularSizeFont,
                                $"Enemy Health: {enemies[0].CurrentHealth}/{enemies[0].MaxHealth}",
                                new Vector2(100, 90),
                                   Color.Black);

                            }

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
                            if (endResult == Result.Victory)
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

        public void AttackAnimation()
        {
            int constantX = 100;
            int psuedoTimer = 0;

            while (psuedoTimer <= constantX)
            {
                psuedoTimer += 10;
                ppX += 10;
                ssC = Color.Red;
            }
            psuedoTimer = 0;
        }

        public void AttackReset()
        {
            int constantX = 100;
            int psuedoTimer = 0;

            while (psuedoTimer <= constantX)
            {
                psuedoTimer += 10;
                ppX -= 10;
                ssC = Color.White;
            }

            psuedoTimer = 0;
        }

        public void DamageAnimation()
        {
            ssX -= 100;
            ssY += 100;
            ppC = Color.Red;
        }

        public void DamageReset()
        {
            ssX += 100;
            ssY -= 100;
            ppC = Color.White;
        }

        public void Wiggle(int x)
        {
            int constantX = x;
            if (x >= constantX)
            {
                while (x < constantX + 10)
                {
                    x += 5;
                }
            }
            else
            {
                while (x > constantX - 10)
                {
                    x -= 5;
                }
            }
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