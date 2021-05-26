using G01_Perseus.EventSystem.Events;
using G01_Perseus.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    public class RespawnMenu : GameState
    {

        private Rectangle bounds;
        private Texture2D backgroundTexture;
        private UIButton respawnPlayerButton;
        private UIButton returnToMenuButton;
        private UIButton exitGameButton;

        public RespawnMenu(Texture2D backgroundTexture)
        {
            this.bounds = new Rectangle((int)(Game1.camera.Viewport.Width * 0.5f) - 500 / 2, (int)(Game1.camera.Viewport.Height * 0.5f) - 350 / 2, 500, 350); 
            this.backgroundTexture = backgroundTexture;

            SpriteFont font = AssetManager.FontAsset("default_font");
            Texture2D btnTexture = Util.CreateFilledRectangleTexture(Color.DarkGray, 350, 70);

            respawnPlayerButton = new UIButton(new Rectangle(new Point(bounds.Center.X - 350 / 2, bounds.Center.Y - 100), new Point(350, 70)), btnTexture, "Respawn Player", font, () =>
            {
                RespawnPlayerButton(); 
            });

            returnToMenuButton = new UIButton(new Rectangle(new Point(bounds.Center.X - 350 / 2, bounds.Center.Y + 0), new Point(350, 70)), btnTexture, "Return To Menu", font, () =>
            {
                ReturnToMenuButton();
            });

            exitGameButton = new UIButton(new Rectangle(new Point(bounds.Center.X - 350 / 2, bounds.Center.Y + 100), new Point(350, 70)), btnTexture, "Exit Game", font, () =>
            {
                ExitGameButton();
            });
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();

            #region DEBUG
            Texture2D tempTexture = Util.CreateFilledRectangleTexture(Color.White, bounds.X, bounds.Y);
            #endregion

            spriteBatch.Draw(/*backgroundTexture*/ tempTexture, bounds, null, Color.DarkSlateGray, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            respawnPlayerButton.Draw(spriteBatch, gameTime);
            returnToMenuButton.Draw(spriteBatch, gameTime);
            exitGameButton.Draw(spriteBatch, gameTime);

            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            respawnPlayerButton.Update(gameTime);
            returnToMenuButton.Update(gameTime);
            exitGameButton.Update(gameTime);
        }

        private void RespawnPlayerButton()
        {
            Debug.WriteLine("Player respawned");
            EntityManager.CreatePlayer();
            Game1.camera.FollowTarget = EntityManager.Player;
            EventManager.Dispatch(new PopStateEvent());
        }   
        
        private void ReturnToMenuButton()
        {
            Debug.WriteLine("Returned to menu");
            EventManager.Dispatch(new PopStateEvent());
        }

        private void ExitGameButton()
        {
            Debug.WriteLine("Game exits");
        }
    }
}
