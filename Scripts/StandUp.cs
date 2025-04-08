using UnityEngine;

namespace TowerDefense
{
    public class StandUp : MonoBehaviour
    {
        private Rigidbody2D rb;
        private SpriteRenderer sr;
        private Enemy e;
        private bool flipX;

        private void Start()
        {
            rb = transform.root.GetComponent<Rigidbody2D>();
            sr = GetComponent<SpriteRenderer>();
        }

        public void Use(EnemyAsset asset)
        {
            flipX = asset.flipX;
        }

        private void LateUpdate()
        {
            // Objekt bleibt aufrecht
            transform.up = Vector2.up;

            float xMotion = rb.velocity.x;
            
            // Bewegt sich deutlich nach rechts?
            if (xMotion > 0.03f)
            {
                // Dann nutze die „Basis-Ausrichtung“ aus dem Asset
                sr.flipX = flipX;
            }
            // Alles andere (links, 0, minimal rechts) => nimm das Gegenteil
            else if (xMotion < 0.03f)
            {
                sr.flipX = !flipX;
            }
        }
    }
}