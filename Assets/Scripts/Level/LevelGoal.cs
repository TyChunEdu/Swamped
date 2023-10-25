using System;
using Singletons;
using UnityEngine;

namespace Level
{
    public class LevelGoal : MonoBehaviour
    {
        private float _time;
        
        void Start()
        {
            Time.timeScale = 1;
        }

        void Update()
        {
            _time += Time.deltaTime;
        }
        
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject == PlayerActor.Instance.gameObject)
            {
                if (LevelPrompts.Instance == null)
                {
                    Time.timeScale = 0;
                    Debug.Log("Level Won");
                }
                else
                { // TODO: rewards should be based on performance in level
                    if (_time < Player.Instance.currentMission.timeLimit)
                    {
                        LevelPrompts.Instance.missionMenu.RenderMissionOutcome(
                            Player.Instance.currentMission.greatOutcome
                        );  
                    }
                    else
                    {
                        LevelPrompts.Instance.missionMenu.RenderMissionOutcome(
                            Player.Instance.currentMission.neutralOutcome
                        );  
                    }
                    
                }
            }
        }
    }
}