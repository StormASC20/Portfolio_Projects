﻿using Microsoft.Xna.Framework;
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
        private GameState state;

        private Button startButton;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            state = GameState.MainMenu;
            startButton = new Button(_graphics.PreferredBackBufferWidth / 3, _graphics.PreferredBackBufferHeight - 200, 200, 100);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            menuFont = this.Content.Load<SpriteFont>("MenuFont");
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState kbState = Keyboard.GetState();

            switch(state)
            {
                case GameState.MainMenu:
                    {
                        if(startButton.IsPressed())
                        {
                            state = GameState.Game;
                        }
                        if (kbState.IsKeyDown(Keys.G))
                        {
                            state = GameState.GameOver;
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

                        break;
                    }
                case GameState.Game:
                    {
                        _spriteBatch.DrawString(
                            menuFont,
                            "This is the Game (test)",
                            new Vector2(_graphics.PreferredBackBufferWidth / 6, _graphics.PreferredBackBufferHeight / 4),
                            Color.SaddleBrown);

                        break;
                    }
                case GameState.Pause:
                    {
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