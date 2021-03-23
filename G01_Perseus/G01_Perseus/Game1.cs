using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace G01_Perseus
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Color backgroundColor;
        Player player;
        public static Camera camera;

        Random random;
        List<SpaceObject> spaceObjects;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }


        protected override void Initialize()
        {
            Util.Device = graphics.GraphicsDevice;

            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player = new Player(new Vector2(250 ,250), 3, Color.Blue, new Point(50, 50));
            camera = new Camera();
            camera.FollowTarget = player;
            camera.Viewport = new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height);

            random = new Random(1);
            Texture2D spaceObjectTexture = Util.CreateTexture(Color.White, 10, 10);
            spaceObjects = new List<SpaceObject>();
            Rectangle spawnArea = new Rectangle(-Window.ClientBounds.Width, Window.ClientBounds.Width, -Window.ClientBounds.Height, Window.ClientBounds.Height);
            for(int i = 0; i < 100; i++)
            {
                Vector2 position = new Vector2(random.Next(spawnArea.X, spawnArea.Y), random.Next(spawnArea.Width, spawnArea.Height));
                Vector2 size = new Vector2(10);
                spaceObjects.Add(new SpaceObject(spaceObjectTexture, position, size));
            }
        


            backgroundColor = Color.Black;
        }


        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update(gameTime);
            camera.Update();

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(backgroundColor);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.Translation);

            foreach(SpaceObject spaceObject in spaceObjects)
            {
                spaceObject.Draw(spriteBatch);
            }

            player.Draw(spriteBatch);

            spriteBatch.End();       

            base.Draw(gameTime);
        }
    }
}
