using System;
using UnityEngine;

namespace Level
{
    public class PlayerActor : Actor
    {
        public static PlayerActor Instance { get; private set; }
        public AudioSource runAudioSource;
        public AudioSource jumpAudioSource;

        private bool goalReached = false;

        private void Awake()
        { 
            if (Instance != null && Instance != this) 
                Destroy(this); 
            else 
                Instance = this; 
        }
        protected new void Update()
        {
            base.Update();
            if (GetJumping())
            {
                jumpAudioSource.Play();
            }

            if (Math.Abs(GetHorizontalMovement()) >= 0.0f && IsMoving && !isAirborne && !goalReached)
            {
                runAudioSource.UnPause();
            }
            else
            {
                runAudioSource.Pause();
            }
        }
    
        protected override float GetHorizontalMovement()
        {
            return Input.GetAxis("Horizontal");
        }

        protected override bool GetJumping()
        {
            return Input.GetButtonDown("Jump");
        }

        protected override bool GetHitting()
        {
            return Input.GetButton("Fire1");
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.GetComponent<LevelGoal>())
            {
                goalReached = true;
            }
        }
    }
}
