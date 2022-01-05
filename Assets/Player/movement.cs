// script must be attached to the player object in order to function properly
using System;
using UnityEngine;
using UnityEngine.UI;

public class movement : MonoBehaviour
{
    // refrence environment variables
    private CharacterController playerController;
    private Transform playerCamera;

    // script inspector attribute variables
    [SerializeField, Range(0f, 100f)]
    private float movementSpeedForward = 50f;
    [SerializeField, Range(0f, 100f)]
    private float movementSpeedSideways = 50f;
    [SerializeField, Range(0f, 5f)]
    private float lookSpeed = 1f;
    [SerializeField, Range(5f, 90f)]
    private float lookUpperLimit = 85f;
    [SerializeField, Range(-90f, -5f)]
    private float lookLowerLimit = -85f;
    [SerializeField, Range(0f, 1f)]
    private float jumpInitialVelocity = 1f;
    [SerializeField, Range(0f, 1f)]
    private float jumpSenseRange = .1f;
    [SerializeField, Range(1, 3)]
    public float sprintMutliplier = 1f;

    // script private condensed vars
    private Vector3 movementSpeed;


    // global vars to be refrenced in multiple functions
    private Vector3 playerMovement;
    private Vector2 look;
    private float gravityEffect;
    private float currentVerticalMovement;

    // Start is called before the first frame update
    private void Start()
    {
        // get the refrence for the character controller
        playerController = this.GetComponent<CharacterController>();

        // get the refrence for the character camera
        playerCamera = this.transform.GetChild(0).transform;

        // set the target frame rate for the application to 60fps
        Application.targetFrameRate = 60;

        // initalize globar vars
        look = new Vector2(0, 0);
        playerMovement = new Vector3(0, 0, 0);
        gravityEffect = 0;
        currentVerticalMovement = 0;

        // lock cursor to center of screen
        Cursor.lockState = CursorLockMode.Locked;

        // condense vars
        movementSpeed = new Vector3(movementSpeedForward, 0, movementSpeedSideways);
    }

    // Update is called once per frame
    private void Update()
    {
        // get mouse and keyboard input
        look = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        playerMovement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // rotate camera vertically to look up and down with mouse
        // set current looking direction as temp var
        float directionTempVert = playerCamera.eulerAngles.x;

        // query current direction, normalize, and add movement
        float verticalAngle = normalizeAngle(playerCamera.rotation.eulerAngles.x);
        verticalAngle += Vector2.Scale(look, new Vector2(lookSpeed, lookSpeed)).y;

        // clamp to possible range and denormalize
        verticalAngle = Mathf.Clamp(verticalAngle, lookLowerLimit, lookUpperLimit);
        verticalAngle = denormalizeAngle(verticalAngle);

        // set direction of camera
        playerCamera.Rotate(new Vector3(verticalAngle - directionTempVert, 0, 0));
    }

    // Fixed Update is called once per physics cycle
    private void FixedUpdate()
    {
        // create current physics cycle movement variable
        Vector3 currentMovement = new Vector3(0, 0, 0);

        // calculate gravity movement
        if(!isGrounded())
        {
            gravityEffect += Physics.gravity.y * Time.fixedDeltaTime;
        }
        else
        {
            gravityEffect = Physics.gravity.y * Time.fixedDeltaTime;
            currentVerticalMovement = 0;
        }
        currentVerticalMovement += gravityEffect * Time.fixedDeltaTime;

        // jump function
        RaycastHit jumpRayHit;
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down + new Vector3(0f, -playerController.height / 2, 0f)) * 1f, out jumpRayHit, 1f);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down + new Vector3(0f, -playerController.height / 2, 0f)) * 1f, Color.green);
        if (jumpRayHit.distance <= jumpSenseRange && Input.GetButtonDown("Jump"))
        if(isGrounded() && Input.GetButtonDown("Jump"))
        {
            currentVerticalMovement += new Vector3(0, jumpInitialVelocity, 0);
            Debug.Log("Jump");
            currentVerticalMovement += jumpInitialVelocity;
        }

        // add in the vertical component of movement
        currentMovement += new Vector3(0, currentVerticalMovement, 0);

        // add wasd forward backward left right movement controls to player
        playerMovement = transform.TransformDirection(playerMovement);
        currentMovement += Vector3.Scale(playerMovement, movementSpeed) * Time.fixedDeltaTime;
        if(!Input.GetButton("Sprint"))
        {
            currentMovement *= sprintMutliplier;
        }

        // move the player based the the movement for the current physics update
        playerController.Move(currentMovement);

        // rotate player horizontally to look at mouse
        transform.Rotate(new Vector3(0, Vector2.Scale(look, new Vector2(lookSpeed, lookSpeed)).x, 0));
    }

    // normalize angle to return between -180 and 180, centered on horizon with negative values facing down
    float normalizeAngle(float angle)
    {
        angle = -(angle - 180f - Mathf.Sign(angle - 180f) * 180f);
        return angle;
    }

    // reverse normalization of angle
    float denormalizeAngle(float angle)
    {
        angle = -(-360f + angle) % 360f;
        return angle;
    }

    bool isGrounded()
    {
        return playerController.isGrounded;
    }
}
