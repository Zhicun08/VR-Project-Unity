using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    // Physics Movement
    [SerializeField] private GameObject followObject;
    [SerializeField] private float followSpeed = 30f;
    private Transform followTarget;
    private Rigidbody handRb;

    // Start is called before the first frame update
    void Start()
    {
        handRb = GetComponent<Rigidbody>();
        followTarget = followObject.transform;

        // Reset Rigidbody setting in case something was changed in Inspector
        handRb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        handRb.interpolation = RigidbodyInterpolation.Interpolate;
        handRb.mass = 20f;

        // Teleport hands
        handRb.position = followTarget.position;
        handRb.rotation = followTarget.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        PhysicsMove();
    }

    private void PhysicsMove()
    {
        // Update Position
        var distance = Vector3.Distance(followTarget.position, transform.position);
        handRb.velocity = (followTarget.position - transform.position).normalized * (followSpeed * distance);
    }
}
