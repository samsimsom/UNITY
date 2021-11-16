using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class ExplosiveBarrel : MonoBehaviour
{
    public BarrelType type;
    
    private static readonly int SHPropColor = Shader.PropertyToID("_Color");
    
    // Property
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

    
    // Gizmolari herzaman ciziyor.
    private void OnDrawGizmos()
    {
        // Barrel Type baslangicta null olcagi icin bu kontrolu yapiyorum.
        if (type == null) 
        {
            return;
        }
        
        // Gizmos.DrawWireSphere(transform.position, radius);
        Handles.color = type.color;
        Handles.DrawWireDisc(transform.position, Vector3.up, 
            type.radius);
        Handles.color = Color.white;
    }
    
    
    #region OnDrawGizmosSelected
    // Secili objelerin gizmolarini cizdiriyor.
    // otimizasyon icin uygun gizmolari cizdirmek viewport icin
    // yuk olabilir.
    private void OnDrawGizmosSelected()
    {

    }

    #endregion
    
    
    private void TryApplyColor()
    {
        if (type == null)
        {
            return;
        }
        
        MeshRenderer rnd = GetComponent<MeshRenderer>();
        Mpb.SetColor(SHPropColor, type.color);
        rnd.SetPropertyBlock(Mpb);
    }

    
    // Degisen deger oldugunda calisan muthis method.
    // Editor-only function that Unity calls when the script is
    // loaded or a value changes in the Inspector.
    private void OnValidate() => TryApplyColor();
    
    
    // Game Object Check Box is Enable
    private void OnEnable()
    {
        TryApplyColor();
        
        // Obje aktif oldugunda kendini listeye ekliyor.
        ExplosiveBarrelManager.AllBarrels.Add(this);
    }

    
    // Game Object Check Box is Disable
    private void OnDisable()
    {
        // obje inaktif olduguunda kendini listeden cikartiyor.
        ExplosiveBarrelManager.AllBarrels.Remove(this);
    }
    
    

}
