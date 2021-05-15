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
    public class MissionInterface : GameState
    {
        private UIButton missionAcceptButton;
        private UIButton missionDenyButton;
        private UIButton exitButton;

        private Rectangle bounds;
        private Texture2D backgroundTexture;

        private Texture2D missionButtonUnavailableTexture;
        private Texture2D missionButtonUnavailableTransparentTexture;

        private Texture2D missionButtonAvailableTexture;
        private Texture2D missionButtonAvailableTransparentTexture;

        private Texture2D missionButtonHoveredTexture;
        private Texture2D missionButtonHoveredTransparentTexture;

        private Mission[] missions;
        private ExtendedTimer[] missionCooldowns;
        private UIButton[] missionSlotSelectButtons;

        private int selectedMission;
        private int hoveredMission;
        private bool isOutOfFocus;

        public MissionInterface(Mission[] missions, ExtendedTimer[] missionSlotTimers)
        {
            this.missionCooldowns = missionSlotTimers;
            this.missions = missions;

            this.missionButtonUnavailableTexture = AssetManager.TextureAsset("button_background_unavailable");
            this.missionButtonUnavailableTransparentTexture = AssetManager.TextureAsset("button_background_unavailable_transparent");

            this.missionButtonAvailableTexture = AssetManager.TextureAsset("button_background_available");
            this.missionButtonAvailableTransparentTexture = AssetManager.TextureAsset("button_background_available_transparent");

            this.missionButtonHoveredTexture = AssetManager.TextureAsset("button_background_hovered");           
            this.missionButtonHoveredTransparentTexture = AssetManager.TextureAsset("button_background_hovered_transparent");

            this.backgroundTexture = AssetManager.TextureAsset("monitor_focused");
            
            this.Transparent = true;

            this.bounds = new Rectangle(Game1.camera.Viewport.Width / 2 - 780 / 2, Game1.camera.Viewport.Height / 2 - 580 / 2, 780, 580);
            
            this.missionSlotSelectButtons = new UIButton[missions.Count()];

            exitButton = new UIButton(new Rectangle(bounds.X + 700, bounds.Y + 100, 50, 59), null, "Exit Menu", AssetManager.FontAsset("default_font"), () =>
            {
                EventManager.Dispatch(new PopStateEvent());
            });

            missionAcceptButton = new UIButton(new Rectangle(bounds.X + 499, bounds.Y + 481, 60, 59), null, "Accept", AssetManager.FontAsset("default_font"), () =>
            {
                if(missions[selectedMission] == null)
                {
                    Console.WriteLine("Selected mission was NULL!");
                    return;
                }

                EventManager.Dispatch(new MissionAcceptedClickEvent(missions[selectedMission]));
            });

            missionDenyButton = new UIButton(new Rectangle(bounds.X + 600, bounds.Y + 481, 60, 59), null, "Deny", AssetManager.FontAsset("default_font"), () => 
            {
                if (missions[selectedMission] == null)
                {
                    Console.WriteLine("Selected mission was NULL!");
                    return;
                }

                EventManager.Dispatch(new MissionDeniedClickEvent(missions[selectedMission]));
            });

            for (int i = 0; i < missions.Count(); i++)
            {
                if(missions[i] == null)
                {
                    //continue;
                }

                string missionText = "";

                if (missions[i] != null)
                {
                    missionText = missions[i].Presentation();
                }

                missionSlotSelectButtons[i] = new UIButton(new Rectangle(bounds.X + 125, bounds.Y + 65 + (85 * i), 550, 80), missionButtonAvailableTexture, missionText, AssetManager.FontAsset("default_font"), () =>
                {
                    // The mission associated with the button which was being hovered before OnClick was called is set to being the selected mission.
                    if(missionSlotSelectButtons[hoveredMission].Texture != missionButtonUnavailableTexture)
                    {
                        selectedMission = hoveredMission;
                    }
                });

                missionSlotSelectButtons[i].HoveredTexture = missionButtonHoveredTexture;
            }
        }

        public override void Update(GameTime gameTime)
        {
            UpdateFocus();
            HandleMouse();

            missionAcceptButton.Update(gameTime);
            missionDenyButton.Update(gameTime);
            exitButton.Update(gameTime);

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

        private void HandleMouse()
        {
            if(isOutOfFocus)
            {
                if(backgroundTexture != AssetManager.TextureAsset("monitor_not_focused"))
                {
                    backgroundTexture = AssetManager.TextureAsset("monitor_not_focused");
                }

                if(KeyMouseReader.LeftClick())
                {
                    EventManager.Dispatch(new PopStateEvent()); // Leave mission interface.
                }
            }
            else if (backgroundTexture != AssetManager.TextureAsset("monitor_focused"))
            {
                backgroundTexture = AssetManager.TextureAsset("monitor_focused");
            }
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
        
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(backgroundTexture, bounds, null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 1.0f);

            exitButton.Draw(spriteBatch, gameTime);
            missionAcceptButton.Draw(spriteBatch, gameTime);
            missionDenyButton.Draw(spriteBatch, gameTime);
            
            for(int i = 0; i < missionSlotSelectButtons.Count(); i++)
            {
                missionSlotSelectButtons[i].Draw(spriteBatch, gameTime);
            }

            spriteBatch.End();
        }
    }
}
