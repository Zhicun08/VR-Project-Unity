using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    public static DetectPlayer instance;

    public bool playerDetect;

    public Transform feetTrans; //Position of where the players feet touch the ground
    private float groundCheckDist = .5f; //How far down to check for the ground. The radius of Physics.CheckSphere
    public LayerMask elevatorLayer;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        playerDetect = false;
    }

    // Update is called once per frame
    void Update()
    {
        //The sphere check draws a sphere like a ray cast and returns true if any collider is withing its radius.
        //grounded is set to true if a sphere at feetTrans.position with a radius of groundCheckDist detects any objects on groundLayer within it
        playerDetect = Physics.CheckSphere(feetTrans.position, groundCheckDist, elevatorLayer);
        if (playerDetect == true)
        {
            Debug.Log(playerDetect);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = gameObject.transform;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }
}
