using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Level;
using UnityEngine;
using UnityEngine.Lumin;

namespace Level
{
    [RequireComponent(typeof(Collider2D))]
    public class Enemy : DeathZone
    {
        [SerializeField] private Transform leftEdge;
        [SerializeField] private Transform rightEdge;
        [SerializeField] private float speed;
        
        private Vector3 initScale;
        private bool movingLeft;

        private void Awake() 
        {
            initScale = transform.localScale;
        }

        private void Start()
        {
            leftEdge.parent = null;
            rightEdge.parent = null;
        }

        private void Update()
        {
            if (movingLeft)
            {
                if (transform.position.x >= leftEdge.position.x)
                {
                    MoveInDirection(-1);
                }
                else
                {
                    DirectionChange();
                }
            }
            else
            {
                if (transform.position.x <= rightEdge.position.x)
                {
                    MoveInDirection(1);
                }
                else
                {
                    DirectionChange();
                }
            }
        }

        private void DirectionChange()
        {
            movingLeft = !movingLeft;
        }

        private void MoveInDirection(int direction)
        {
            // Face direction
            transform.localScale = new Vector3(Math.Abs(initScale.x) * direction,
                initScale.y, initScale.z);
            
            // Move that direction
            transform.position = new Vector3(transform.position.x + Time.deltaTime * direction * speed,
                transform.position.y,
                transform.position.z);
        }

        protected new void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.GetComponent<Enemy>())
            {
                DirectionChange();
            }
            base.OnCollisionEnter2D(col);
        }
    }
}