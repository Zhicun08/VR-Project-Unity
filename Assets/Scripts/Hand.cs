using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Hand : MonoBehaviour
{
    public static Hand instance;

    // Physics Movement
    [SerializeField] private ActionBasedController controller;
    [SerializeField] private float followSpeed = 30f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private Vector3 positionOffset;
    [SerializeField] private Vector3 rotationOffset;
    [Space]
    [SerializeField] private Transform palm;
    [SerializeField] private float reachDistance = 0.1f, joinDistance = 0.05f;

    [SerializeField] private LayerMask grabbableLayer;
    private int ignorePlayerLayer; //This layer does not collide with the player
    private int originalLayer; //Here we can save the original layer the object was on that was picked up

    public bool isGrabbing;
    private GameObject heldObject;
    private Transform grabPoint;
    private FixedJoint joint1, joint2;

    private Transform followTarget;
    private Rigidbody handRb;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        handRb = GetComponent<Rigidbody>();
        followTarget = controller.gameObject.transform;

        //This gets the layer number from the IgnorePlayer so we dont have to track it ourselves
        ignorePlayerLayer = LayerMask.NameToLayer("IgnorePlayer");

        // Reset Rigidbody setting in case something was changed in Inspector
        handRb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        handRb.interpolation = RigidbodyInterpolation.Interpolate;
        handRb.mass = 20f;
        handRb.maxAngularVelocity = 20f;

        // Input Setup
        controller.selectAction.action.started += Grab;
        controller.selectAction.action.canceled += Released;

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
        // Update Position with Offset
        var positionWithOffset = followTarget.position + positionOffset;

        // Get the distance between target and player's hand
        var distance = Vector3.Distance(positionWithOffset, transform.position);
        handRb.velocity = (positionWithOffset - transform.position).normalized * (followSpeed * distance);

        // Update Rotation with Offset
        var rotationWithOffset = followTarget.rotation * Quaternion.Euler(rotationOffset);

        // Get the difference between the rotation of target and the rotation of player's hand
        var q = rotationWithOffset * Quaternion.Inverse(handRb.rotation);
        // Concert the rotation difference to angle
        q.ToAngleAxis(out float angle, out Vector3 axis);
        handRb.angularVelocity = axis * angle * Mathf.Deg2Rad * rotationSpeed;
    }

    private void Grab(InputAction.CallbackContext context)
    {
        // Check if player is holding something, exit the method
        if (isGrabbing || heldObject) return;

        // If player is not holding something, return a list of grabbable colliders within reach distance
        Collider[] grabbableColliders = Physics.OverlapSphere(palm.position, reachDistance, grabbableLayer);

        // If there's nothing grabbable within reach distance, exit the method
        if (grabbableColliders.Length < 1) return;

        // Store the first grabbable object found
        var objectToGrab = grabbableColliders[0].transform.gameObject;

        // Get the object's rigidbody
        var objectRb = objectToGrab.GetComponent<Rigidbody>();

        // If the object has a rigidbody
        if (objectRb != null)
        {
            // Set object as new held object
            heldObject = objectRb.gameObject;
        }
        // If the object doesn't have a rigidbody
        else
        {
            // Check for rigidbody in parent
            objectRb = objectToGrab.GetComponentInParent<Rigidbody>();
            // If parent has rigidbody
            if (objectRb != null)
            {
                // Set object as new held object
                heldObject = objectRb.gameObject;
            }
            else
            {
                // Exit the method
                return;
            }
        }

        StartCoroutine(GrabObject(grabbableColliders[0], objectRb));
    }

    private IEnumerator GrabObject(Collider collider, Rigidbody targetRb)
    {
        isGrabbing = true;

        // Create a grab point
        grabPoint = new GameObject().transform;
        // Find the closest point on the collider to the palm position
        grabPoint.position = collider.ClosestPoint(palm.position);
        // Parent the grab point to the held object
        grabPoint.parent = heldObject.transform;
        // Save the original layer
        originalLayer = heldObject.gameObject.layer; 
        // Ignore player layer - keeps held objects from hitting the players collider
        heldObject.gameObject.layer = ignorePlayerLayer; 

        // Move hand to grab point
        followTarget = grabPoint;

        // Wait for hand to reach grab point
        while (grabPoint != null && Vector3.Distance(grabPoint.position, palm.position) > joinDistance && isGrabbing)
        {
            yield return new WaitForEndOfFrame();
        }

        // Freeze hand and object motion
        handRb.velocity = Vector3.zero;
        handRb.angularVelocity = Vector3.zero;
        targetRb.velocity = Vector3.zero;
        targetRb.angularVelocity = Vector3.zero;

        targetRb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        targetRb.interpolation = RigidbodyInterpolation.Interpolate;

        // Attach joint from hand to object
        joint1 = gameObject.AddComponent<FixedJoint>();
        joint1.connectedBody = targetRb;
        joint1.breakForce = float.PositiveInfinity;
        joint1.breakTorque = float.PositiveInfinity;

        joint1.connectedMassScale = 1;
        joint1.massScale = 1;
        joint1.enableCollision = false;
        joint1.enablePreprocessing = false;

        // Attach joint from object to hand
        joint2 = heldObject.AddComponent<FixedJoint>();
        joint2.connectedBody = handRb;
        joint2.breakForce = float.PositiveInfinity;
        joint2.breakTorque = float.PositiveInfinity;

        joint2.connectedMassScale = 1;
        joint2.massScale = 1;
        joint2.enableCollision = false;
        joint2.enablePreprocessing = false;

        // Reset follow target
        followTarget = controller.gameObject.transform;
    }

    private void Released(InputAction.CallbackContext context)
    {
        if (joint1 != null)
        {
            Destroy(joint1);
        }

        if (joint2 != null)
        {
            Destroy(joint2);
        }

        if (grabPoint != null)
        {
            Destroy(grabPoint.gameObject);
        }

        if (heldObject != null)
        {
            var targetRb = heldObject.GetComponent<Rigidbody>();
            targetRb.collisionDetectionMode = CollisionDetectionMode.Discrete;
            targetRb.interpolation = RigidbodyInterpolation.None;
            heldObject.gameObject.layer = originalLayer; //Reset the physics layer
            heldObject = null;
        }

        isGrabbing = false;
        followTarget = controller.gameObject.transform;
    }
}
