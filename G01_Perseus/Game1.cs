using G01_Perseus.EventSystem.Events;
using G01_Perseus.EventSystem.Listeners;
using G01_Perseus.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace G01_Perseus
{
    public class Game1 : Game, PushStateListener, PopStateListener
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

        public static StateStack stateStack;

        //Note: make a class for error messages that takes a string as an input for the player to see what is wrong if there is time left over

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 920;

            EventManager.Register(this);
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

            int tileWidth = 1180;
            int tileHeight = 1080;
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


            //enemy = new Enemy(Util.CreateFilledRectangleTexture(Color.Cyan, 50, 50), new Vector2(50, 50), Vector2.Zero, Vector2.One, null, SpriteEffects.None, Color.White, 0, 0.8f, false, 1, 0);
            //level.AddEntity(enemy);

            stateStack = new StateStack();
            Resources.Initialize(0, 0, 0, 0, 0, 0, 0, 0, 1);
            stateStack.Push(new HUD(Window)); // MARKUS, Avkommentera denna raden för att testa quest loggen            
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
            stateStack.Update(gameTime);

            

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


            stateStack.Draw(spriteBatch, gameTime);
            base.Draw(gameTime);
        }

        public void OnPopState(PopStateEvent e)
        {
            stateStack.Pop();
            Console.WriteLine("Popped State: "+e);
        }

        public void OnPushState(PushStateEvent e)
        {
            stateStack.Push(e.State);
            Console.WriteLine("Pushed State: "+e);
        }
    }
}
