using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform _muzzle;
    [SerializeField] private Projectile _projectile;
    [SerializeField] private float _msBetweenShots = 100.0f;
    [SerializeField] private float _muzzleVelocity = 35.0f;

    private float nextShotTime;
    
    public void Shoot()
    {
        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + _msBetweenShots / 1000.0f;
            Projectile newProjectile = Instantiate(_projectile, _muzzle.position, 
                _muzzle.rotation) as Projectile;
            newProjectile.SetSpeed(_muzzleVelocity);
        }
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
