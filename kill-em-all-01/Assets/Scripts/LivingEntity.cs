using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    [SerializeField] private float startingHealth;
    protected float Health;
    protected bool IsDead;

    // Event Declaration
    public event System.Action OnDeath;
    
    protected virtual void Start()
    {
        Health = startingHealth;
    }

    
    public void TakeHit(float damage, RaycastHit hit)
    {
        // TODO: Do some stuff here with hit variable.
        TakeDamage(damage);
    }

    
    public void TakeDamage(float damage)
    {
        Health -= damage;

        if (Health <= 0 && !IsDead)
        {
            Die();
        }
    }
    
    
    protected void Die()
    {
        IsDead = true;
        if (OnDeath != null)
        {
            OnDeath();
        }
        Destroy(gameObject);
    }
}
