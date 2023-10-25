using System.Collections.Generic;
using System.IO;
using Buffs;
using Items;
using Singletons;
using UnityEngine;
// ReSharper disable MemberCanBePrivate.Global

namespace SaveAndLoad
{
    public class Save
    {
        public int Day;
        public float Hunger;
        public float Thirst;
        public float Stamina;
        public Inventory.Inventory Inventory;
        public Buff ActiveBuff;
        public PseudoDictionary<Robot, float> RobotItemProductionMultiplier;

        private static readonly string FilePath = Application.persistentDataPath + "/savegame.json";

        public static void SaveGame()
        {
            var save = new Save
            {
                Day = GameTime.Instance.daysElapsed,
                Hunger = Player.Instance.HungerMeter.currentMeterAmount,
                Thirst = Player.Instance.ThirstMeter.currentMeterAmount,
                Stamina = Player.Instance.StaminaMeter.currentMeterAmount,
                Inventory = Player.Instance.inventory,
                ActiveBuff = Player.Instance.activeBuff,
                RobotItemProductionMultiplier = Player.Instance.robotItemProductionMultiplier
            };
            save.WriteToFile();
        }

        public static void LoadGame()
        {
            var save = LoadFromFile();
            GameTime.Instance.daysElapsed = save.Day;
            GameTime.Instance.minutesElapsed = 0;
            Player.Instance.HungerMeter.currentMeterAmount = save.Hunger;
            Player.Instance.ThirstMeter.currentMeterAmount = save.Thirst;
            Player.Instance.StaminaMeter.currentMeterAmount = save.Stamina;
            Player.Instance.inventory = save.Inventory;
            Player.Instance.activeBuff = save.ActiveBuff;
            Player.Instance.robotItemProductionMultiplier = save.RobotItemProductionMultiplier;
        }

        public static bool SaveFileExists()
        {
            return File.Exists(FilePath);
        }

        private void WriteToFile()
        {
            var saveData = JsonUtility.ToJson(this);
            File.WriteAllText(FilePath, saveData);
        }

        private static Save LoadFromFile()
        {
            var saveData = File.ReadAllText(FilePath);
            return JsonUtility.FromJson<Save>(saveData);
        }
    }
}