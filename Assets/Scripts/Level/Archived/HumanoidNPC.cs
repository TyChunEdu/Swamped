using UnityEngine;

namespace Level
{
    public class HumanoidNPC : Actor
    {
        public float jumpDistanceThreshold = 1f;
        public float horizontalDetectionOffset = 0.1f;
        public float verticalDetectionOffset = 0.5f;
        public float shrunkTime = 1f;
    
        private float _currentMovement;
        private float _deltaTime;
        private bool _shrunk;
        private float _shrunkTimer;

        protected new void Update()
        {
            base.Update();
            
            // React to getting hit by player
            if (!_shrunk)
            {
                if (PlayerActor.Instance.isHitting && PlayerActor.Instance.weapon != null && rigidBody.IsTouching(PlayerActor.Instance.weapon))
                {
                    _shrunk = true;
                    _shrunkTimer = shrunkTime;
                    var newScale = transform.localScale;
                    newScale.y /= 2;
                    transform.localScale = newScale;
                }
            } else {
                _shrunkTimer -= Time.deltaTime;
                if (_shrunkTimer <= 0)
                {
                    _shrunk = false;
                    var newScale = transform.localScale;
                    newScale.y *= 2;
                    transform.localScale = newScale;
                }
            }
            
        }
    
        protected override float GetHorizontalMovement()
        {
            _deltaTime += Time.deltaTime;
            if (_deltaTime > 1)
            {
                _deltaTime = 0;
                _currentMovement = _currentMovement == 0 ? Utils.Utils.RandomChoice(new[] { -1, 1 }) : 0;
            }

            return _currentMovement;
        }

        protected bool IsFacingPlatform()
        {
            if (!IsMoving)
                return false;
        
            var castPosition = transform.position;
            var castHorizontalOffset = spriteRenderer.bounds.extents.x + horizontalDetectionOffset;
            castPosition.x += FacingRight ? castHorizontalOffset : -castHorizontalOffset;
            return Physics2D.BoxCast(
                castPosition,
                new Vector2(horizontalDetectionOffset, spriteRenderer.bounds.size.y - verticalDetectionOffset),
                0,
                FacingRight ? transform.right : -transform.right, 
                jumpDistanceThreshold);
        }

        protected override bool GetJumping()
        {
            return IsFacingPlatform();
        }

        protected override bool GetHitting()
        {
            return false;
        }
    }
}
