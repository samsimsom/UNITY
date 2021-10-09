using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;

    private Camera _viewCamera;
    private PlayerController _controller;


    private Vector3 CalculateVelocity()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = 0;
        float inputZ = Input.GetAxisRaw("Vertical");

        Vector3 moveInput = new Vector3(inputX, inputY, inputZ);
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;

        return moveVelocity;
    }


    private void DrawRayFromCamera()
    {
        Ray ray = _viewCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            Debug.DrawLine(ray.origin, point, Color.red);
            _controller.LookAt(point);
        }
    }

    void Start()
    {
        _controller = GetComponent<PlayerController>();
        _viewCamera = Camera.main;
    }


    void Update()
    {
        _controller.Move(CalculateVelocity());
        DrawRayFromCamera();
    }
}