using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float moveSpeed;
    [SerializeField] float acceleration;
    [SerializeField] float decceleration;
    [SerializeField] float velPower;

    //private input values
    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDir;
    private Vector3 movementVel;


    //references
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        MovePlayer();
    }

    public void HandleMoveInput(InputAction.CallbackContext ctx)
    {
        moveDir = ctx.ReadValue<Vector2>();

        if (moveDir.magnitude > 1)
        {
            moveDir = moveDir / moveDir.magnitude;
        }
    }

    public void HandleJumpInput(InputAction.CallbackContext ctx)
    {
        
    }

    void MovePlayer()
    {
        float targetSpeedHorizontal = moveDir.x * moveSpeed;
        float targetSpeedVertical = moveDir.y * moveSpeed;
        float speedDifX = targetSpeedHorizontal - rb.velocity.x;
        float speedDifZ = targetSpeedVertical - rb.velocity.z;
        float accelRateX = (Mathf.Abs(speedDifX) > 0.01f) ? acceleration : decceleration;
        float accelRateZ = (Mathf.Abs(speedDifZ) > 0.01f) ? acceleration : decceleration;

        float movementX = Mathf.Pow(Mathf.Abs(speedDifX) * accelRateX, velPower) * Mathf.Sign(speedDifX);
        float movementZ = Mathf.Pow(Mathf.Abs(speedDifZ) * accelRateZ, velPower) * Mathf.Sign(speedDifZ);

        movementVel.x = movementX * Vector2.right.x;
        movementVel.z = movementZ * Vector3.forward.z;

        rb.AddForce(movementVel);
    }


}
