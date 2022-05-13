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

    private Transform player = null; 
    private Rigidbody playerRb = null; 

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
    }
}
