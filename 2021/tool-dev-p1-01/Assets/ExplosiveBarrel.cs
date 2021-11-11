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

    private static readonly int SHPropColor = Shader.PropertyToID("_Color");
    
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
        MeshRenderer rnd = GetComponent<MeshRenderer>();
        Mpb.SetColor(SHPropColor, color);
        rnd.SetPropertyBlock(Mpb);
    }

    
    // Degisen deger oldugunda calisan muthis method.
    // Editor-only function that Unity calls when the script is
    // loaded or a value changes in the Inspector.
    private void OnValidate()
    {
        ApplyColor();
    }


    // Obje aktif oldugunda kendini listeye ekliyor.
    private void OnEnable()
    {
        ApplyColor();
        ExplosiveBarrelManager.AllBarrels.Add(this);
    }

    
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
