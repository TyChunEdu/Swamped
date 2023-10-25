using System;
using UnityEngine;
using Singletons;
using System.Collections.Generic;
using Missions;
using Random = System.Random;

namespace Buffs
{
    [CreateAssetMenu(menuName = "Buff")]
    public class Buff : ScriptableObject
    {
        public static Buff[] AllBuffs;
        
        public int[] activeDay;
        public BuffEffects effects;
        public string buffName;
        public string description;
        public Mission mission;

        public bool IsPossibleToday()
        {
            int currentDay = GameTime.Instance.Day();
            return Array.Exists(this.activeDay, element => element == currentDay);
        }

        public void SetUpBuff()
        {
            if (effects.isRampaging)
            {
                effects.SetRampaging();
                if (!effects.hasRampaged)
                {
                    effects.hasRampaged = true;
                }
            }
            else
            {
                effects.SetNotRampaging();
            }
        }
        
        public static BuffEffects ActiveEffects()
        {
            return Player.Instance.activeBuff.effects;
        }

        public static Buff GetRandomPossibleBuff()
        {
            var randomBuff = Utils.Utils.RandomChoice(GetAllPossibleBuffsToday().ToArray());
            randomBuff.SetUpBuff();
            return randomBuff;
        }

        public static List<Buff> GetAllPossibleBuffsToday() {
            List<Buff> possibleBuffs = new List<Buff>();
            foreach (var buff in AllBuffs)
            {
                if (buff.IsPossibleToday()) {
                    possibleBuffs.Add(buff);
                }
            }
            
            return possibleBuffs;
        }

        public static string GetActiveBuffsToString()
        {
            Buff buff = Player.Instance.activeBuff;
            string s = "";
            s += buff.buffName + ": " + buff.description + "\n";

                if (s.Equals(""))
            {
                s = "You're ready to go! No buffs active.";
            }

            return s;
        }
    }
}