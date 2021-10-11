using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    [SerializeField] private float startingHealth;
    protected float health;
    protected bool isDead;

    public event System.Action OnDeath;
    
    protected virtual void Start()
    {
        health = startingHealth;
    }

    
    public void TakeHit(float damage, RaycastHit hit)
    {
        health -= damage;

        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    protected void Die()
    {
        isDead = true;
        if (OnDeath != null)
        {
            OnDeath();
        }
        GameObject.Destroy(gameObject);
    }
}
