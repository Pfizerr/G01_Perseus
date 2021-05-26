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
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Color backgroundColor;

        public static Camera camera;
        public static Random random = new Random(1);
        private static StateStack stateStack;

        private static bool shouldQuit;

        //Note: make a class for error messages that takes a string as an input for the player to see what is wrong if there is time left over


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            graphics.PreparingDeviceSettings += Graphics_PreparingDeviceSettings;
            graphics.ApplyChanges();

            EventManager.Register(this);
        }


        protected override void Initialize()
        {
            Util.Device = graphics.GraphicsDevice;

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();

            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            AssetManager.LoadAssets(Content);
            MissionManager.LoadMissions();

            EntityManager.AddFaction(new Faction("test faction one", AssetManager.SpriteAsset("planet1"), AssetManager.SpriteAsset("planet_highlight_outline2_green")));
            EntityManager.factions[0].CreatePlanet("planet name one", new Vector2(0, 0), 3);
            EntityManager.CreatePlayer();
            EntityManager.CreateEnemy(new Vector2(250, 250));
            Resources.Initialize(0, 0, 0, 0, 0, 0, 0, 0, 1);

            camera = new Camera(Window);

            random = new Random(1);



            stateStack = new StateStack();
            stateStack.Push(new MainMenu(Window));
            //stateStack.Push(new InGameState(camera));
            //stateStack.Push(new HUD(Window));     
            backgroundColor = Color.Black;
        }

        protected override void UnloadContent()
        {

        }

        public static void Shutdown()
        {
            shouldQuit = true;
        }

        private void Graphics_PreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {
            graphics.PreferMultiSampling = true;
            e.GraphicsDeviceInformation.PresentationParameters.MultiSampleCount = 8;
        }

        protected override void Update(GameTime gameTime)
        {
            if (shouldQuit)
            {
                Exit();
            }

            KeyMouseReader.Update();
            stateStack.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(backgroundColor);           

            stateStack.Draw(spriteBatch, gameTime);
        }

        public void OnPopState(PopStateEvent e)
        {
            stateStack.Pop();
            Console.WriteLine("Popped State: " + e);
        }

        public void OnPushState(PushStateEvent e)
        {
            stateStack.Push(e.State);
            Console.WriteLine("Pushed State: " + e.State);
        }
    }
}