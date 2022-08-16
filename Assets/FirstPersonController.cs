using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
// Base code from https://sharpcoderblog.com/blog/unity-3d-fps-controller
public class FirstPersonController: MonoBehaviour
{
    bool CanSecondJump = false;
    bool Landed = true;
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    public float CanSecondJumpTimer = 0;
    float lastSpeedX;
    float lastSpeedY;
    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
       
        float curSpeedX = 0;
        float curSpeedY = 0;
       
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
       
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        curSpeedX = canMove ? (!characterController.isGrounded && Input.GetAxis("Vertical") == 0 ? lastSpeedX : (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical")) :  0;
        curSpeedY = canMove ? (!characterController.isGrounded && Input.GetAxis("Vertical") == 0 ? lastSpeedX : (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal")) : 0;
            

        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);
       

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
        lastSpeedX = curSpeedX;
        lastSpeedY = curSpeedY;

    }
}
