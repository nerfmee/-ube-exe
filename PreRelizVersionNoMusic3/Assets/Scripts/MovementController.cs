using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float horizontalSpeed;
    private float speedX = 0.3f;
   [SerializeField] private Vector2 _direction;
   

    [SerializeField] private Rigidbody2D rigidbody2D;
  public  void LeftButton()
    {
        horizontalSpeed = -1.25f;
        _direction.x = -1f;

    }

  public  void RightButton()
    {
        horizontalSpeed = 1.25f;
        _direction.x = 1f;

    }
  public   void Stop()
    {
        _direction.x = 0f;
        horizontalSpeed = 0f;
    }

    private void FixedUpdate()
    { 
        rigidbody2D.AddForce(_direction.normalized * speedX, ForceMode2D.Impulse);
    }

    
}
