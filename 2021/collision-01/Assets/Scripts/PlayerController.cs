using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace CollisionProjectOne
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Camera sceneCamera;
        [SerializeField] private float moveSpeed;

        private Vector3 _velocity;
        private Rigidbody _rigidbody;
        private Vector3 _point;
        private GameObject _pointerSphere;
    
        // Raycasting from camera to invisible ground plane
        private void DrawRayFromCamera()
        {
            Ray ray = sceneCamera.ScreenPointToRay(Input.mousePosition);
            Plane virtualGroundPlane = new Plane(Vector3.up, Vector3.zero);
        
            float rayDistance; // Raycasting output variable.
            if (virtualGroundPlane.Raycast(ray, out rayDistance))
            {
                Vector3 point = ray.GetPoint(rayDistance);
                _point = point;
                Debug.DrawLine(ray.origin, point, Color.red);
            }

        }
        
        
        // Create Pointer Spehere
        private void CreatePointerSphere()
        {
            _pointerSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            _pointerSphere.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            Destroy(_pointerSphere.GetComponent<Collider>());
            _pointerSphere.GetComponent<Renderer>().material.color = Color.green;

        }


        // Move Pointer Shpere on camera raycast hit point
        private void TransformPointerSphere()
        {
            _pointerSphere.transform.position = _point;
        }

        
        // Player Velocity calculation
        private void Move()
        {
            float inputX = Input.GetAxisRaw("Horizontal");
            float inputY = 0.0f;
            float inputZ = Input.GetAxisRaw("Vertical");

            Vector3 moveInput = new Vector3(inputX, inputY, inputZ);
            _velocity = moveInput.normalized * moveSpeed;
            
            _rigidbody.velocity = _velocity * Time.fixedTime;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        
        private void Start()
        {
            CreatePointerSphere();
        }


        private void Update()
        {
            DrawRayFromCamera();
            TransformPointerSphere();

            transform.LookAt(new Vector3(_point.x, transform.position.y, _point.z));
        }


        private void FixedUpdate()
        {
            // Vector3 newPosition = _rigidbody.position + (_velocity * Time.fixedTime);
            // _rigidbody.MovePosition(newPosition);
            Move();
        }
    }
}
