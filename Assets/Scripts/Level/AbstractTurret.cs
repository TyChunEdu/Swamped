using UnityEngine;

namespace Level
{
    public abstract class AbstractTurret : MonoBehaviour
    {
        public GameObject projectilePrefab;
        public GameObject projectileSpawnPoint;
        public float shootInterval = 3f;

        protected abstract Vector2 GetDirection();

        private void Awake()
        {
            InvokeRepeating(nameof(ShootFireball), shootInterval, shootInterval);
        }

        protected void ShootFireball()
        {
            var turretAngle = Vector2.SignedAngle(projectilePrefab.transform.right, GetDirection());
            var projectile = Instantiate(projectilePrefab);
            projectile.transform.Rotate(projectile.transform.forward, turretAngle);
            projectile.GetComponent<TurretProjectile>().MoveTo(projectileSpawnPoint.transform.position);
        }
    }
}