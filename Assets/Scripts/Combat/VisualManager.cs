using System.Collections;
using UnityEngine;

namespace Combat
{
    public class VisualManager : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Health health;
        private readonly Color flashColor = Color.red;


        private void Awake()
        {
            health.OnTakeDamage += FlashDamage;
            health.OnDie += () => spriteRenderer.sortingOrder = 0;
        }

        private void OnDestroy()
        {
            health.OnTakeDamage -= FlashDamage;
        }

        private void FlashDamage()
        {
            StartCoroutine(FlashRedOverTime(0.1f));
        }

        private IEnumerator FlashRedOverTime(float duration)
        {
            var elapsedTime = 0f;
            var originalColor = spriteRenderer.color;

            while (elapsedTime < duration)
            {
                var t = Mathf.PingPong(elapsedTime, duration / 2) / (duration / 2);
                spriteRenderer.color = Color.Lerp(originalColor, flashColor, t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            spriteRenderer.color = originalColor; // Reset to original color
        }
    }
}