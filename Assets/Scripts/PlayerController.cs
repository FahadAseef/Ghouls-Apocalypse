using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] float moveSpeed = 5f;
    private float jumpForce = 4f;
    private float gravity = 12f;
    private float verticalVelocity;
    private float speed = 7f;
    private int desiredLine = 1; // 0 = left , 1 = midddle , 2 = right 
    private const float LANE_DISTANCE = 3f; 


    private void Start()
    {
        controller= GetComponent<CharacterController>();
    }

    private void Update()
    {
        // gather the inputs on which lane we should be
        if ((Input.GetKeyDown(KeyCode.A)) || Input.GetKeyDown(KeyCode.LeftArrow)) 
        {
            moveLane(true);
        }
        if ((Input.GetKeyDown(KeyCode.D)) || Input.GetKeyDown(KeyCode.RightArrow)) 
        {
            moveLane(false);
        }

        //calculate where we should be in the future
        Vector3 targetPosition = transform.position.z * Vector3.forward;
        if (desiredLine == 0)
        {
            transform.position += Vector3.left * LANE_DISTANCE;
        }
        else if(desiredLine == 2)
        {
            transform.position += Vector3.right * LANE_DISTANCE;
        }

        //lets calculate our move delta
        Vector3 moveVector = Vector3.zero;
        moveVector.x = (targetPosition - transform.position).normalized.x * speed;
        moveVector.y = -0.1f;
        moveVector.z = speed;

        //move the character
        controller.Move(moveVector * Time.deltaTime);

    }

    private void moveLane(bool goingRight)
    {
        //if (!goingRight)
        //{
        //    desiredLine--;
        //    if (desiredLine == -1)
        //    {
        //        desiredLine = 0;
        //    }
        //}
        //else
        //{
        //    desiredLine++;
        //    if (desiredLine == 3)
        //    {
        //        desiredLine = 2;
        //    }
        //}

        desiredLine += goingRight ? 1 : -1;
        desiredLine = Mathf.Clamp(desiredLine,0,2);
        
    }
}
