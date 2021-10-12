using System;
using UnityEngine;
using System.Collections;
using  System.Collections.Generic;

public class SomeClass : MonoBehaviour
{
    private void Start()
    {
        List<BadGuy> badGuys = new List<BadGuy>();
        
        badGuys.Add(new BadGuy("Hasan", 50));
        badGuys.Add(new BadGuy("Kamil", 23));
        badGuys.Add(new BadGuy("Tarik", 45));
        
        badGuys.Sort();
        foreach (BadGuy badGuy in badGuys)
        {
            Debug.Log($"{badGuy.name} -> {badGuy.power}");
        }
        
        badGuys.Clear();
    }
}
