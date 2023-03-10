using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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

        
        private KeyboardState kbState;
        private KeyboardState prevKbState;

        private int screenWidth;
        private int screenHeight;

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
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            menuFont = this.Content.Load<SpriteFont>("MenuFont");
            regularSizeFont = this.Content.Load<SpriteFont>("NormalFontSize");
            //pretzelButton = this.Content.Load<Texture2D>("prezel");
            foodCourt = this.Content.Load<Texture2D>("foodCourt");
            startImg = this.Content.Load<Texture2D>("startButton");
            attackImg = this.Content.Load<Texture2D>("attackButton");
            defendImg = this.Content.Load<Texture2D>("defendButton");
            rectangleTexture = this.Content.Load<Texture2D>("Rectangle");
            //minimize = new Button((_graphics.PreferredBackBufferWidth /2) - 100, _graphics.PreferredBackBufferHeight / 2, 200, 100, minimizeImg);
            startbutton = new Button((_graphics.PreferredBackBufferWidth / 3), _graphics.PreferredBackBufferHeight / 2, 200, 100, startImg);
            attack = new Button((screenWidth/2) + 75, screenHeight/2, 200, 100, attackImg);
            defend = new Button((screenWidth / 2) + 75, (screenHeight / 2) + 125, 200, 100, defendImg);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.M))
                Exit();

            kbState = Keyboard.GetState();

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
                        if (kbState.IsKeyDown(Keys.G))
                        {
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

            prevKbState = Keyboard.GetState();

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
                        startbutton.Draw(_spriteBatch);
                        break;
                    }
                case GameState.Game:
                    {
                        _spriteBatch.Draw(rectangleTexture, 
                            new Rectangle((screenWidth/2+50),screenHeight/2,screenWidth/2,screenHeight),
                            Color.White);
                        attack.Draw(_spriteBatch);
                        defend.Draw(_spriteBatch);  
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
    }
}