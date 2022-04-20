// script must be attached to the player object in order to function properly
using UnityEngine;
using UnityEngine.InputSystem;

public class movement : MonoBehaviour
{
    // movement state
    [HideInInspector]
    public enum MovementType { GROUND, AIR, GRAPPLE }
    [HideInInspector]
    public MovementType currentMoveType = MovementType.GROUND;

    // public refrence vars
    [SerializeField]
    private Texture2D crosshair;

    // button pressed variables
    private bool jumpNext = false;
    private bool sprintNext = false;

    // refrence environment variables
    private CharacterController playerController;
    private Transform playerCamera;
    [HideInInspector]
    public InputMasterActions inputMaster;
    [HideInInspector]
    public Vector3 grappleTarget;

    // script inspector attribute variables
    [SerializeField, Range(0f, 100f)]
    private float movementSpeedForward = 50f;
    [SerializeField, Range(0f, 100f)]
    private float movementSpeedSideways = 50f;
    [SerializeField, Range(0f, 10f)]
    private float lookSpeed = 1f;
    [SerializeField, Range(5f, 90f)]
    private float lookUpperLimit = 85f;
    [SerializeField, Range(-90f, -5f)]
    private float lookLowerLimit = -85f;
    [SerializeField, Range(0f, 100f)]
    private float jumpInitialVelocity = 1f;
    [SerializeField, Range(1f, 3f)]
    private float sprintMutliplier = 1f;
    [SerializeField, Range(0f, 1f)]
    private float airMoveMultiplier = 0.5f;
    [SerializeField, Range(0f, 100f)]
    private float grappleMoveMultiplier = 10f;

    // script private condensed vars
    private Vector3 movementSpeed;

    // local vars to be refrenced in multiple functions
    private Vector3 playerMovement;
    private Vector2 look;
    private float gravityEffect;
    private float currentVerticalMovement;

    // create input system catching variables
    private Vector3 inputMovement = new Vector3(0, 0, 0);
    private Vector2 inputLook = new Vector2(0, 0);

    float temp = 0;

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

        // lock cursor to center of screen and texture
        Cursor.lockState = CursorLockMode.Locked;

        // condense vars
        movementSpeed = new Vector3(movementSpeedForward, 0, movementSpeedSideways);
    }

    // Update is called once per frame and is used only for look functions
    private void Update()
    {
        // get mouse and keyboard input
        look = inputLook;
        if (sprintNext == true && inputMovement.z > 0)
            // add sprint multiplier for forward running only
            playerMovement = Vector3.Scale(inputMovement, new Vector3(1, 1, sprintMutliplier));
        else
            playerMovement = inputMovement;

        // rotate camera vertically to look up and down with mouse
        // set current looking direction as temp var
        float directionTempVert = playerCamera.eulerAngles.x;

        // query current direction, normalize, and add movement
        float verticalAngle = normalizeAngle(playerCamera.rotation.eulerAngles.x);
        verticalAngle += look.y * lookSpeed;

        // clamp to possible range and denormalize
        verticalAngle = Mathf.Clamp(verticalAngle, lookLowerLimit, lookUpperLimit);
        verticalAngle = denormalizeAngle(verticalAngle);

        // set direction of camera
        playerCamera.Rotate(new Vector3(verticalAngle - directionTempVert, 0, 0));
    }

    // Fixed Update is called once per physics cycle and is used for movement
    private void FixedUpdate()
    {
        // create current physics cycle movement variable
        Vector3 currentMovement = new Vector3(0, 0, 0);

        // switch based on movement type
        switch (currentMoveType)
        {
            case MovementType.GROUND:
                // jump only if on ground
                if(jumpNext)
                {
                    currentVerticalMovement = jumpInitialVelocity;
                    jumpNext = false;
                    currentMovement += new Vector3(0, currentVerticalMovement, 0);
                    currentMoveType = MovementType.AIR;
                    temp = Time.time;
                    break;
                }

                // force player into ground when grounded
                currentVerticalMovement = -1f;
                currentMovement += new Vector3(0, currentVerticalMovement, 0);

                // apply keyboard input movement to player when grounded
                playerMovement = transform.TransformDirection(playerMovement);
                currentMovement += Vector3.Scale(playerMovement, movementSpeed) * Time.fixedDeltaTime;

                // switch to air
                if (!playerController.isGrounded)
                {
                    currentMoveType = MovementType.AIR;
                    temp = Time.time;
                }
                break;
            case MovementType.AIR:
                // kill jump when already in air
                jumpNext = false;

                // apply gravity in air
                gravityEffect = Physics.gravity.y * Time.fixedDeltaTime;
                currentVerticalMovement += gravityEffect;
                currentMovement += new Vector3(0, currentVerticalMovement, 0);

                // apply air movement from keyboard input
                playerMovement = transform.TransformDirection(playerMovement);
                currentMovement += Vector3.Scale(playerMovement, movementSpeed) * airMoveMultiplier * Time.fixedDeltaTime;

                // switch to ground
                if (playerController.isGrounded)
                {
                    currentMoveType = MovementType.GROUND;
                }
                    
                break;
            case MovementType.GRAPPLE:
                // kill jump when grappling
                jumpNext = false;
                if(Mathf.Abs(Vector3.Distance(grappleTarget, this.transform.position)) > 2)
                {
                    Vector3 normalGrapplePull = (grappleTarget - this.transform.position).normalized;
                    Vector3 physicalGrapplePull = normalGrapplePull * grappleMoveMultiplier;
                    currentMovement = Vector3.zero;
                    currentMovement += physicalGrapplePull * Time.deltaTime * grappleMoveMultiplier;
                }
                else
                {
                    currentMoveType = MovementType.AIR;
                }
                break;
        }

        // move the player based the the movement for the current physics update
        playerController.Move(currentMovement);

        // rotate player horizontally to look at mouse
        transform.Rotate(new Vector3(0, look.x * lookSpeed, 0));
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

    // movement function
    public void Movement(InputAction.CallbackContext context)
    {
        inputMovement = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);
    }

    // look function
    public void Look(InputAction.CallbackContext context)
    {
        inputLook = context.ReadValue<Vector2>() * Time.deltaTime;
    }

    // jump function
    public void Jump(InputAction.CallbackContext context)
    {
        jumpNext = true;
    }

    // sprint function
    public void Sprint(InputAction.CallbackContext context)
    {
        if(context.started || context.canceled)
            sprintNext = !sprintNext;
    }
}
