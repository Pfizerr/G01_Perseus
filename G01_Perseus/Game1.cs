using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace G01_Perseus
{
    public class Game1 : Game
    {
        //test
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Color backgroundColor;
        Player player;
        Level level;
        Enemy enemy;
        public static Camera camera;

        public static Random random;
        List<SpaceObject> spaceObjects;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 920;
        }


        protected override void Initialize()
        {
            Util.Device = graphics.GraphicsDevice;

            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Input.Init();
            EntityManager.CreatePlayer();
            EntityManager.CreateEnemy();
            camera = new Camera();
            camera.FollowTarget = player;
            camera.Viewport = new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height);

            random = new Random(1);

            int tileWidth = 50;
            int tileHeight = 50;
            level = new Level(10, 10, tileWidth, tileHeight);

            Texture2D spaceObjectTexture = Util.CreateFilledRectangleTexture(Color.White, 10, 10);
            spaceObjects = new List<SpaceObject>();
            Rectangle spawnArea = new Rectangle(0, 10, 0, 10);
            for(int i = 0; i < 1000; i++)
            {

                Point size = new Point(random.Next(1, 5));
                Vector2 position = new Vector2(random.Next(spawnArea.X, spawnArea.Y) * tileWidth + random.Next(size.X, tileWidth - size.X), random.Next(spawnArea.Width, spawnArea.Height) * tileHeight + random.Next(size.Y, tileHeight - size.Y));
                //Vector2 position = new Vector2((i % 10) * 50 + 25, (i / 10) * 50 + 25);
                spaceObjects.Add(new SpaceObject(spaceObjectTexture, position, size.ToVector2()));
                level.AddEntity(new Enemy(position, 0.0f, Color.White, size));
            }

            
            enemy = new Enemy(new Vector2(25, 25), 0.0f, Color.Cyan, new Point(50, 50));
            level.AddEntity(enemy);


            backgroundColor = Color.Black;
        }


        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            EntityManager.Update(gameTime);
            //player.Update(gameTime);
            camera.Update();
            Input.Update();

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(backgroundColor);

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied, null, null, null, null, camera.Translation);
            level.Draw(spriteBatch, camera);

            foreach(SpaceObject spaceObject in spaceObjects)
            {
                //spaceObject.Draw(spriteBatch);
            }

            EntityManager.Draw(spriteBatch);

            //player.Draw(spriteBatch);

            spriteBatch.End();       

            base.Draw(gameTime);
        }
    }
}
