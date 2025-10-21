using System;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;

    [SerializeField] private Slider HealthBar;

    private int health;

    public bool IsDead => health == 0;

    private void Start()
    {
        health = maxHealth;
    }

    public event Action OnTakeDamage;
    public event Action OnDie;

    public void DealDamage(int damage)
    {
        if (health == 0) return;

        health = Mathf.Max(health - damage, 0);

        OnTakeDamage?.Invoke();
        PlaySound();
        if (health == 0)
        {
            health = 0;
            OnDie?.Invoke();
            HealthBar.gameObject.SetActive(false);
        }
    }

    private void PlaySound()
    {
        GetComponent<AudioSource>().Play();
    }
}