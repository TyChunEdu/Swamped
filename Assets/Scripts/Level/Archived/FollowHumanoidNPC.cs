using System;
using UnityEngine;

namespace Level
{
    public class FollowHumanoidNPC : HumanoidNPC
    {
        public float distanceToStopFollowing = 3f;
        public float distanceToStartFollowing = 5f;
        
        private GameObject _target;
        private bool _movingTowardsTarget;

        private void Start()
        {
            _target = PlayerActor.Instance.gameObject;
        }

        protected override float GetHorizontalMovement()
        {
            var diffVector = _target.transform.position - transform.position;
            var horizontalDistance = Math.Abs(diffVector.x);
            
            if (_movingTowardsTarget && horizontalDistance < distanceToStopFollowing)
                _movingTowardsTarget = false;
            else if (!_movingTowardsTarget && horizontalDistance > distanceToStartFollowing)
                _movingTowardsTarget = true;

            return _movingTowardsTarget ? diffVector.x / horizontalDistance : 0;
        }

        protected override bool GetJumping()
        {
            return IsFacingPlatform();
        }
    }
}
