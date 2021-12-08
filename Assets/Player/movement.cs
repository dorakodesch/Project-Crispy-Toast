// script must be attached to the player object in order to function properly
using System;
using UnityEngine;

public class movement : MonoBehaviour
{
    // refrence environment variables
    private CharacterController playerController;
    private Transform playerCamera;

    // script public attribute variables
    public Vector3 movementSpeed = new Vector3(1, 1, 1);
    public Vector2 lookSpeed = new Vector2(1, 1);
    public float lookUpperLimit = 85f;
    public float lookLowerLimit = -85f;

    // global vars to be refrenced in multiple functions
    private Vector3 playerMovement;
    private Vector2 look;
    private Vector3 gravityEffect;

    // Start is called before the first frame update
    private void Start()
    {
        // get the refrence for the character controller
        playerController = this.GetComponent<CharacterController>();

        // get the refrence for the character camera
        playerCamera = this.transform.GetChild(0).transform;

        // set the target frame rate for the application to 60fps
        Application.targetFrameRate = 60;

        // set global vars initially
        look = new Vector2(0, 0);
        playerMovement = new Vector3(0, 0, 0);
        gravityEffect = new Vector3(0, 0, 0);
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
        verticalAngle += Vector2.Scale(look, lookSpeed).y;
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
        {
            //gravityEffect -= Physics.gravity;
            Debug.Log("grounded");
        }
        else
        {
            gravityEffect = new Vector3(0, 0, 0);
        }
        currentMovement += gravityEffect * Time.fixedDeltaTime;

        // add wasd forward backward left right movement controls to player
        playerMovement = transform.TransformDirection(playerMovement);
        currentMovement += Vector3.Scale(playerMovement, movementSpeed) * Time.fixedDeltaTime;

        // move the player based the the movement for the current physics update
        playerController.Move(currentMovement);

        // rotate player horizontally to look at mouse
        transform.Rotate(new Vector3(0, Vector2.Scale(look, lookSpeed).x, 0));
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
}
