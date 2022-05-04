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

    private bool IsGrounded => Physics.Raycast(
        new Vector2(transform.position.x, transform.position.y + 2.0f),
        Vector3.down, 2.0f);

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
    }

    private void OnJump(InputAction.CallbackContext obj)
    {
        if (!IsGrounded) return;
        playerRb.AddForce(Vector3.up * jumpForce);
    }


}
