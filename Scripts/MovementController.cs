using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float horizontalSpeed;
    private float speedX;

  [SerializeField] private Rigidbody2D rigidbody2D;
  public  void LeftButton()
    {
        speedX = -horizontalSpeed;
    }

  public  void RightButton()
    {
        speedX = horizontalSpeed;
    }
  public   void Stop()
    {
        speedX = 0;
    }

    private void FixedUpdate()
    {
        transform.Translate(speedX, 0, 0);
    }

    
}
