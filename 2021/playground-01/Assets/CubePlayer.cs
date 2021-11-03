using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Playground
{
    public class CubePlayer : MonoBehaviour
    {
        [SerializeField] private Text collisionText;


        private void OnTriggerEnter(Collider other)
        {
            collisionText.text = other.gameObject.name;
            Debug.Log("Enter");
        }


        private void OnTriggerExit(Collider other)
        {
            Debug.Log("Exit");
        }

        
        private void OnTriggerStay(Collider other)
        {
            Debug.Log("Stay");
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
}
