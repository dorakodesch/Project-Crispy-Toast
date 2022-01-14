using UnityEngine;
using UnityEngine.InputSystem;

public class toolFunctions : MonoBehaviour
{
    // Create variables for object in hand
    [SerializeField]
    private enum inHand { START, hammer = START, grapple, END = grapple };
    private inHand current;
    [SerializeField]
    private GameObject[] tools;
    private GameObject currentTool;

    // Start is called before the first frame update
    void Start()
    {
        current = inHand.hammer;
        replaceHand();
    }

    // Change object function
    public void changeInHand(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            // Cycle through in hand values
            current += (int)context.ReadValue<Vector2>().y;
            if(current < inHand.START)
            {
                current = inHand.END;
            }
            else if(current > inHand.END)
            {
                current = inHand.START;
            }
            replaceHand();
        }
    }

    // Instantiate new object in hand
    void replaceHand()
    {
        if(currentTool != null)
            Destroy(currentTool);
        currentTool = Instantiate(tools[(int)current]);
        
        // Set parent and position to that of player
        currentTool.GetComponent<Transform>().SetParent(this.transform, false);
    }

    // Called on fire button down
    public void Fire(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            Transform cam = this.GetComponentInChildren<Camera>().transform;
            Ray forward = new Ray(cam.position, cam.TransformDirection(Vector3.forward));
            currentTool.GetComponent<ToolMaster>().Fire(forward);
        }
    }

    // Called on aim
    public void Aim(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            Ray forward = new Ray(this.transform.position, this.GetComponent<Camera>().transform.TransformDirection(Vector3.forward));
            currentTool.GetComponent<ToolMaster>().Aim(forward, true);
        }
        if(context.canceled)
        {
            Ray forward = new Ray(this.transform.position, this.GetComponent<Camera>().transform.TransformDirection(Vector3.forward));
            currentTool.GetComponent<ToolMaster>().Aim(forward, false);
        }
    }
}
