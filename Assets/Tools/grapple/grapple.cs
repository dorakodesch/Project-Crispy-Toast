using System.Collections;
using UnityEngine;

public class grapple : MonoBehaviour
{
    public float range = 100;
    public LayerMask targetLayers = 7;

    // Aim down sights on aim function call
    public void Aim(Ray forward, Vector3 position, Quaternion rotation, bool down)
    {

    }

    // Fire grapple on fire call
    public void Fire(Ray forward, Vector3 position, Quaternion rotation)
    {
        // Cast ray at object to attach grapple
        if(Physics.Raycast(forward, out RaycastHit hit, range))
        {
            Vector3 destination = hit.point;
            this.GetComponentInParent<movement>().grappleTarget = destination;
            this.GetComponentInParent<movement>().currentMoveType = movement.MovementType.GRAPPLE;
        }
    }

    // Return null for instantiate
    public void Inst(Vector3 position, Quaternion rotation)
    {
        return;
    }

    // Coroutine to move player to grapple point
    IEnumerator grapplePull(Vector3 destination)
    {
        yield return null;
    }
}
