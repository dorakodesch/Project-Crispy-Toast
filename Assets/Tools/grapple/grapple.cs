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
            attachGrapple(hit.point);
        }
    }

    // hook launch function
    void attachGrapple(Vector3 target)
    {

    }
}
