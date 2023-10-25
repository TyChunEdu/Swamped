using System;
using UnityEngine;

namespace Level
{
    public class AimingTurret : AbstractTurret
    {
        private GameObject _player;

        private void Start()
        {
            _player = PlayerActor.Instance.gameObject;
        }

        private void Update()
        {
            var turretTransform = transform;
            var turretToPlayer = _player.transform.position - turretTransform.position;
            var angleBetweenTurretAndPlayer = Vector2.SignedAngle(GetDirection(), turretToPlayer);
            transform.Rotate(transform.forward, angleBetweenTurretAndPlayer);
        }

        protected override Vector2 GetDirection()
        {
            return -transform.right;
        }
    }
}