using System;
using UnityEngine;

namespace Projectiles
{
    public abstract class BaseProjectile : MonoBehaviour
    {
        public float speed = 1;

        private void Update()
        {
            transform.Translate(transform.up * speed * Time.deltaTime);
        }

        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
    }
}