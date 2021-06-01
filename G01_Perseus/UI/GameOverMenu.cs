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
        private UIButton playerRespawnButton;
        private UIButton openMainMenuButton;
        private UIButton exitGameButton;

        public RespawnMenu(Texture2D backgroundTexture)
        {
            this.bounds = new Rectangle((int)(Game1.camera.Viewport.Width * 0.5f) - 500 / 2, (int)(Game1.camera.Viewport.Height * 0.5f) - 350 / 2, 500, 350); 
            this.backgroundTexture = backgroundTexture;

            SpriteFont font = AssetManager.FontAsset("default_font");
            Texture2D buttonTexture = Util.CreateFilledRectangleTexture(Color.DarkGray, 350, 70);

            playerRespawnButton = new UIButton(new Rectangle(new Point(bounds.Center.X - 350 / 2, bounds.Center.Y - 100), new Point(350, 70)), buttonTexture, "Respawn Player", font, () =>
            {
                RespawnPlayerButton(); 
            });

            openMainMenuButton = new UIButton(new Rectangle(new Point(bounds.Center.X - 350 / 2, bounds.Center.Y + 0), new Point(350, 70)), buttonTexture, "Return To Menu", font, () =>
            {
                ReturnToMenuButton();
            });

            exitGameButton = new UIButton(new Rectangle(new Point(bounds.Center.X - 350 / 2, bounds.Center.Y + 100), new Point(350, 70)), buttonTexture, "Exit Game", font, () =>
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
            playerRespawnButton.Draw(spriteBatch, gameTime);
            openMainMenuButton.Draw(spriteBatch, gameTime);
            exitGameButton.Draw(spriteBatch, gameTime);

            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            playerRespawnButton.Update(gameTime);
            openMainMenuButton.Update(gameTime);
            exitGameButton.Update(gameTime);
        }

        private void RespawnPlayerButton()
        {
            Debug.WriteLine("Player respawned");
            PlayerRespawn();
            EventManager.Dispatch(new PopStateEvent());
        }   
        
        private void ReturnToMenuButton()
        {
            Debug.WriteLine("Returned to menu");
            EventManager.Dispatch(new PopStateEvent());
            EventManager.Dispatch(new PopStateEvent());
            PlayerRespawn();
        }

        private void ExitGameButton()
        {
            Debug.WriteLine("Game exits");
        }


        /// <summary>
        /// MOVE THIS SOMEWHERE.
        /// </summary>
        private void PlayerRespawn()
        {
            EntityManager.CreatePlayer();
            Game1.camera.FollowTarget = EntityManager.Player;
        }
    }
}
