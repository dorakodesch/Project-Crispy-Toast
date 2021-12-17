using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grapple : MonoBehaviour
{
    // inspector fields
    [SerializeField]
    private float range = 100;
    public LayerMask layer;

    // fire function
    public void fire(Ray forward)
    {
        // create hit
        RaycastHit hit;

        // check for ray collision
        if (Physics.Raycast(forward, out hit, range, layer))
        {
            // run launch function with hit
            launch(hit.point);
        }
    }

    // hook launch function
    void launch(Vector3 target)
    {

    }
}
