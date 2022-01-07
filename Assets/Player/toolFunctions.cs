using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toolFunctions : MonoBehaviour
{
    // Create variables for object in hand
    public enum inHand { hammer, grapple };
    public inHand current;
    public GameObject[] tools;
    public int objIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        current = inHand.hammer;
    }

    // Update is called once per frame
    void Update()
    {
        changeInHand();
    }

    // Change object function
    public void changeInHand()
    {

    }
}
