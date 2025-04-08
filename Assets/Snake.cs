using System;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;
using UnityEngine.Playables;
public class Snake : MonoBehaviour
{
    
   //globals
    [Header("Snake Components")]
    public Transform head;              //head of snake to target movement
    public List<Transform> bodySegments; // list of body parts to target follow

    [Header("Movement Settings")]
    public float speed = 5f;            //sppeed
    public float turnSpeed = 100f;       
    public float segmentDistance = 1f;  

    [Header("Jump Settings")]
    public float jumpForce = 50f;        
    public LayerMask groundLayer;

    [Header("Startup")]
    public Vector3 startPosition;

    // private
    private Rigidbody headRigidbody;

    [SerializeField]
    private bool isGrounded;
    private Vector2 moveInput;
    void Awake()
    {
        

        // Set up RB - note all of this can be done in editor
        headRigidbody = head.GetComponent<Rigidbody>();
        if (headRigidbody == null)
        {
            headRigidbody = head.gameObject.AddComponent<Rigidbody>();
        }
        headRigidbody.useGravity = true;
        headRigidbody.freezeRotation = true;//no tilt

        startPosition = transform.position;
    }


    void Start()
    {
        // put body segments behind the head initially
        Vector3 position = head.position;
        Vector3 direction = -head.forward;
        for (int i = 0; i < bodySegments.Count; i++)
        {
            position += direction * segmentDistance;
            bodySegments[i].position = position;
            bodySegments[i].rotation = head.rotation;
        }
    }

    void Update()
    {
        //will explain this later
        isGrounded = Physics.Raycast(head.position, Vector3.down, 2.0f, groundLayer);
        
        //move forward
        Vector3 horizontalMovement = head.forward * speed * Time.deltaTime;
        headRigidbody.MovePosition(headRigidbody.position + horizontalMovement);

        // foreach segment, follow
        for (int i = 0; i < bodySegments.Count; i++)
        {
            //some math to smooth rotate and follow
            Transform frontSegment = (i == 0) ? head : bodySegments[i - 1];
            Vector3 targetPosition = frontSegment.position - frontSegment.forward * segmentDistance;
            bodySegments[i].position = Vector3.MoveTowards(bodySegments[i].position, targetPosition, speed * Time.deltaTime);
            Vector3 directionToTarget = (targetPosition - bodySegments[i].position).normalized;
            if (directionToTarget != Vector3.zero)
            {
                bodySegments[i].rotation = Quaternion.LookRotation(directionToTarget);
            }
        }
    }

    void FixedUpdate()
    {
        // rotate head in fixed update if moveInput
        float horizontal = moveInput.x;
        head.Rotate(0, horizontal * turnSpeed * Time.deltaTime, 0);
    }

    // jump force
    public void OnJump()
    {
        if (isGrounded)
        {
            headRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}