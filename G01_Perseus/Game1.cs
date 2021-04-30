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

        private StateStack stateStack;

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

            AssetManager.LoadAssets(Content);

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
            Rectangle spawnArea = new Rectangle(0, 10, 0, 10);
            //for(int i = 0; i < 1000; i++)
            //{

            //    Vector2 size = new Vector2(random.Next(1, 5));
            //    Vector2 position = new Vector2(random.Next(spawnArea.X, spawnArea.Y) * tileWidth + random.Next((int)size.X, tileWidth - (int)size.X), random.Next(spawnArea.Width, spawnArea.Height) * tileHeight + random.Next((int)size.Y, tileHeight - (int)size.Y));
            //    //Vector2 position = new Vector2((i % 10) * 50 + 25, (i / 10) * 50 + 25);
            //    spaceObjects.Add(new SpaceObject(spaceObjectTexture, position, size));
            //    level.AddEntity(new Enemy(Util.CreateFilledRectangleTexture(Color.White, (int)size.X, (int)size.Y), position, Vector2.Zero, Vector2.One, null, SpriteEffects.None, Color.White, 0, 0.3f, false, 1, 0));
            //}


            enemy = new Enemy(Util.CreateFilledRectangleTexture(Color.Cyan, 50, 50), new Vector2(50, 50), Vector2.Zero, Vector2.One, null, SpriteEffects.None, Color.White, 0, 0.8f, false, 1, 0);
            level.AddEntity(enemy);

            stateStack = new StateStack();
            stateStack.Push(new InGameState());

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
            KeyMouseReader.Update();

            

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(backgroundColor);

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied, null, null, null, null, camera.Translation);
            level.Draw(spriteBatch, camera);


            EntityManager.Draw(spriteBatch);
            
            //player.Draw(spriteBatch);

            spriteBatch.End();


            //stateStack.Draw(spriteBatch, gameTime);

            //Second draw call to make the HUD independant of the camera/world movement
            spriteBatch.Begin();
            spriteBatch.Draw(AssetManager.TextureAsset("gradient_bar"), new Rectangle(40, Window.ClientBounds.Height - 40, AssetManager.TextureAsset("gradient_bar").Width, AssetManager.TextureAsset("gradient_bar").Height / 2), Color.Crimson);
            spriteBatch.Draw(AssetManager.TextureAsset("gradient_bar"), new Rectangle(40, Window.ClientBounds.Height - 80, AssetManager.TextureAsset("gradient_bar").Width, AssetManager.TextureAsset("gradient_bar").Height / 2), Color.Cyan);


            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
