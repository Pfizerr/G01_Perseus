using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    public static class Serializer
    {

        private static string FILE_NAME = "Save.txt";


        public static void SaveGame()
        {

            StreamWriter writer = new StreamWriter(FILE_NAME);

            writer.WriteLine("Currency=" + Resources.Currency);
            writer.WriteLine("Dust=" + Resources.Dust);
            writer.WriteLine("SkillPoints=" + Resources.SkillPoints);
            writer.WriteLine("SpDamage=" + Resources.SpDamage.ToString());
            writer.WriteLine("SpHealth=" + Resources.SpHealth.ToString());
            writer.WriteLine("SpShields=" + Resources.SpShields.ToString());
            writer.WriteLine("SpFireRate=" + Resources.SpFireRate.ToString());
            writer.WriteLine("MaxPoints=" + Resources.MaxPoints.ToString());
            writer.WriteLine("Level=" + Resources.Level);
            writer.WriteLine("XP=" + Resources.XP.ToString());
            writer.WriteLine("XPToNextLevel=" + Resources.XPToNextLevel.ToString());

            writer.WriteLine("PlayerPositionX=" + EntityManager.Player.Position.X);
            writer.WriteLine("PlayerPositionY=" + EntityManager.Player.Position.Y);
            writer.WriteLine("PlayerHealth=" + EntityManager.Player.Health);
            writer.WriteLine("PlayerShields=" + EntityManager.Player.Shields);
            //writer.WriteLine("PlayerSelectedWeapon=" + EntityManager.Player.EquipedWeapon.Name);
            for(int i = 0; i < EntityManager.Player.Weapons.Count; i++)
            {
                writer.WriteLine("PlayerWeapon" + i + "=" + EntityManager.Player.Weapons[i].Name);
            }

            for (int i = 0; i < EntityManager.Player.playerStatus.Missions.Count; i++)
            {
                writer.WriteLine("Mission" + EntityManager.Player.playerStatus.Missions[i].Id + "=" + EntityManager.Player.playerStatus.Missions[i].Id);
                writer.WriteLine("Mission" + EntityManager.Player.playerStatus.Missions[i].Id + "Tracker=" + EntityManager.Player.playerStatus.Missions[i].Tracker.TasksCompleted);
            }
            for (int i = 0; i < EntityManager.Player.playerStatus.CompletedMissions.Count; i++)
            {
                //writer.WriteLine("MissionCompleted" + i + "=" + EntityManager.Player.playerStatus.CompletedMissions[i].Id);
            }


            writer.Close();
        }

        public static void LoadGame()
        {
            StreamReader reader = new StreamReader(FILE_NAME);

            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            string line;
            while((line = reader.ReadLine()) != null)
            {
                string key = line.Split('=')[0];
                string value = line.Split('=')[1];

                keyValues.Add(key, value);
            }

            int currency = int.Parse(keyValues["Currency"]);
            int dust = int.Parse(keyValues["Dust"]);
            int skillPoints = int.Parse(keyValues["SkillPoints"]);
            float spDamage = float.Parse(keyValues["SpDamage"]);
            float spHealth = float.Parse(keyValues["SpHealth"]);
            float spShields = float.Parse(keyValues["SpShields"]);
            float spFireRate = float.Parse(keyValues["SpFireRate"]);
            float maxPoints = float.Parse(keyValues["MaxPoints"]);
            int level = int.Parse(keyValues["Level"]);
            float xp = float.Parse(keyValues["XP"]);
            float xpToNextLevel = float.Parse(keyValues["XPToNextLevel"]);

            Vector2 playerPos = new Vector2(float.Parse(keyValues["PlayerPositionX"]), float.Parse(keyValues["PlayerPositionY"]));
            int playerHealth = int.Parse(keyValues["PlayerHealth"]);
            int playerShields = int.Parse(keyValues["PlayerShields"]);

            Resources.Initialize(currency, dust, skillPoints, (int)spDamage, (int)spShields, (int)spHealth, (int)spFireRate, xp, level); // Weird casting because its stored as float but takes int as argument
            EntityManager.ReInit();
            InGameState.InitGame();

            EntityManager.Player.Position = playerPos;
            EntityManager.Player.Health = playerHealth;
            EntityManager.Player.Shields = playerShields;

            for(int i = 1; i < MissionManager.LoadedMissionsCount(); i++)
            {
                if (!keyValues.ContainsKey("Mission" + i))
                {
                    continue;
                }

                Mission mission = MissionManager.LoadMission(i);
                int tracker = int.Parse(keyValues["Mission" + i + "Tracker"]);
                mission.Tracker.TasksCompleted = tracker;
                mission.Tracker.Owner = EntityManager.Player;
                mission.Tracker.IsActive = true;
                EntityManager.Player.playerStatus.Missions.Add(mission);
            }

            reader.Close();
        }

    }
}
