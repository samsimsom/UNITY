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


    private void Awake()
    {
        // Editor tarafinda yapilan degisikliklerin pekcogunun
        // oyun icini etkilememesi gerekir.
        // Malzeme degistirme vb. gibi durumlar ile karsilasildiginda 
        // en guzel cozumlerden biri. Yeni malzemeyi burda yaratmak
        // ve HideFlags kullanarak kaydedilmesini engellemek.

        Shader shader = Shader.Find("Default/Diffuse");
        Material mat = new Material(shader);
        mat.hideFlags = HideFlags.HideAndDontSave;

        GetComponent<Transform>().hideFlags = HideFlags.HideInInspector;
        
        // Bu sekilde material erisimi yaptiginda,
        // unity malarialin bir kopyasini olusturu ve onu editleyip objeye atar.
        // orjinal material degisltirilmez.
        // will duplicate the material (dusuk performans)
        // GetComponent<MeshRenderer>().material.color = color;

        // Orjinal material assetini degistirir.
        // will modify the asset.
        GetComponent<MeshRenderer>().sharedMaterial.color = color;
        
        // bu duruma benzer bir konu MeshFilter icindir.
        // GetComponent<MeshFilter>().sharedMesh
        // GetComponent<MeshFilter>().mesh
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
