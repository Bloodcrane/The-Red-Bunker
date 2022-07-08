using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    //the charachtercompononet for movement
    public CharacterController cc;
    public HeadBobController headBobController;

    float yVelocity = 0f;
    [Range(5f,25f)]
    public float gravity = 15f;
    //the speed of the player movement
    [Range(5f,15f)]
    public float movementSpeed = 10f, defaultMovementSpeed = 10f;
    //Run Speed
    [Range(5f, 15f)]
    public float runSpeed = 20f;
    //jump speed
    [Range(5f,15f)]
    public float jumpSpeed = 10f;
    public float climbSpeed = 50f;

    //now the camera so we can move it up and down
    Transform cameraTransform;
    float pitch = 0f;
    [Range(1f,90f)]
    public float maxPitch = 85f;
    [Range(-1f, -90f)]
    public float minPitch = -85f;
    [Range(0.5f, 5f)]
    public float mouseSensitivity = 2f;
    public bool Walking;
    public bool Running;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cc = GetComponent<CharacterController>();
        headBobController = GetComponent<HeadBobController>();
        cameraTransform = GetComponentInChildren<Camera>().transform;
        defaultMovementSpeed = movementSpeed;
    }

    void Update()
    {
        Look();
        Move();
        Run();
    }

    void Look()
    {
        //get the mouse inpuit axis values
        float xInput = Input.GetAxis("Mouse X") * mouseSensitivity;
        float yInput = Input.GetAxis("Mouse Y") * mouseSensitivity;
        //turn the whole object based on the x input
        transform.Rotate(0, xInput, 0);
        //y input to pitch
        pitch -= yInput;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        //create the local rotation value for the camera and set it
        Quaternion rot = Quaternion.Euler(pitch, 0, 0);
        cameraTransform.localRotation = rot;
    }

    void Move()
    {
        //update speed based on the input
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        input = Vector3.ClampMagnitude(input, 1f);
        //transofrm it based off the player transform and scale it by movement speed
        Vector3 move = transform.TransformVector(input) * movementSpeed;
        //is it on the ground
        if (cc.isGrounded)
        {
            //yVelocity = -gravity * Time.deltaTime;
            //check for jump here
            if (Input.GetButtonDown("Jump"))
            {
                yVelocity = jumpSpeed;
            }
        }
        // add the gravity to the yvelocity
        yVelocity -= gravity * Time.deltaTime;
        move.y = yVelocity;
        //and finally move
        cc.Move(move * Time.deltaTime);

        //Move Check
        if (input.x != 0 || input.z != 0 && cc.isGrounded)
        {
            Walking = true;
        }
        else
        {
            Walking = false;
        }
    }

    void Run()
    {
        // Stop running
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Running = false;
            headBobController._frequency = 5f;
            movementSpeed = defaultMovementSpeed;
        }

        // Check if the player is walking and is on ground
        if (Running == false && cc.isGrounded == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Running = true;
                headBobController._frequency = 10f;
                if(cc.isGrounded == false)
                {
                    Running = false;
                }
            }
        }

        // If running is true then the player speed will increase
        if(Running == true)
        {
            movementSpeed = runSpeed;
        }
        else
        {
            movementSpeed = defaultMovementSpeed;
        }
    }
}
