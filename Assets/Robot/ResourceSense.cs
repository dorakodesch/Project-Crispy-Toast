using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ResourceSense : MonoBehaviour
{
    // radius for robot destroying free segments
    [SerializeField, Min(0f)]
    float resourceBreakdownRadius;

    DestructableChunk[] chunks;

    NavMeshAgent agent;

    int state; // following player or not, int and not bool bc might have more states later

    Collider[] colliders;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        state = 0;
    }

    private void Update()
    {
        if (state == 0)
        {
            // grabs all chunks within scene
            chunks = FindObjectsOfType<DestructableChunk>();

            // checks if chunk is "free" (ie has a rigidbody)
            for (int i = 0; i < chunks.Length; i++)
            {
                GameObject chunkObject = chunks[i].gameObject;
                if (chunkObject.GetComponent<Rigidbody>())
                {
                    agent.SetDestination(chunkObject.transform.position);
                    state = 1;
                    Debug.Log("chunk registered");
                    break;
                }
            }
        }
        else
        {
            colliders = Physics.OverlapSphere(transform.position, resourceBreakdownRadius);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].GetComponent<DestructableChunk>())
                {
                    if (colliders[i].GetComponent<Rigidbody>())
                    {
                        Destroy(colliders[i].gameObject);
                        state = 0;
                    }
                }
            }
        }
    }
}
