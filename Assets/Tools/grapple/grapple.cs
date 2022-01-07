using UnityEngine;

public class grapple : ToolMaster
{
    // inspector fields
    [SerializeField]
    private float range = 100;
    public LayerMask layer;

    // fire function
    public override void Fire(Ray forward)
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

    // aim function
    public override void Aim(Ray forward)
    {
        return;
    }

    // hook launch function
    void attachGrapple(Vector3 target)
    {

    }
}
