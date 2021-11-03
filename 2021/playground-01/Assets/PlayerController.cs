using System;
using UnityEngine;
using UnityEngine.UI;

namespace Playground
{
    public class PlayerController : MonoBehaviour
    {
        // DEBUG
        [SerializeField] private Text directionText;
        [SerializeField] private Text triggerName;
        
        // Player Settings
        [SerializeField] private float moveSpeed;
        private Vector3 _direction;


        private void Direction()
        {

            float inputX = Input.GetAxisRaw("Horizontal");
            float inpuyY = 0.0f;
            float inputZ = Input.GetAxisRaw("Vertical");

            _direction = new Vector3(inputX, inpuyY, inputZ).normalized;

        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log(other.gameObject.name);

            if (other)
            {
                triggerName.text = other.gameObject.name;
            }
        }


        private void Start()
        {
        
        }

        
        private void Update()
        {
            Direction(); directionText.text = _direction.ToString();

            transform.position += new Vector3(_direction.x, _direction.z, 0.0f) 
                                  * moveSpeed * Time.deltaTime;

        }

    }
}
