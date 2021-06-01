using G01_Perseus.EventSystem.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus.UI
{

/*public override void Update(GameTime gameTime)
        {
            UpdateFocus();
            HandleInput();

            missionAcceptButton.Update(gameTime);
            missionDenyButton.Update(gameTime);

            for (int i = 0; i < missionSlotSelectButtons.Count(); i++)
            {
                missionSlotSelectButtons[i].Update(gameTime);

                if (missionSlotSelectButtons[i].IsMouseHovered) // Check if mouse is hovering button
                {
                    hoveredMission = i; // This value will be used to determine which button (and by association, which mission) was clicked when OnClick is called.
                }

                if (missions[i] == null) // If the slot is empty (i.e. mission is NULL)
                {
                    if (missionSlotSelectButtons[i].Texture != TrySwitchTexture(missionButtonUnavailableTexture, isOutOfFocus)) // If texture currently is set to something other than .."Unavailable"..
                    {
                        missionSlotSelectButtons[i].Texture = TrySwitchTexture(missionButtonUnavailableTexture, isOutOfFocus); // : set to .."Unavailable"..
                    }
                    else if (missionSlotSelectButtons[i].HoveredTexture != TrySwitchTexture(missionButtonUnavailableTexture, isOutOfFocus))// If hovered texture currently is set to something other than .."Unavailable"..
                    {
                        missionSlotSelectButtons[i].HoveredTexture = TrySwitchTexture(missionButtonUnavailableTexture, isOutOfFocus); // : set to .."Unavailable"..
                    }

                    missionSlotSelectButtons[i].Text = Convert.ToInt32(missionCooldowns[i].TimeRemaining / 1000).ToString() + " s"; // Update button text with current timer time.
                }
                else // Slot is not empty
                {
                    if (missionSlotSelectButtons[i].Texture == TrySwitchTexture(missionButtonUnavailableTexture, isOutOfFocus)) // If texture is currently set to .."Unavailable"..
                    {
                        missionSlotSelectButtons[i].Texture = TrySwitchTexture(missionButtonAvailableTexture, isOutOfFocus); // : set to .."Available"..
                        missionSlotSelectButtons[i].HoveredTexture = TrySwitchTexture(missionButtonUnavailableTexture, isOutOfFocus); // : set to .."Available"..
                        missionSlotSelectButtons[i].Text = missions[i].Presentation(); // To get rid of the timer text and get the mission description back.
                    }
                    else if (missionSlotSelectButtons[i] == missionSlotSelectButtons[selectedMission]) // If the associated mission is the selected mission
                    {
                        missionSlotSelectButtons[i].Texture = missionButtonHoveredTexture; // : set texture to hovered(also used when selected) texture.
                    }
                    else if (missionSlotSelectButtons[i] != missionSlotSelectButtons[selectedMission]) // If associated mission is not selected,
                    {
                        missionSlotSelectButtons[i].Texture = missionButtonAvailableTexture; // : set texture to regular available texture.
                    }
                }

                missionSlotSelectButtons[i].Texture = TrySwitchTexture(missionSlotSelectButtons[i].Texture, isOutOfFocus);
                missionSlotSelectButtons[i].HoveredTexture = TrySwitchTexture(missionSlotSelectButtons[i].HoveredTexture, isOutOfFocus);
            }
        }

        private Texture2D TrySwitchTexture(Texture2D texture, bool isOutOfFocus)
        {
            if(isOutOfFocus)
            {
                if (texture == missionButtonAvailableTexture)
                {
                    return missionButtonAvailableTransparentTexture;
                }
                else if (texture == missionButtonHoveredTexture)
                {
                    return missionButtonHoveredTransparentTexture;
                }
                else if (texture == missionButtonUnavailableTexture)
                {
                    return missionButtonUnavailableTransparentTexture;
                }
            }
            else
            {
                if (texture == missionButtonAvailableTransparentTexture)
                {
                    return missionButtonAvailableTexture;
                }
                else if (texture == missionButtonHoveredTransparentTexture)
                {
                    return missionButtonHoveredTexture;
                }
                else if (texture == missionButtonUnavailableTransparentTexture)
                {
                    return missionButtonUnavailableTexture;
                }
            }

            return texture;
        }

        public void UpdateFocus()
        {
            if(KeyMouseReader.MouseScreenPosition.X < bounds.Left
                || KeyMouseReader.MouseScreenPosition.X > bounds.Right
                || KeyMouseReader.MouseScreenPosition.Y < bounds.Top
                || KeyMouseReader.MouseScreenPosition.Y > bounds.Bottom)
            {
                isOutOfFocus = true;
                return;
            }

            isOutOfFocus = false;
        }

        public void ExitUserInterface()
        {
            EventManager.Dispatch(new PopStateEvent());
        }
        
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(backgroundTexture, bounds, null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 1.0f);

            missionAcceptButton.Draw(spriteBatch, gameTime);
            missionDenyButton.Draw(spriteBatch, gameTime);
            
            for(int i = 0; i < missionSlotSelectButtons.Count(); i++)
            {
                missionSlotSelectButtons[i].Draw(spriteBatch, gameTime);
            }

            spriteBatch.End();
        }
    }*/
}
