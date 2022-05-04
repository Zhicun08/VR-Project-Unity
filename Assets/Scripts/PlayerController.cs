using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionReference jumpActionReference;
    [SerializeField] private float jumpForce = 500.0f;

    private Rigidbody playerRb;
    private CapsuleCollider playerCollider;
    private XROrigin xrOrigin;

    public Transform feetTrans; //Position of where the players feet touch the ground
    float groundCheckDist = .5f; //How far down to check for the ground. The radius of Physics.CheckSphere
    public bool grounded = false; //Is the player on the ground
    //The physics layers you want the player to be able to jump off of. Just dont include the layer the palyer is on.
    public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
        xrOrigin = GetComponent<XROrigin>();
        jumpActionReference.action.performed += OnJump;
    }

    // Update is called once per frame
    void Update()
    {
        var center = xrOrigin.CameraInOriginSpacePos;
        playerCollider.center = new Vector3(center.x, playerCollider.center.y, center.z);
        playerCollider.height = xrOrigin.CameraInOriginSpaceHeight;

        //The sphere check draws a sphere like a ray cast and returns true if any collider is withing its radius.
        //grounded is set to true if a sphere at feetTrans.position with a radius of groundCheckDist detects any objects on groundLayer within it
        grounded = Physics.CheckSphere(feetTrans.position, groundCheckDist, groundLayer);
    }

    private void OnJump(InputAction.CallbackContext obj)
    {
        if (!grounded) return;
        playerRb.AddForce(Vector3.up * jumpForce);
    }


}
