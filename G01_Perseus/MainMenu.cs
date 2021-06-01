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
        private Texture2D panelTexture;
        private Menu menu;
        private Background background;
        public MainMenu(GameWindow window)
        {
            this.window = window;

            this.background = new Background(2000, 2000, 5);
            this.panelTexture = AssetManager.TextureAsset("start_menu_background_UI");

            menu = new Menu(new Rectangle((int)(window.ClientBounds.Width / 2 - ((panelTexture.Width * 1.5f) / 2)) + 50, (int)(window.ClientBounds.Height * 0.3f) + 50, (int)(panelTexture.Width * 1.5f) - 100, (int)(panelTexture.Height * 1.5f) - 100), AssetManager.FontAsset("main_menu_font"));
            menu.Options.Add(new MenuOption("New Game", StartGame));
            menu.Options.Add(new MenuOption("Load Game", () => { Console.WriteLine("Test2"); }));
            menu.Options.Add(new MenuOption("Credits", () => { Console.WriteLine("Test3"); }));
            menu.Options.Add(new MenuOption("Quit", Game1.Shutdown));
           
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
            //spriteBatch.Draw(background, new Rectangle(0, 0, window.ClientBounds.Width, window.ClientBounds.Height), Color.White);
            background.Draw(spriteBatch, new Rectangle(0,0,window.ClientBounds.Width, window.ClientBounds.Height), new Vector2(0,0));
            spriteBatch.Draw(panelTexture, new Vector2(window.ClientBounds.Width / 2 - ((panelTexture.Width * 1.5f) / 2), window.ClientBounds.Height * 0.3f), null, Color.White, 0.0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0.5f);
            menu.Draw(spriteBatch, gameTime);
            spriteBatch.End();
        }
    }
}
