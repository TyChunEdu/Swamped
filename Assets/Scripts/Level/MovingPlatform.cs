using UnityEngine;

namespace Level
{
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class MovingPlatform : MonoBehaviour
    {
        public SpriteRenderer boundsRenderer;
        public float speed = 5f;
        
        protected SpriteRenderer PlatformRenderer;

        private void Start()
        {
            PlatformRenderer = GetComponent<SpriteRenderer>();
        }
        
        void OnCollisionEnter2D(Collision2D col)
        {
            col.gameObject.transform.SetParent(gameObject.transform);
        }
        
        void OnCollisionExit2D(Collision2D col)
        {
            col.gameObject.transform.parent = null;
        }
    }
}