using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace G01_Perseus
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Color backgroundColor;

        private Player player;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {

            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            player = new Player(new Vector2(0 ,0), new Vector2(1, 1), Color.Blue, new Point(32, 32));
        }


        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update(gameTime);


            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            player.Draw(spriteBatch);

            spriteBatch.End();       

            base.Draw(gameTime);
        }

        public static class Util
        {
            private static GraphicsDevice device;
            public static GraphicsDevice Device { private get => Util.device; set => Util.device = value; }
    
            public static Texture2D CreateTexture(Color pxColor, int pxWidth, int pxHeight)
            {
                Texture2D texture = new Texture2D(device, pxWidth, pxHeight);
                Color[] data = new Color[pxWidth * pxHeight];
                for(int px = 0; px < data.Length; px++)
                {
                    data[px] = pxColor;
                }
                texture.SetData(data);
                return texture;
            }
        }
    }
}
