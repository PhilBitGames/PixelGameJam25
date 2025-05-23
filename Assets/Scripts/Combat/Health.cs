using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100;
    
    [SerializeField]
    private Slider HealthBar;

    private int health;
    
    private bool isInvulnerable = false;

    public event Action OnTakeDamage;
    public event Action OnDie;
    //private bool isDead;

    private void Start()
    {
        health = maxHealth;
        //isDead = false;
    }
    
    public bool IsDead => health == 0;

    public void SetIsInvulnerable(bool isInvulnerable)
    {
        this.isInvulnerable = isInvulnerable;
    }

    public void DealDamage(int damage)
    {
        if(health == 0) { return; }
        if(isInvulnerable) { return; }

        health = Mathf.Max(health - damage, 0);
        
        OnTakeDamage?.Invoke();
        PlaySound();
        if(health == 0)
        {
            health = 0;
            //isDead = true;
            OnDie?.Invoke();
            HealthBar.gameObject.SetActive(false);
            return;
        }
        
        //HealthBar.value = (float)health / maxHealth;

    }

    public void PlaySound()
    {
        GetComponent<AudioSource>().Play();
    }
}
