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
    /// <summary>
    /// The current state of the game
    /// </summary>
    public enum GameState
    {
        MainMenu,
        Game,
        Pause,
        GameOver,
        Transition
    }
    /// <summary>
    /// End result of the game--Win or Lose
    /// </summary>
    public enum Result
    {
        Victory,
        Defeat
    }
    public class Game1 : Game
    {
        // Fields --
        // - MonoGame fields
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        // - Enums/Fonts & Text
        private SpriteFont menuFont;
        private SpriteFont regularSizeFont;
        private SpriteFont subHeaderFont;
        private GameState state;
        private Result endResult;
        // - Textures
        private Texture2D foodCourt;
        private Texture2D rectangleTexture;
        private Texture2D startImg;
        private Texture2D pretzelPaladinConceptImg;
        private Texture2D sbarroSamuraiTexture;
        private Texture2D pretzelPaladinBackTextture;
        private Texture2D pretzelCursor;
        private Texture2D bodok;
        private Texture2D textBox;
        private Texture2D healthBar;
        private Texture2D healthBox;
        private Texture2D bap;
        private Texture2D walk;
        private Texture2D transition;
        private float playerHealthPercent;
        private float enemyHealthPercent;


        // - Buttons
        private Button startbutton;
        private Button lastPressed;
        // - Moves
        private Move lastMove;
        private Move enemyMove;
        private Move bossMove;
        private bool playerTurn;
        private Random rng;
        private Stopwatch actualTimer;
        // - Mouse State Tracking
        private MouseState msState;
        private MouseState prevMouseState;
        // - Move List/Enemies
        private List<Move> moves;
        private List<Enemy> enemies;
        // - Screen Dimensions
        private int screenWidth;
        private int screenHeight;
        private Rectangle rectLocation;
        // - Animation 
        private int ppX;
        private int ppY;
        private int ssX;
        private int ssY;
        private int enemyWait;
        private bool animating;
        private bool movingForwards;
        private bool enemyAnimating;
        private bool enemyMovingForwards;
        private int constantX;
        private int psuedoTimer;
        private int enemyPsuedoTimer;
        private int enemyConstantX;

        private bool walkDone;
        private int walkX;

        private Color ppC;
        private Color ssC;

        // - Move Related Buttons
        private Button topLeftMove;
        private Button topRightMove;
        private Button bottomLeftMove;
        private Button bottomRightMove;
        private Button backButton;
        private Button exitGame;
        List<Button> buttonList = new List<Button>();

        private int moveCooldown;

        // - Characters
        private Enemy enemy;
        private Enemy enemy2;
        private Enemy enemy3;
        private Player player;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            // Starting State
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
            // - Animation Initializations
            movingForwards = true;
            enemyMovingForwards = true;
            animating = false;
            enemyAnimating = false;
            constantX = 100;
            psuedoTimer = 0;
            walkDone = false;
            walkX = 10;
            enemyConstantX = 100;
            enemyPsuedoTimer = 0;
            enemyWait = 0;
            
            // Moves/Buttons Initializations
            lastMove = new Move(" ", 0, " ", 0);
            enemyMove = new Move(" ", 0, " ", 0);
            topLeftMove = new Button();
            topRightMove = new Button();
            bottomLeftMove = new Button();
            bottomRightMove = new Button();
            backButton = new Button((screenWidth / 3), (screenHeight / 2) - 50, 350, 100, rectangleTexture, new Move(" ", 0, " ", 0));
            exitGame = new Button((screenWidth / 3) + 75, (screenHeight / 2) + 100, 200, 100, rectangleTexture, new Move(" ", 0, " ", 0));
            backButton.Text = "RETURN TO BATTLE";
            exitGame.Text = "EXIT GAME";
            exitGame.Enabled = true;
            backButton.Enabled = true;
            playerHealthPercent = 1f;
            enemyHealthPercent = 1f;
            playerTurn = true;
            rng = new Random();
            actualTimer = new Stopwatch();
            buttonList.Add(topLeftMove);
            buttonList.Add(topRightMove);
            buttonList.Add(bottomLeftMove);
            buttonList.Add(bottomRightMove);


            // Animation Initializations 
            ppX = 50;
            ppY = 200;
            ssX = 700;
            ssY = -25;
            ppC = Color.White;
            ssC = Color.White;
            _graphics.ApplyChanges();
            base.Initialize();
        }
        protected override void LoadContent()
        {
            // - Fonts
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            menuFont = this.Content.Load<SpriteFont>("MenuFont");
            regularSizeFont = this.Content.Load<SpriteFont>("NormalFontSize");
            subHeaderFont = this.Content.Load<SpriteFont>("Subheader");
            // Textures
            bodok = this.Content.Load<Texture2D>("bodok transparent");
            foodCourt = this.Content.Load<Texture2D>("food court background");
            startImg = this.Content.Load<Texture2D>("startButton");
            rectangleTexture = this.Content.Load<Texture2D>("rectangle image");
            pretzelCursor = this.Content.Load<Texture2D>("pretzel");
            textBox = this.Content.Load<Texture2D>("text rect");
            healthBar = this.Content.Load<Texture2D>("health bar");
            healthBox = this.Content.Load<Texture2D>("health box");
            pretzelPaladinConceptImg = this.Content.Load<Texture2D>("PretzelPaladin");
            sbarroSamuraiTexture = this.Content.Load<Texture2D>("Sbarro Samurai");
            pretzelPaladinBackTextture = this.Content.Load<Texture2D>("PretzelPaladin Back Image");
            bap = this.Content.Load<Texture2D>("bap");
            walk = this.Content.Load<Texture2D>("walk");
            transition = this.Content.Load<Texture2D>("transition background");

            // Enemy with Textures
            enemy = new Enemy(sbarroSamuraiTexture, "Sbarro Samurai", 100, 1, 1);
            enemy3 = new Enemy(bap, "B.A.P", 100, 1, 1);
            enemy2 = new Enemy(bodok, "B.O.D.O.K", 110, 1, 1);
            player = new Player(rectangleTexture, "Pretzel Paladin", 500, 1, 1);

            enemies.Add(enemy);
            enemies.Add(enemy2);
            enemies.Add(enemy3);
            // Buttons with Textures
            startbutton = new Button((_graphics.PreferredBackBufferWidth / 2) - 100, (_graphics.PreferredBackBufferHeight / 3) + 170, 200, 100, startImg);
            rectLocation = new Rectangle((screenWidth / 2 + 30), screenHeight / 2 + 30, screenWidth / 2, screenHeight);
        }
        protected override void Update(GameTime gameTime)
        {
            KeyboardState kbState = Keyboard.GetState();
            msState = Mouse.GetState();
            screenHeight = _graphics.PreferredBackBufferHeight;
            screenWidth = _graphics.PreferredBackBufferWidth;

            switch (state)
            {
                case GameState.MainMenu:
                    {
                        startbutton.Enabled = true;
                        if (startbutton.IsPressed())
                        {
                            state = GameState.Game;
                        }

                        if (kbState.IsKeyDown(Keys.Escape))
                        {
                            state = GameState.Pause;
                        }
                        break;
                    }
                case GameState.Game:
                    {
                        // Pauses the Game if the user presses the ESCAPE key
                        if (kbState.IsKeyDown(Keys.Escape))
                        {
                            state = GameState.Pause;
                        }
                        

                        // Inflicts Damage to enemy based on move chosen
                        if (playerTurn == true)
                        {
                            EnableButtons();
                            PlayerTurnPress();
                        }

                        else if (playerTurn == false && animating == false)
                        {

                            enemyWait++;
                            if (enemyWait > 10)
                            {
                                enemyAnimating = true;
                                enemyMove = moves[rng.Next(0, moves.Count)];
                                player.TakeDamage(enemyMove.AmountDamage);
                                playerHealthPercent = (float)(player.CurrentHealth) / player.MaxHealth;
                                enemyWait = 0;
                                playerTurn = true;
                                CheckCooldown();
                            }
                        }

                        // When the player or enemy's health go below or equal to 0 the game is over 
                        if (player.CurrentHealth <= 0)
                        {
                            endResult = Result.Defeat;
                            state = GameState.GameOver;
                        }
                        if (enemies[0].CurrentHealth <= 0)
                        {
                            enemies.RemoveAt(0);
                            enemyHealthPercent = 1;
                            walkDone = false;
                            state = GameState.Transition;
                        }
                        // See if there are no enemies left
                        if (enemies.Count <= 0)
                        {
                            enemies.Add(enemy);
                            enemies.Add(enemy2);
                            enemies.Add(enemy3);
                            enemies[0].MaxHealth = enemy.MaxHealth + 100;
                            enemies[0].CurrentHealth = enemies[0].MaxHealth;
                            enemies[1].MaxHealth = enemy2.MaxHealth + 100;
                            enemies[1].CurrentHealth = enemies[1].MaxHealth;
                            enemies[2].MaxHealth = enemy3.MaxHealth + 100;
                            enemies[2].CurrentHealth = enemies[2].MaxHealth;
                            player.CurrentHealth += player.MaxHealth;
                            /*walkDone = false;
                            state = GameState.Transition;*/
                        }

                        break;

                    }
                case GameState.Pause:
                    {
                        if (kbState.IsKeyDown(Keys.Enter) || backButton.IsPressed())
                        {
                            state = GameState.Game;
                        }
                        if (exitGame.IsPressed())
                        {
                            Exit();
                        }

                        break;
                    }
                case GameState.GameOver:
                    {
                        if (player.CurrentHealth <= 0)
                        {
                            endResult = Result.Defeat;
                        }
                        else
                        {
                            endResult = Result.Victory;
                        }
                        if (kbState.IsKeyDown(Keys.Escape))
                        {
                            Exit();
                        }

                        break;
                    }

                case GameState.Transition:
                    {
                        if (walkDone == true)
                        {
                            playerTurn = true;
                            state = GameState.Game;
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

            //animation for the battle state
            BattleWiggle();

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
                        //draws characters and health and junk
                        DrawCharacters();
                        CheckCooldown();

                        if (playerTurn == true)
                        {
                            _spriteBatch.DrawString(subHeaderFont, "Moves:", new Vector2(rectLocation.X + 30, rectLocation.Y + 20), Color.DarkRed);
                            // Creates 4 Attack buttons
                            CreateButtons();

                            // Draws button to screen    
                            DrawButtonsWithText();
                            
                            //deals with the aftermath of
                            //clicking a button
                            ButtonsClick();
                            
                            if (lastPressed != null && lastMove != null && playerTurn == true)
                            {
                                _spriteBatch.DrawString(
                                    subHeaderFont,
                                    $"{player.Name} used {lastPressed.Text}",
                                    new Vector2(100, 30),
                                    Color.Firebrick);
                                _spriteBatch.DrawString(
                                           subHeaderFont,
                                           $"{player.Name} dealt {enemy.DamageDealt} damage to",
                                           new Vector2(100, 60),
                                           Color.Firebrick);
                                _spriteBatch.DrawString(subHeaderFont, $"{enemies[0].Name}", new Vector2(100, 90), Color.Firebrick);
                                //topLeftMove.Enabled = false;
                                //topRightMove.Enabled = false;
                                //bottomLeftMove.Enabled = false;
                                //bottomRightMove.Enabled = false;
                                playerTurn = false;
                            }
                            if (playerTurn == false)
                            {
                                _spriteBatch.DrawString(
                                regularSizeFont,
                                $"Enemy used {enemyMove.MoveName}",
                                new Vector2(850, 30),
                                Color.Black);

                                _spriteBatch.Draw(textBox, new Rectangle(840, 25, 260, 100), Color.White);

                                _spriteBatch.DrawString(
                                        subHeaderFont,
                                        $"{enemies[0].Name} dealt {player.DamageDealt}",
                                        new Vector2(850, 30),
                                        Color.Black);

                                _spriteBatch.DrawString(
                                    subHeaderFont,
                                    $"damage to Pretzel",
                                    new Vector2(850, 57),
                                    Color.Black);

                                _spriteBatch.DrawString(
                                        subHeaderFont,
                                        $"Paladin",
                                        new Vector2(850, 85),
                                        Color.Black);

                                playerTurn = true;
                            }
                        }

                        ButtonHover();

                        break;
                    }

                case GameState.Pause:
                    {
                        _spriteBatch.DrawString(
                            menuFont,
                            "PAUSE MENU",
                            new Vector2((screenWidth / 3) - 25, screenHeight / 6),
                            Color.SaddleBrown);

                        backButton.DrawWithText(_spriteBatch, Color.RosyBrown, subHeaderFont, rectangleTexture, false);
                        exitGame.DrawWithText(_spriteBatch, Color.RosyBrown, subHeaderFont, rectangleTexture, false);

                        break;
                    }
                case GameState.GameOver:
                    {
                        if (endResult == Result.Defeat)
                        {
                            _spriteBatch.DrawString(
                                menuFont,
                                "GET TOSSED",
                                new Vector2((screenWidth / 4), 200),
                                Color.SaddleBrown);

                            _spriteBatch.DrawString(
                                regularSizeFont,
                                "The world is doomed to be fed absolutely divine cinnamon pretzel nuggets...",
                                new Vector2((screenWidth / 200), screenHeight - 200),
                                Color.SaddleBrown);
                        }

                        if (endResult == Result.Victory)
                        {

                            _spriteBatch.DrawString(
                                menuFont,
                                "Lookin' Cool, Paladin!",
                                new Vector2((screenWidth / 4), 200),
                                Color.SaddleBrown);

                            _spriteBatch.DrawString(
                                regularSizeFont,
                                "The world is safe from Auntie Anne's once again...",
                                new Vector2((screenWidth / 200), screenHeight - 200),
                                Color.SaddleBrown);
                        }
                        break;
                    }
                case GameState.Transition:
                    {
                        _spriteBatch.Draw(transition, new Rectangle(0, 0, 1100, 700), Color.White);
                        _spriteBatch.DrawString(menuFont, "You won!", new Vector2(75, 75), Color.Firebrick);
                        _spriteBatch.DrawString(subHeaderFont, "Now approaching...\n" + enemies[0].Name, new Vector2(850, 50), Color.Firebrick);
                        if (walkX < 1100)
                        {
                            _spriteBatch.Draw(walk, new Rectangle(walkX, 200, 400, 400), Color.White);
                            walkX += 5;
                        }
                        else
                        {
                            walkDone = true;
                            ppX = 50;
                            walkX = 10;
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
                new Rectangle(mState.X - (75/2), mState.Y - (75/2), 75, 75),
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
            StreamReader file = new StreamReader("../../../Content/" + fileName);
            string currentLine;
            while ((currentLine = file.ReadLine()) != null)
            {
                string[] components = currentLine.Split(",");
                moves.Add(new Move(components[0], int.Parse(components[1]), components[2], int.Parse(components[3])));
            }
            file.Close();
        }

        public void EnableButtons()
        {
            foreach(Button button in buttonList)
            {
                if (button.Move != null && button.Move.OnCooldown == false)
                {
                    button.Enabled = true;
                }
                else
                {
                    button.Enabled = false;
                }
            }
            
        }

        public void CheckCooldown()
        {
            foreach (Button button in buttonList)
            {
                if (button.Move != null && button.Move.CoolDownTime >= button.Move.Cooldown)
                {
                    button.Move.OnCooldown = false;
                }
                else
                {
                    button.Move.CoolDownTime++;
                }

            }
        }

        public void DrawButtonsWithText()
        {
            foreach(Button button in buttonList)
            {
                if(button.Enabled == true)
                {
                    button.DrawWithText(_spriteBatch, Color.Firebrick, subHeaderFont, rectangleTexture, true);
                }
                else
                {
                    button.DrawWithText(_spriteBatch, Color.Gray, subHeaderFont, rectangleTexture, true);
                }
            }
        }

        public void PlayerTurnPress()
        {
            foreach(Button button in buttonList)
            {
                if (button.IsPressed())
                {
                    lastPressed = button;
                    lastMove = button.Move;
                    enemies[0].TakeDamage(button.Damage);
                    enemyHealthPercent = (float)enemies[0].CurrentHealth / enemies[0].MaxHealth;
                    playerTurn = false;
                    animating = true;
                    button.Move.OnCooldown = true;
                    button.Move.CoolDownTime = 0;
                }
            }
            
        }

        

        public void ButtonsClick()
        {
            foreach(Button button in buttonList)
            {
                if (button.IsPressed())
                {
                    lastPressed = button;
                    lastMove = button.Move;
                    _spriteBatch.DrawString(
                           subHeaderFont,
                           $"{player.Name} dealt {enemy.DamageDealt} to {enemies[0].Name}",
                           new Vector2(100, 50),
                            Color.Firebrick);
                }
            }
        }

        public void CreateButtons()
        {
            int xPos = 0;
            int yPos = 0;
            int width = rectLocation.Width / 3;
            int height = rectLocation.Height / 6;

            for(int i = 0; i < buttonList.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        xPos = rectLocation.X + 60;
                        yPos = rectLocation.Y + 60;
                        break;

                    case 1:
                        xPos = rectLocation.X + 295;
                        yPos = rectLocation.Y + 60;
                        break;

                    case 2:

                        xPos = rectLocation.X + 60;
                        yPos = rectLocation.Y + 200;
                        break;

                    case 3:
                        xPos = rectLocation.X + 295;
                        yPos = rectLocation.Y + 200;
                        break;
                }

                buttonList[i] = new Button(xPos, yPos, width, height, rectangleTexture, moves[i]);
            }
        }

        public void ButtonHover()
        {
            foreach(Button button in buttonList)
            {
                if (button.IsHover())
                {
                    _spriteBatch.DrawString(
                        regularSizeFont, 
                        button.Move.Description, 
                        new Vector2(rectLocation.X + 10, rectLocation.Y), 
                        Color.Black);
                }
            }
           
        }

        public void BattleWiggle()
        {
            if (animating && playerTurn == false)
            {

                if (psuedoTimer < constantX && movingForwards)
                {
                    psuedoTimer += 10;
                    ppX += 10;
                }
                if (psuedoTimer >= constantX && movingForwards)
                {
                    ssC = Color.Red;
                    movingForwards = false;
                }
                if (!movingForwards)
                {
                    psuedoTimer -= 10;
                    ppX -= 10;
                }
                if (psuedoTimer <= 0)
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
                    ppC = Color.Red;
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
        }

        public void DrawCharacters()
        {
            _spriteBatch.Draw(
                            foodCourt,
                            new Rectangle(0, 0, screenWidth, screenHeight),
                            Color.White);

            _spriteBatch.Draw(
                pretzelPaladinBackTextture,
                new Rectangle(ppX, ppY, 350, 500),
                ppC);

            _spriteBatch.Draw(
                healthBox,
                new Rectangle(50 - 10, 200 - 10, 350, 75),
                Color.White);

            int healthWidth = (int)(340 * playerHealthPercent);

            _spriteBatch.Draw(
                healthBar,
                new Rectangle(50 - 5, 200 - 7, healthWidth, 70),
                Color.White);

            _spriteBatch.DrawString(
                subHeaderFont,
                $"{player.CurrentHealth}/{player.MaxHealth}",
                new Vector2(50 + 120, 200 + 10),
                Color.Black);

            _spriteBatch.Draw(
                textBox,
                new Rectangle(90, 25, 400, 100),
                Color.White);

            _spriteBatch.Draw(
                enemies[0].CharacterImage,
                new Rectangle(ssX, ssY, 300, 450),
                ssC);

            int healthWidthE = (int)(243 * enemyHealthPercent);

            _spriteBatch.Draw(
                healthBox,
                new Rectangle(700, 320, 250, 55),
                Color.White);

            _spriteBatch.Draw(
                healthBar,
                new Rectangle(703, 322, healthWidthE, 51),
                Color.White);

            _spriteBatch.DrawString(
                subHeaderFont,
                $"{enemies[0].CurrentHealth}/{enemies[0].MaxHealth}",
                 new Vector2(785, 330),
                Color.Black);

            _spriteBatch.Draw(
                rectangleTexture,
                    rectLocation,
                    Color.White);
        }
    }
}