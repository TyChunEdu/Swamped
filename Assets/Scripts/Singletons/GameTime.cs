using System;
using System.Collections.Generic;
using Buffs;
using DailyUpdates.Components;
using InGameMenu;
using SaveAndLoad;
using UnityEngine;

namespace Singletons
{
    /// <summary>
    /// In-game time is represented by a day, hour, and minute.
    /// It is essentially stored as two values: the number of days completed, and the number of minutes since the start
    /// of the day.
    /// 
    /// Singleton pattern inspired by https://gamedevbeginner.com/singletons-in-unity-the-right-way/
    /// </summary>
    public class GameTime : MonoBehaviour
    {
        private const float RealSecondsPerGameMinute = 1f;
        private const int MorningHour = 7;
        public static GameTime Instance { get; private set; }

        public int daysElapsed; // days completed
        public int minutesElapsed; // minutes elapsed since start of day
        public bool paused;

        private float _timeChange;
        private readonly List<(Action Callback, int ScheduledTime)> _timers = new();
        private readonly List<(Action Callback, int NextScheduledTime, int Interval, bool pauseOvernight)> _repeatedTimers = new();
        private readonly List<Action> _dailyTimers = new();

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(gameObject);
            else
                Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (paused) return;
            if (_timeChange > RealSecondsPerGameMinute)
            {
                _timeChange = 0;
                SkipMinutes(1);
            }
            _timeChange += Time.deltaTime;
        }

        public void Pause()
        {
            paused = true;
        }

        public void Unpause()
        {
            paused = false;
        }

        public void AddTimer(int minutes, Action callback)
        {
            _timers.Add((callback, minutesElapsed + minutes));
        }
        
        public void AddRepeatedTimer(int minutes, Action callback, bool pauseOvernight = false)
        {
            _repeatedTimers.Add((callback, minutesElapsed + minutes, minutes, pauseOvernight));
        }

        public void SkipMinutes(int minutes, bool overnight = false)
        {
            // elapse time
            minutesElapsed += minutes;

            // update timed events
            for (var i = _timers.Count - 1; i >= 0; i--)
            {
                var timer = _timers[i];
                if (minutesElapsed >= timer.ScheduledTime)
                {
                    timer.Callback();
                    _timers.RemoveAt(i);
                }
            }
            
            // updated repeated timed events
            for (var i = _repeatedTimers.Count - 1; i >= 0; i--)
            {
                var timer = _repeatedTimers[i];
                if (overnight && timer.pauseOvernight)
                {
                    _repeatedTimers[i] = (timer.Callback, 0, timer.Interval, true);
                    continue;
                }
                var nextScheduledTime = timer.NextScheduledTime;
                while (nextScheduledTime <= minutesElapsed)
                {
                    timer.Callback();
                    nextScheduledTime += timer.Interval;
                }

                if (overnight)
                    nextScheduledTime = 0;

                _repeatedTimers[i] = (timer.Callback, nextScheduledTime, timer.Interval, timer.pauseOvernight);
            }
        }

        public void SkipHours(float hours, bool overnight = false)
        {
            SkipMinutes(HoursToMinutes(hours), overnight);
        }

        public string TimeString()
        {
            return $"Day {Day()}";
        }

        public int Day()
        {
            return daysElapsed + 1;
        }

        public int HoursElapsed()
        {
            return minutesElapsed / HoursToMinutes(1);
        }
        
        public int Hour()
        {
            return HoursElapsed() + MorningHour;
        }

        public int Minute()
        {
            return minutesElapsed - HoursToMinutes(HoursElapsed());
        }

        public void NextDay()
        {
            // Do end-of-day things
            
            // Add resources collected by robots to inventory
            foreach (var (robot, quantity) in Player.Instance.inventory.GetRobotsInInventory().AsDictionary())
                for (var i = 0; i < quantity; i++)
                    robot.AddDailyResourcesToInventory();
            
            // Skip to next day
            SkipHours(8, true);
            minutesElapsed = 0;
            daysElapsed++;
            Player.Instance.numOfMissionsCompletedToday = 0;
            
            // Lose Condition. You took more than 10 days
            if (daysElapsed == 11)
            {
                InGameMenuManager.Instance.LoseGame("You Took Too Much Time!");
            }
            
            if (!InGameMenuManager.Instance.IsGameOver())
            {
                // Choose buff for today
                Player.Instance.activeBuff = Buff.GetRandomPossibleBuff();
                
                UpdateDayTracker.Instance.DisplayDailyUpdates();
            
                // Save game
                Save.SaveGame();
            }
        }

        public static int HoursToMinutes(float hours)
        {
            return (int)Math.Round(hours * 60);
        }
    }
}