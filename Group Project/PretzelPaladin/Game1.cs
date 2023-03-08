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

        private Texture2D pretzelButton;
        private Texture2D foodCourt;
        private Texture2D rectangleTexture;
        private Button startbutton;
        private GameState state;

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

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            menuFont = this.Content.Load<SpriteFont>("MenuFont");
            regularSizeFont = this.Content.Load<SpriteFont>("NormalFontSize");

            pretzelButton = this.Content.Load<Texture2D>("prezel");
            foodCourt = this.Content.Load<Texture2D>("foodCourt");
            rectangleTexture = this.Content.Load<Texture2D>("Rectangle");

            startbutton = new Button((_graphics.PreferredBackBufferWidth / 3), _graphics.PreferredBackBufferHeight / 2, 200, 100, pretzelButton);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.M))
                Exit();

            KeyboardState kbState = Keyboard.GetState();

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