// script must be attached to the player object in order to function properly
using UnityEngine;
using UnityEngine.InputSystem;

public class movement : MonoBehaviour
{
    // button pressed variables
    private bool jumpNext = false;
    private bool sprintNext = false;

    // exposed script variables
    [HideInInspector]
    public bool movementOverride = false;

    // refrence environment variables
    private CharacterController playerController;
    private Transform playerCamera;
    public InputMasterActions inputMaster;

    // script inspector attribute variables
    [SerializeField, Range(0f, 100f)]
    private float movementSpeedForward = 50f;
    [SerializeField, Range(0f, 100f)]
    private float movementSpeedSideways = 50f;
    [SerializeField, Range(0f, 1f)]
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

    // create input system catching variables
    private Vector3 inputMovement = new Vector3(0, 0, 0);
    private Vector2 inputLook = new Vector2(0, 0);

    // Awake is called before start
    private void Awake()
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
        look = inputLook;
        playerMovement = inputMovement;

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
        if(!playerController.isGrounded)
            gravityEffect += Physics.gravity.y * Time.fixedDeltaTime;
        else
        {
            gravityEffect = Physics.gravity.y * Time.fixedDeltaTime;
            currentVerticalMovement = 0;
        }
        currentVerticalMovement += gravityEffect * Time.fixedDeltaTime;

        // jump
        if(jumpNext)
        {
            currentVerticalMovement += new Vector3(0, jumpInitialVelocity, 0);
            Debug.Log("Jump");
            currentVerticalMovement += jumpInitialVelocity;
            jumpNext = false;
        }

        // add in the vertical component of movement
        currentMovement += new Vector3(0, currentVerticalMovement, 0);

        // add wasd forward backward left right movement controls to player
        playerMovement = transform.TransformDirection(playerMovement);
        currentMovement += Vector3.Scale(playerMovement, movementSpeed) * Time.fixedDeltaTime;
        if(sprintNext)
            currentMovement *= sprintMutliplier;

        // move the player based the the movement for the current physics update
        if(!movementOverride)
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

    // input manager functions
    public void Movement(InputAction.CallbackContext context)
    {
        inputMovement = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);
    }
    public void Look(InputAction.CallbackContext context)
    {
        inputLook = context.ReadValue<Vector2>();
    }

    // jump function
    public void Jump(InputAction.CallbackContext context)
    {
        if (playerController.isGrounded)
            jumpNext = true;
    }

    public void Sprint(InputAction.CallbackContext context)
    {
        if(context.started || context.canceled)
            sprintNext = !sprintNext;
    }
}
