using System;
using UnityEngine;

namespace Level
{
    public class MovingPlatformHorizontal : MovingPlatform
    {
        private bool _movingRight;

        private void Update()
        {
            if (_movingRight && PlatformRenderer.bounds.max.x >= boundsRenderer.bounds.max.x)
                _movingRight = false;
            else if (!_movingRight && PlatformRenderer.bounds.min.x <= boundsRenderer.bounds.min.x) 
                _movingRight = true;
            transform.Translate((_movingRight ? Vector3.right : Vector3.left) * (Time.deltaTime * speed));
        }
    }
}