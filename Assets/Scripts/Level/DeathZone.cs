using System;
using Missions;
using Singletons;
using UnityEngine;

namespace Level
{
    
    public class DeathZone : MonoBehaviour
    {
        protected void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject == PlayerActor.Instance.gameObject)
            {
                PlayerActor.Instance.GetComponent<Animator>().SetBool("Dead", true);
                if (LevelPrompts.Instance == null)
                {
                    Time.timeScale = 0;
                    Debug.Log("Level Lost");
                }
                else
                {
                    LevelPrompts.Instance.missionMenu.RenderMissionOutcome(Player.Instance.currentMission.badOutcome);
                }
            } 
        }
    }
}