using G01_Perseus.EventSystem.Events;
using G01_Perseus.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    public class MainMenu : GameState
    {

        private GameWindow window;
        private Menu menu;

        public MainMenu(GameWindow window)
        {
            this.window = window;
            menu = new Menu(new Rectangle(100, 200, window.ClientBounds.Width - 200, 500), AssetManager.FontAsset("default_font"));
            menu.Options.Add(new MenuOption("New Game", StartGame));
            menu.Options.Add(new MenuOption("Load Game", () => { Console.WriteLine("Test2"); }));
            menu.Options.Add(new MenuOption("Credits", () => { Console.WriteLine("Test3"); }));
            menu.Options.Add(new MenuOption("Quit", Game1.Shutdown));
            menu.Options.Add(new MenuOption(AssetManager.TextureAsset("beam"), () => { Console.WriteLine("Test5"); }));
        }

        private void StartGame()
        {
            EventManager.Dispatch(new PushStateEvent(new InGameState(Game1.camera)));
            EventManager.Dispatch(new PushStateEvent(new HUD(window)));
        }

        public override void Update(GameTime gameTime)
        {
            menu.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            menu.Draw(spriteBatch, gameTime);
            spriteBatch.End();
        }
    }
}
