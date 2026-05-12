using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Monogame_1._4__Part_2____Making_it_Snow
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D flake;

        Rectangle window;
        Rectangle tempSnowFlake;

        Random generator = new Random();
        Random colorgenerator = new Random();

        List<Rectangle> snowFlakes;

        Vector2 fallSpeed;

        List<Color> colors = new List<Color>() { Color.Thistle, Color.Red, Color.LightGoldenrodYellow, Color.Gainsboro };
        List<Color> snowflakeColor = new List<Color>();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            window = new Rectangle(0, 0, 800, 500);
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.ApplyChanges();

            snowFlakes = new List<Rectangle>();

            Rectangle tempSnowFlake;
            for (int i = 0; i < 50; i++)
            {
                tempSnowFlake = new Rectangle(
                    generator.Next(window.Width),
                    generator.Next(window.Height),
                    8,
                    8);
                snowFlakes.Add(tempSnowFlake);
                snowflakeColor.Add(colors[colorgenerator.Next(colors.Count)]);
            }

            fallSpeed = new Vector2(0, 2);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            flake = Content.Load<Texture2D>("flake");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            for (int i = 0; i < snowFlakes.Count; i++)
            {
                snowFlakes[i] = new Rectangle(
                    snowFlakes[i].X + (int)fallSpeed.X,
                    snowFlakes[i].Y + (int)fallSpeed.Y,
                    snowFlakes[i].Width,
                    snowFlakes[i].Height);

                if (snowFlakes[i].Y > window.Height)
                {
                    snowFlakes[i] = new Rectangle(
                        generator.Next(window.Width),
                        generator.Next(-10,0),
                        8,
                        8);
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            for (int i = 0; i < snowFlakes.Count; i++)
            {
                _spriteBatch.Draw(flake, snowFlakes[i], snowflakeColor[i]);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
