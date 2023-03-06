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
        private Texture2D pretzelButton;
        private Texture2D foodCourt;
        private Button button;
        private GameState state;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            state = GameState.MainMenu;
            base.Initialize();
            button = new Button(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2 + 100, 200, 100, pretzelButton);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            menuFont = this.Content.Load<SpriteFont>("MenuFont");
            pretzelButton = this.Content.Load<Texture2D>("prezel");
            foodCourt = this.Content.Load<Texture2D>("foodCourt");
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            switch(state)
            {
                case GameState.MainMenu:
                    {
                        if (button.IsPressed())
                        {
                            state = GameState.Game;
                        }
                        break;
                    }
                case GameState.Game:
                    {
                        break;
                    }
                case GameState.Pause:
                    {
                        break;
                    }
                case GameState.GameOver:
                    {
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
                        button.Draw(_spriteBatch);
                        break;
                    }
                case GameState.Game:
                    {
                        _spriteBatch.Draw(
                            foodCourt, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight),
                            Color.White);
                        break;
                    }
                case GameState.Pause:
                    {
                        break;
                    }
                case GameState.GameOver:
                    {
                        break;
                    }
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}