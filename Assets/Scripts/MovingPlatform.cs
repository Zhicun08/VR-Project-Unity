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

    // Start is called before the first frame update
    void Start()
    {
        canMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove == true && DetectPlayer.instance.playerDetect == true)
        {
            //Debug.Log("Moving");
            // Disable elevator sign
            elevatorSign.SetActive(false);

            // Move platform to the current point
            platform.position = Vector3.MoveTowards(platform.position, points[currentPoint].position, moveSpeed * Time.deltaTime);

            // If platform is close to one point
            if (Vector3.Distance(platform.position, points[currentPoint].position) < 0.5f)
            {
                StartCoroutine(DelayCountdown());
                // Increase the current point
                currentPoint++;

                // if the current point is the end of the list, reset it to 0
                if (currentPoint >= points.Length)
                {
                    currentPoint = 0;
                }
            }
        }

        if (!DetectPlayer.instance.playerDetect)
        {
            // Enable elevator sign
            elevatorSign.SetActive(true);
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

    IEnumerator DelayCountdown()
    {
        canMove = false;
        yield return new WaitForSeconds(5f);
        canMove = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(gameObject.tag))
        {
            ElevatorMove();
            // Disable elevator sign
            elevatorSign.SetActive(false);
            Destroy(other.gameObject);
            // Instantiate collected vfx
            Instantiate(collectVFX, other.gameObject.transform.position, Quaternion.identity);
        }
    }
}
