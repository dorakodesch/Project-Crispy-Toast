using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotInventory : MonoBehaviour
{
    private int[] inStorage = new int[7];
    [SerializeField, Min(0f)]
    float resourceBreakdownRadius = 5f; // minimum distance for resource checks
    Collider[] colliders; // colliders grabbed by physics.overlapsphere

    // Update is called once per frame
    void Update()
    {
        colliders = Physics.OverlapSphere(transform.position, resourceBreakdownRadius);

        foreach (Collider i in colliders)
        {
            // checking for object chunks to destroy
            if (i.gameObject.GetComponent<DestructableChunk>())
            {
                // idk how but this isnt redundant
                if(i.gameObject.GetComponent<DestructableChunk>().jointsGone)
                {
                    Destroy(i.gameObject);
                    inStorage[0]++;
                }
            }

            // checking for players to transfer to
            if (i.gameObject.GetComponent<inventory>())
            {
                for (int j = 0; j < inStorage.Length; j++)
                {
                    i.gameObject.GetComponent<inventory>().resourceCounters[j] += inStorage[j];
                    inStorage[j] = 0;
                }
            }
        } 
    }
}
