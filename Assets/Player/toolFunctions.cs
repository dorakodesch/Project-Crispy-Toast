using System;
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

    // Update is called once per frame
    void Update()
    {

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
}
