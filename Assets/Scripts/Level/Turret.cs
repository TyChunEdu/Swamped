using System;
using UnityEngine;

namespace Level
{
    public class Turret : AbstractTurret
    {
        public Vector2 direction;

        protected override Vector2 GetDirection()
        {
            return direction;
        }
    }
}