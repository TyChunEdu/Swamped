using System;
using UnityEngine;

namespace Level
{
    [RequireComponent(typeof(Collider2D))]
    public class TurretProjectile : DeathZone
    {
        public GameObject spawnPoint;
        public float speed = 5f;

        private void Update()
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        private new void OnCollisionEnter2D(Collision2D col)
        {
            base.OnCollisionEnter2D(col);
            Destroy(gameObject);
        }

        public void MoveTo(Vector3 position)
        {
            transform.Translate(position - spawnPoint.transform.position, Space.World);
        }
    }
}