using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource wallSound;
    [SerializeField] private AudioSource racketSound;


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Player" || other.gameObject.name == "Enemy")
        {
            racketSound.Play();
        }
        else
        {
            wallSound.Play();
        }
    }
    
}
