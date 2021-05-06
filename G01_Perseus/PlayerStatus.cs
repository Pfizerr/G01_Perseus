using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    /// <summary>
    /// This class contains the variables controlling player status. These are variables that are directly relevant and visible to the player.
    /// </summary>
    public class PlayerStatus
    {
        private SpriteFont font;
        public float MaxHealth
        {
            get;
            set;
        }
        public float Health
        {
            get;
            set;
        }
        public float MaxShields
        {
            get;
            set;
        }
        public float Shields
        {
            get;
            set;
        }
        public int SkillPoints
        {
            get;
            set;
        }
        public int Level
        {
            get;
            set;
        }
        public int Experience
        {
            get;
            set;
        }
        public int Resources
        {
            get;
            set;
        }
        public int Dust
        {
            get;
            set;
        }

        public List<Mission> Mission
        {
            get;
            set;
        }

        public List<Mission> CompletedMissions
        {
            get;
            set;
        }

        /*private float maxShields;
        private float shields;
        
        private int skillPoints;
        private int level;
        private int experience;
        private int resources;
        private int dust;*/
        
        /// <summary>
        /// New status constructor.
        /// </summary>
        /// <param name="maxHealth"></param>
        /// <param name="maxShields"></param>
        public PlayerStatus(float maxHealth, float maxShields) : this()
        {
            MaxHealth = maxHealth;
            MaxShields = maxShields;
        }

        /// <summary>
        /// Existing status constructor.
        /// </summary>
        /// <param name="maxHealth"></param>
        /// <param name="maxShields"></param>
        /// <param name="skillPoints"></param>
        /// <param name="level"></param>
        /// <param name="experience"></param>
        /// <param name="resources"></param>
        /// <param name="dust"></param>
        public PlayerStatus(float maxHealth, float maxShields, int skillPoints, int level, int experience, int resources, int dust) : this()
        {
            MaxHealth = maxHealth;
            MaxShields = maxShields;
            SkillPoints = skillPoints;
            Level = level;
            Experience = experience;
            Resources = resources;
            Dust = dust;
        }

        /// <summary>
        /// This constructor is always called.
        /// </summary>
        public PlayerStatus()
        {
            Mission = new List<Mission>();

            font = AssetManager.FontAsset("default_font");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            #region Debug output
            Rectangle viewport = Game1.camera.Viewport;
            Vector2 playerPosition = EntityManager.Player.Center;
            Vector2 position;
            Vector2 size;

            for (int i = 0; i < Mission.Count; i++)
            {
                string str = String.Format("name: {0} <{1}> | owner: {2} | contractor: {3} | {4} / {5}", 
                    Mission[i].Name,
                    Mission[i].Id,
                    Mission[i].Owner,
                    Mission[i].Contractor,
                    Mission[i].Tracker.TasksCompleted.ToString(),
                    Mission[i].Tracker.TasksToComplete.ToString());

                size = font.MeasureString(str);
                position = new Vector2(playerPosition.X - viewport.Width * 0.5f + 15, playerPosition.Y - viewport.Height * 0.5f + size.Y * i + 5);
                spriteBatch.DrawString(font, str, position, Color.White);
            }

            string[] strs = new string[]
            {
                "Resources: " + Resources,
                "Dust: " + Dust,
                "Skillpoints: " + SkillPoints
            };

            for (int i = 0; i < strs.Count(); i++)
            {
                size = font.MeasureString(strs[i]);
                position = new Vector2(playerPosition.X - viewport.Width * 0.5f + 5, playerPosition.Y + viewport.Height * 0.5f  - 100 - (size.Y + 20) * i);
                spriteBatch.DrawString(font, strs[i], position, Color.White);
            }
            #endregion
        }
    }
}
