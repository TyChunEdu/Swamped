using UnityEngine;

namespace Level
{
    public class MovingPlatformVertical : MovingPlatform
    {
        private bool _movingUp;

        private void Update()
        {
            if (_movingUp && PlatformRenderer.bounds.max.y >= boundsRenderer.bounds.max.y)
                _movingUp = false;
            else if (!_movingUp && PlatformRenderer.bounds.min.y <= boundsRenderer.bounds.min.y) 
                _movingUp = true;
            transform.Translate((_movingUp ? Vector3.up : Vector3.down) * (Time.deltaTime * speed));
        }
    }
}