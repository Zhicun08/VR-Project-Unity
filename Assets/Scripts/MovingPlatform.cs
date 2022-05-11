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

    public GameObject elevatorSign;

    public GameObject collectVFX;

    public bool isWaiting;
    public float waitTimer;
    public float maxWaitTime;

    // Start is called before the first frame update
    void Start()
    {
        canMove = false;
        waitTimer = 0;
        maxWaitTime = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isWaiting)
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0)
            { 
                isWaiting = false; 
            }
        }

        if (!isWaiting)
        {
            MovePlatform();
        }
    }

    private void MovePlatform()
    {
        // If player pressed button
        if (canMove == true && DetectPlayer.instance.playerDetect == true)
        {
            // Move platform to the current point
            platform.position = Vector3.MoveTowards(platform.position, points[currentPoint].position, moveSpeed * Time.deltaTime);

            // If platform is close to one point
            if (Vector3.Distance(platform.position, points[currentPoint].position) < 0.5f)
            {
                isWaiting = true;
                waitTimer = maxWaitTime;

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

    public void ElevatorMove()
    {
        // Allow elevator to move
        if (canMove == false)
        {
            canMove = true;
        }

        Debug.Log(canMove);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(gameObject.tag))
        {
            // Instantiate collected vfx
            Instantiate(collectVFX, other.gameObject.transform.position, Quaternion.identity);

            // Disable elevator sign
            elevatorSign.SetActive(false);
            Destroy(other.gameObject);
            ElevatorMove();
        }
    }
}
