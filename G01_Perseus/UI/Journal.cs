using G01_Perseus.EventSystem.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus.UI
{
    /*public class Journal : GameState
    {
        private Rectangle bounds;
        private Rectangle scrollableRegionBounds;
        private bool isScrollable;
        private int pxOnStep; // amount of pixels a single scroll-step will move missions by
        private List<Mission> missions;
        private List<UIButton> buttons;
        private Texture2D backgroundTexture;

        private UIButton exitButton;

        public Journal(List<Mission> missions)
        {
            this.Transparent = true;

            this.bounds = new Rectangle(450,200, 250, 400);
            this.scrollableRegionBounds = new Rectangle(bounds.X + 10, bounds.Y + 10, bounds.Width - 20, bounds.Height - 20);
            this.backgroundTexture = Util.CreateFilledRectangleTexture(new Color(255, 255, 255, 255), 1, 1);
            
            this.exitButton = new UIButton(new Rectangle(bounds.X + 10, bounds.Y + 10, 50, 50), () => 
            {
                EventManager.Dispatch(new PopStateEvent());
            });

            for(int i = 0; i < missions.Count; i++)
            {
                //Turn in mission
                buttons.Add(new UIButton())
                
                    buttons.Add(new UIButton()) 
            }
        }

        private void RemoveMission(Mission mission)
        {
            for(ion)
        }

        public override void Update(GameTime gameTime)
        {
            exitButton.Update(gameTime);

            if(isScrollable && KeyMouseReader.mouseState.ScrollWheelValue < KeyMouseReader.oldMouseState.ScrollWheelValue)
            {

            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTexture, bounds, null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 1.0f);
            exitButton.Draw(spriteBatch, gameTime);
            spriteBatch.End();
        }
    }*/
}
