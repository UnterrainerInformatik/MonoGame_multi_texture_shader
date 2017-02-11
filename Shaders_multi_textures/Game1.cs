using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Shaders_multi_textures
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private const int MARGIN = 10;

        private string[] Levels { get; } = {"ps_4_0_level_9_1", "ps_4_0" };
        private string[] Method { get; } = {"new", "register", "mixed", "mix_switched", "tex2D"};

        private Dictionary<string, Effect> Effects { get; } = new Dictionary<string, Effect>();
        private Dictionary<string, RenderTarget2D> RenderTargets { get; } = new Dictionary<string, RenderTarget2D>();

        private SpriteFont Font { get; set; }
        private Texture2D Background { get; set; }
        private Texture2D DisplacementMap { get; set; }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1400;
            graphics.PreferredBackBufferHeight = 768;
            graphics.HardwareModeSwitch = false;
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Font = Content.Load<SpriteFont>("Arsenal");
            Background = Content.Load<Texture2D>("background");
            DisplacementMap = Content.Load<Texture2D>("displacement");

            foreach (var level in Levels)
            {
                foreach (var method in Method)
                {
                    string s = append(level, method);
                    Effects.Add(s, Content.Load<Effect>(s));
                    RenderTargets.Add(s,
                        new RenderTarget2D(GraphicsDevice, Background.Width, Background.Height, false,
                            GraphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.None, 0,
                            RenderTargetUsage.DiscardContents));
                }
            }
        }

        private string append(string first, string second)
        {
            return $"{first}_{second}";
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (var level in Levels)
            {
                foreach (var method in Method)
                {
                    string s = append(level, method);
                    Effect e = Effects[s];
                    RenderTarget2D r = RenderTargets[s];

                    GraphicsDevice.SetRenderTarget(r);
                    GraphicsDevice.Clear(ClearOptions.Target, Color.MonoGameOrange, 0, 0);

                    switch (method)
                    {
                        case "new":
                        case "mixed":
                            e.Parameters["SecondTexture"].SetValue(DisplacementMap);
                            break;
                        case "register":
                            GraphicsDevice.Textures[1] = DisplacementMap;
                            break;
                    }
                    spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque, null, null, null, e);
                    spriteBatch.Draw(Background, Vector2.Zero, Color.White);
                    spriteBatch.End();
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            int y = MARGIN;
            foreach (var level in Levels)
            {
                int x = MARGIN;

                Vector2 p = new Vector2(x, y);
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                spriteBatch.DrawString(Font, level, p, Color.Orange, 0, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                spriteBatch.End();

                y += 2*MARGIN;

                foreach (var method in Method)
                {
                    string s = append(level, method);
                    RenderTarget2D r = RenderTargets[s];
                    p = new Vector2(x, y);
                    spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque);
                    spriteBatch.Draw(r, p, Color.White);
                    spriteBatch.End();

                    spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                    Vector2 size = Font.MeasureString(method);
                    Vector2 fontPos = p + new Vector2(Background.Width/2f - size.X/2f, Background.Height + MARGIN);
                    spriteBatch.DrawString(Font, method, fontPos, Color.Orange, 0, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                    spriteBatch.End();

                    x += Background.Width + MARGIN;
                }
                y += Background.Height + 3*MARGIN;
            }

            base.Draw(gameTime);
        }
    }
}
