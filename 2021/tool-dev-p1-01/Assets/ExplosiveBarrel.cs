using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ExplosiveBarrel : MonoBehaviour
{
    [SerializeField] [Range(1f, 8f)] private float radius = 1;
    // [SerializeField] private float damage = 10;
    [SerializeField] private Color color = Color.red;

    private MaterialPropertyBlock _mpb;
    public MaterialPropertyBlock Mpb
    {
        get
        {
            if (_mpb  == null)
                _mpb = new MaterialPropertyBlock();
            return _mpb;
        }
    }

    private void ApplyColor()
    {
        
    }


    // Obje aktif oldugunda kendini listeye ekliyor.
    private void OnEnable() => 
        ExplosiveBarrelManager.AllBarrels.Add(this);
    
    // obje inaktif olduguunda kendini listeden cikartiyor.
    private void OnDisable() => 
        ExplosiveBarrelManager.AllBarrels.Remove(this);
    
    // Gizmolari herzaman ciziyor.
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }


    // Secili objelerin gizmolarini cizdiriyor.
    // otimizasyon icin uygun gizmolari cizdirmek viewport icin
    // yuk olabilir.
    private void OnDrawGizmosSelected()
    {
        // throw new NotImplementedException();
    }
}
