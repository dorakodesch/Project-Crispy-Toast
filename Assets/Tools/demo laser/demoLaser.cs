using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demoLaser : MonoBehaviour
{
    public float blastRadius = 10;
    public float blastForce = 10;

    // Return null from aiming
    public void Aim(Ray forward, Vector3 position, Quaternion rotation, bool down)
    {
        return;
    }

    // Destroy objects from hammer hit with fire
    public void Fire(Ray forward, Vector3 position, Quaternion rotation)
    {
        RaycastHit hit;
        Physics.Raycast(forward, out hit, 1000.0f);
        Collider[] hits = Physics.OverlapSphere(hit.point, blastRadius);
        foreach (Collider i in hits)
        {
            if (i.tag == "Joint")
            {
                Destroy(i.gameObject);
            }
        }
        foreach(Collider i in hits)
        {
            if(i.tag == "Chunk")
            {
                i.GetComponent<Rigidbody>().AddExplosionForce(blastForce, hit.point, blastRadius);
            }
        }
        return;
    }

    // Return null from instantiation
    public void Inst(Vector3 position, Quaternion rotation)
    {
        return;
    }
}
