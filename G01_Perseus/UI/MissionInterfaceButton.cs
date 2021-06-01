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
    public class MissionInterfaceButton
    {
        private Texture2D backgroundSelected;
        private Texture2D backgroundAvailable;
        private Texture2D backgroundUnavailable;

        public MissionInterfaceButton(Rectangle bounds, Texture2D backgroundAvailable, Texture2D backgroundSelected, Texture2D backgroundUnavailable, Mission mission, ExtendedTimer timer, string text, SpriteFont font)
        {
            this.backgroundSelected = backgroundSelected;
            this.backgroundAvailable = backgroundAvailable;
            this.backgroundUnavailable = backgroundUnavailable;

            Mission = mission;
            Timer = timer;

            Button = new UIButton(bounds, backgroundAvailable, text, font, () =>
            {
                IsSelected = true;
            });

            Button.HoveredTexture = backgroundSelected;
        }

        public MissionInterfaceButton(UIButton button, Mission mission, ExtendedTimer timer)
        {
            Button = button;
            Mission = mission;
            Timer = timer;
        }

        public Mission Mission
        {
            get;
            private set;
        }

        public ExtendedTimer Timer
        {
            get;
            private set;
        }

        public UIButton Button
        {
            get;
            private set;
        }

        public bool IsHovered
        {
            get;
            private set;
        }

        public bool IsSelected
        {
            get;
            set;
        }

        public float Opacity
        {
            get;
            set;
        }

        public void Update(GameTime gameTime)
        {
            if (Button.Opacity != Opacity)
            {
                Button.Opacity = Opacity;
            }

            UpdateTexture();


            Button.Update(gameTime);
            Timer.Update(gameTime);

            if (Mission != null)
            {
                Mission.Update();
            }
        }

        private void UpdateTexture()
        {
            if (Mission == null || Mission.State != Mission.EState.Offered)
            {
                Button.Texture = backgroundUnavailable;
                Button.HoveredTexture = backgroundUnavailable;
                Button.Text = Convert.ToInt32(Timer.TimeRemaining / 1000).ToString() + " s"; // Update button text with current timer time.
            }
            else if (IsSelected)
            {
                Button.Texture = backgroundSelected;
            }
            else
            {
                Button.Texture = backgroundAvailable;
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime) => Button.Draw(spriteBatch, gameTime);
    }
}
