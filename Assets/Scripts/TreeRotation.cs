using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeRotation : MonoBehaviour
{
    [SerializeField] private float rotatingSpeed = 30f;
    [SerializeField] private float leftRotationConstraint = 50f;
    [SerializeField] private float rightRotationConstraint = -50f;
    private float smoothReturnValue = 25f;
    private float horizontalRotating = 0f;
    private Rigidbody2D rigidBody2D;

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontalRotating = Input.GetAxisRaw("Horizontal") * rotatingSpeed;
    }

    private void FixedUpdate()
    {
        if (rigidBody2D.rotation > leftRotationConstraint)
        {
            rigidBody2D.rotation = Mathf.Lerp(rigidBody2D.rotation, leftRotationConstraint, smoothReturnValue * Time.deltaTime);
        }
        if (rigidBody2D.rotation < rightRotationConstraint)
        {
            rigidBody2D.rotation = Mathf.Lerp(rigidBody2D.rotation, rightRotationConstraint, smoothReturnValue * Time.deltaTime);
        }
        else
        {
            rigidBody2D.rotation += -horizontalRotating * Time.fixedDeltaTime;
        }

    }
}
