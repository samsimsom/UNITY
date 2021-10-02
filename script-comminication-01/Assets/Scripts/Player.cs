using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float playerHealth;
    public float speed = 10;

    private Health healtUi;

    // Start is called before the first frame update
    void Start()
    {
        // Health Objesini obje tiplerine gore ariyor
        // ve degiskene atiyor (Bu Classi ben yarattim)
        healtUi = FindObjectOfType<Health>();
        playerHealth = 90;
    }

    private void Movement()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = 0.0f;
        float inputZ = Input.GetAxisRaw("Vertical");

        Vector3 input = new Vector3(inputX, inputY, inputZ);
        Vector3 direction = input.normalized;
        Vector3 velocity = direction * speed;
        Vector3 moveAmount = velocity * Time.fixedDeltaTime;
        
        transform.Translate(moveAmount);
    }


    private void Action()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            playerHealth -= 20;
            playerHealth = Mathf.Clamp(playerHealth, 0.0f, 100.0f);
            
            if (playerHealth <= 0.0)
            {
                healtUi.GameOver();
                Destroy(gameObject);
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        Action();
    }

    private void FixedUpdate()
    {
        Movement();
    }
}
