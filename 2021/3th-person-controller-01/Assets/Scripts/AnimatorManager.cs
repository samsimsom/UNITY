using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    private Animator _animator;
    private int _horizontal;
    private int _vertical;
    
    public void UpdateAnimatorValues(float horizontalMovement, 
        float verticalMovement, bool isSprinting)
    {
        // Animation Snapping
        float snappedHorizontal;
        float snappedVertical;

        #region Snapped Horizontal Logic
        if (horizontalMovement > 0f && horizontalMovement < 0.55f)
        {
            snappedHorizontal = 0.5f;
        }
        else if (horizontalMovement > 0.55f)
        {
            snappedHorizontal = 1f;
        }
        else if (horizontalMovement < 0f && horizontalMovement > -0.55f)
        {
            snappedHorizontal = -0.5f;
        }
        else if (horizontalMovement < -0.55f)
        {
            snappedHorizontal = -1f;
        }
        else
        {
            snappedHorizontal = 0f;
        }
        
        #endregion

        #region Snapped Vertical Logic
        if (verticalMovement > 0f && verticalMovement < 0.55f)
        {
            snappedVertical = 0.5f;
        }
        else if (verticalMovement > 0.55f)
        {
            snappedVertical = 1f;
        }
        else if (verticalMovement < 0f && verticalMovement > -0.55f)
        {
            snappedVertical = -0.5f;
        }
        else if (verticalMovement < -0.55f)
        {
            snappedVertical = -1f;
        }
        else
        {
            snappedVertical = 0f;
        }    
        
        #endregion


        if (isSprinting)
        {
            snappedHorizontal = horizontalMovement;
            snappedVertical = 2;
        }
        
        _animator.SetFloat(_horizontal, snappedHorizontal, 
            0.1f, Time.deltaTime);
        _animator.SetFloat(_vertical, snappedVertical, 
            0.1f, Time.deltaTime);
    }


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _horizontal = Animator.StringToHash("Horizontal");
        _vertical = Animator.StringToHash("Vertical");
    }
}
