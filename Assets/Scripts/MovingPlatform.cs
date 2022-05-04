using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] points;
    public float moveSpeed;
    public int currentPoint;

    public Transform platform;

    public bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        canMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        // If player pressed button
        if (canMove == true)
        {
            // Move platform to the current point
            platform.position = Vector3.MoveTowards(platform.position, points[currentPoint].position, moveSpeed * Time.deltaTime);
        }
    }

    public void ElevatorMove()
    {
        // Allow elevator to move
        if (canMove == false)
        {
            canMove = true;
        }

        // If platform is close to one point
        if (Vector3.Distance(platform.position, points[currentPoint].position) < 0.5f)
        {
            // Increase the current point
            currentPoint++;

            // if the current point is the end of the list, reset it to 0
            if (currentPoint >= points.Length)
            {
                currentPoint = 0;
            }
        }
    }
}
