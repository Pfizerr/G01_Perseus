using G01_Perseus.Missions.Trackers;
using G01_Perseus.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace G01_Perseus
{
    public static class MissionManager
    {
        private static List<Mission> loadedMissions = new List<Mission>();
        private static string[] missionData;
        private static string[] trackerData;
        public static GraphicsDevice device
        {
            get;
            set;
        }

        public static int GetRandomMissionId() => Game1.rand.Next(1, (loadedMissions.Count() * 1000) / 1000);

        public static int LoadedMissionsCount()
        {
            return loadedMissions.Count;
        }

        public static string LoadMissionDataFromId(int id)
        {
            foreach(string line in trackerData)
            {
                if(line.StartsWith("id=" + id))
                {
                    return line;
                }
            }
            return null;
        }

        public static MissionTracker LoadMissionTracker(int id)
        {
            string line = LoadMissionDataFromId(id);
            string trackerTypeStr = "";
            int tasksToComplete = 0;
            Point sector = new Point();
            List<string> optionalParams = new List<string>();

            string[] data = line.Split(',');

            foreach(string field in data)
            {
                string fieldName = field.Split('=')[0];
                string fieldValue = field.Split('=')[1];
                
                if(fieldName == "id")
                {
                    continue;
                }
                else if (fieldName == "tracker_type")
                {
                    trackerTypeStr = fieldValue;
                }
                else if (fieldName == "tracker_tasks")
                {
                    tasksToComplete = Convert.ToInt32(fieldValue);
                }
                else // optional/specific parameters
                {
                    optionalParams.Add(field);
                }
                if (fieldName == "sector")
                {
                    int x = Convert.ToInt32(fieldValue.Split('.')[0]);
                    int y = Convert.ToInt32(fieldValue.Split('.')[1]);
                    sector = new Point(x, y);
                }

            }

            // UPDATE THESE AS NEW TRACKERS ARE ADDED
            switch (trackerTypeStr)
            {
                case "KILLS_TRACKER":
                    Type typeOfSubject = null;
                    for (int i = 0; i < optionalParams.Count; i++)
                    {
                        string fieldName = optionalParams[i].Split('=')[0];
                        string fieldValue = optionalParams[i].Split('=')[1];
                    
                        if(fieldName == "type_of_subject")
                        {
                            typeOfSubject = Type.GetType(fieldValue);
                        }
                    }
                    return new KillsMissionTracker(typeOfSubject, sector, tasksToComplete);
                case "TRAVEL_TRACKER":
                    for (int i = 0; i < optionalParams.Count; i++)
                    {
                        string fieldName = optionalParams[i].Split('=')[0];
                        string fieldValue = optionalParams[i].Split('=')[1];
                    }
                    return new TravelMissionTracker(sector, tasksToComplete);
                case "SURVIVE_TRACKER":
                    int timeToSurvive = 0;
                    for (int i = 0; i < optionalParams.Count; i++)
                    {
                        string fieldName = optionalParams[i].Split('=')[0];
                        string fieldValue = optionalParams[i].Split('=')[1];

                        if(fieldName == "timeToSurvive")
                        {
                            timeToSurvive = Convert.ToInt32(fieldValue);
                        }
                    }
                    return new SurviveMissionTracker(sector, timeToSurvive, tasksToComplete);
            }

            return null;
        }

        public static Mission LoadMission(int id)
        {
            if(missionData == null || trackerData == null)
            {
                LoadData();
            }

            foreach (string line in missionData)
            {
                if (line.StartsWith("id=" + id))
                {
                    Mission mission = new Mission();
                    mission.Id = id;

                    string[] data = line.Split(',');

                    foreach(string field in data)
                    {
                        if (field.StartsWith("name="))
                        {
                            mission.Name = field.Split('=')[1];
                        }
                        else if (field.StartsWith("description="))
                        {
                            mission.Description = field.Split('=')[1];
                        }
                        else if (field.StartsWith("tracker_id="))
                        {
                            mission.Tracker = LoadMissionTracker(Convert.ToInt32(field.Split('=')[1]));
                        }
                        else if (field.StartsWith("rewards_resources="))
                        {
                            mission.Currency = Convert.ToInt32(field.Split('=')[1]);
                        }
                        else if (field.StartsWith("rewards_dust="))
                        {
                            mission.Dust = Convert.ToInt32(field.Split('=')[1]);
                        }
                        else if (field.StartsWith("rewards_skillpoints="))
                        {
                            mission.Experience = Convert.ToInt32(field.Split('=')[1]);
                        }
                        else if (field.StartsWith("rewards_equipment"))
                        {
                            //mission.Rewards.Equipment = Equipment.StringToEquipment(field.Split('=')[1]);
                        }
                    }
                    return mission;
                }
            }

            return null;
        }

        public static void LoadMissions()
        {
            loadedMissions.Add(LoadMission(1));
            loadedMissions.Add(LoadMission(2));
            loadedMissions.Add(LoadMission(3));
            loadedMissions.Add(LoadMission(4));
            loadedMissions.Add(LoadMission(5));
        }

        public static void LoadData()
        {
            missionData = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\..\..\..\..\Content\data\MissionData.txt");
            trackerData = File.ReadAllLines(Directory.GetCurrentDirectory() + @"\..\..\..\..\Content\data\TrackerData.txt");
        }
    }
}