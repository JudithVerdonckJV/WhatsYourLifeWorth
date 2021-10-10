using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BaseMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;

    private Animator animator;

    private Vector2 moveDirection;
    private Rigidbody2D rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void SetMoveDirection( Vector2 dir)
    {
        moveDirection = dir;
    }

    private void Update()
    {
        moveDirection.Normalize();

        if (animator == null) return;
        animator.SetFloat("Horizontal", moveDirection.x);
        animator.SetFloat("Vertical", moveDirection.y);
        animator.SetFloat("Speed", moveDirection.sqrMagnitude);
    }

    void FixedUpdate()
    {
        if (rigidBody == null) return;
        rigidBody.MovePosition(rigidBody.position + moveDirection * movementSpeed * Time.fixedDeltaTime);
    }
}
