using System;
using System.Collections;
using UnityEngine;

namespace Combat
{
    public class VisualManager : MonoBehaviour
    {
        
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Health health;

        private void Awake()
        {
            health.OnTakeDamage += FlashDamage;
            health.OnDie += () => spriteRenderer.sortingOrder = 0;
        }
        
        public void FlashDamage()
        {
            //if(health.IsDead) { return; }
            StartCoroutine(FlashRedOverTime(0.1f));
        }

        public IEnumerator FlashRedOverTime(float duration)
        {
            Color originalColor = spriteRenderer.color;
            Color flashColor = Color.red;

            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                float t = Mathf.PingPong(elapsedTime, duration / 2) / (duration / 2);
                spriteRenderer.color = Color.Lerp(originalColor, flashColor, t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            spriteRenderer.color = originalColor; // Reset to original color
        }

        private void OnDestroy()
        {
            health.OnTakeDamage -= FlashDamage;
        }
    }
}