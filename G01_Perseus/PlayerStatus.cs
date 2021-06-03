using G01_Perseus.EventSystem.Events;
using G01_Perseus.EventSystem.Listeners;
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
    public class PlayerStatus : MissionTurnedInListener, MissionRemovedListener
    {
        private SpriteFont font;
        public float MaxHealth { get; set;}
        public float Health { get; set;}
        public float MaxShields { get; set;}
        public float Shields { get; set;}

        public List<Mission> Missions { get; set;}

        public List<Mission> CompletedMissions { get; set;}

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
        /// This constructor is always called.
        /// </summary>
        public PlayerStatus()
        {
            Missions = new List<Mission>();
            CompletedMissions = new List<Mission>();

            font = AssetManager.FontAsset("default_font");
            EventManager.Register(this);
        }

        public void Update()
        {
            foreach(Mission mission in Missions)
            {
                mission.Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            #region Debug output
            Rectangle viewport = Game1.camera.Viewport;
            Vector2 playerPosition = EntityManager.Player.Center;
            Vector2 position;
            Vector2 size;

            for (int i = 0; i < Missions.Count; i++)
            {
                string str = String.Format("name: {0} <{1}> | owner: {2} | contractor: {3} | {4} / {5}", 
                    Missions[i].Name,
                    Missions[i].Id,
                    Missions[i].Owner,
                    Missions[i].Contractor,
                    Missions[i].Tracker.TasksCompleted.ToString(),
                    Missions[i].Tracker.TasksToComplete.ToString());

                size = font.MeasureString(str);
                position = new Vector2(playerPosition.X - viewport.Width * 0.5f + 15, playerPosition.Y - viewport.Height * 0.5f + size.Y * i + 5);
                spriteBatch.DrawString(font, str, position, Color.White);
            }

            //string[] strs = new string[]
            //{
            //    "Resources: " + Resources,
            //    "Dust: " + Dust,
            //    "Skillpoints: " + SkillPoints
            //};

            //for (int i = 0; i < strs.Count(); i++)
            //{
            //    size = font.MeasureString(strs[i]);
            //    position = new Vector2(playerPosition.X - viewport.Width * 0.5f + 5, playerPosition.Y + viewport.Height * 0.5f  - 100 - (size.Y + 20) * i);
            //    spriteBatch.DrawString(font, strs[i], position, Color.White);
            //}
            #endregion
        }

        public void OnTurnIn(MissionTurnedInEvent e)
        {
            Resources.AddDust(e.Mission.Dust);
            Resources.AddCurrency(e.Mission.Currency);
            Resources.AddXP(e.Mission.SkillPoints);
            Missions.Remove(e.Mission);
        }

        public void OnRemoved(MissionRemovedEvent e)
        {
            Missions.Remove(e.Mission);
        }
    }
}
