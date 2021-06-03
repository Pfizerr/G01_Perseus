using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace G01_Perseus.UI
{
    public class GameOver : GameState
    {
        private GameWindow window;
        private Vector2 mainTextPosition;
        private Vector2 subTextPosition;
        private Vector2 panelPosition;
        private Vector2 mainTextSize;
        private Vector2 subTextSize;
        private string mainTextString;
        private string subTextString;
        private SpriteFont mainTextFont;
        private SpriteFont subTextFont;
        private Texture2D panel;
        private float restartDisplay = 10;
        private Timer restartTimer;
        StateStack stateStack;
        bool hasBeenReset = false;
        public GameOver(GameWindow window, StateStack stateStack)
        {
            this.stateStack = stateStack;
            restartTimer = new Timer(100);
            this.window = window;
            Transparent = true;
            mainTextString = "GAME OVER";
            subTextString = "Reloading latest save in: ";
            mainTextFont = AssetManager.FontAsset("main_menu_font");
            subTextFont = AssetManager.FontAsset("main_menu_font");
            panel = AssetManager.TextureAsset("start_menu_background_UI");
            SetTextSize();
            SetPositions(10);


        }

        public override bool Transparent { get => base.Transparent; protected set => base.Transparent = value; }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(panel, panelPosition, Color.White);
            spriteBatch.DrawString(mainTextFont, mainTextString, mainTextPosition, Color.White);
            spriteBatch.DrawString(subTextFont, subTextString, subTextPosition, Color.White);
            
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            if (!hasBeenReset)
            {
                restartTimer.Reset(gameTime);
                hasBeenReset = true;
            }
            if (restartTimer.IsDone(gameTime))
            {
                stateStack.Pop();
                Serializer.LoadGame();
                
            }
            if (restartDisplay >= 0)
            {
                subTextString = "Reloading latest save in: " + (int)(restartDisplay -= (float)gameTime.ElapsedGameTime.TotalSeconds);
            }
        }

        private void SetPositions(float offSet)
        {
            mainTextPosition = new Vector2(window.ClientBounds.Width / 2, window.ClientBounds.Height / 2 - mainTextSize.Y) - mainTextSize / 2;
            subTextPosition = new Vector2(window.ClientBounds.Width / 2, window.ClientBounds.Height / 2) - subTextSize / 2;
            panelPosition = new Vector2((window.ClientBounds.Width - panel.Width) / 2, (window.ClientBounds.Height - panel.Height - mainTextSize.Y) / 2);
        }

        private void SetTextSize()
        {
            mainTextSize = mainTextFont.MeasureString(mainTextString);
            subTextSize = subTextFont.MeasureString(subTextString);
        }
    }
}
