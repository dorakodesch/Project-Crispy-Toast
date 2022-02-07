// robot movement, currently just follows the player around
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class RobotNavigation : MonoBehaviour
{
	[SerializeField]
	Transform player;

	[SerializeField, Min(1f)]
	float distanceFromPlayer = 1.5f;

	[SerializeField, Min(0f)]
	float resourceBreakdownRadius = 5f; // minimum distance for resource checks

	Collider[] colliders; // colliders grabbed by physics.overlapsphere

	int state;

	Vector3 destination;

	NavMeshAgent agent;

	DestructableChunk[] chunks;

	private void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		state = 0;
		destination = transform.position;
	}

	private void Update()
	{
		// segment check
		// physics.overlapsphere grabs all colliders within the sphere defined by (center, radius)
		colliders = Physics.OverlapSphere(transform.position, resourceBreakdownRadius);

		// checking for object chunks
		for (int i = 0; i < colliders.Length; i++)
		{
			Collider collider = colliders[i];
			if (collider.gameObject.GetComponent<DestructableChunk>())
			{
				if (collider.gameObject.GetComponent<Rigidbody>())
				{
					Destroy(collider.gameObject);
					state = 0;
				}
                else
                {
					state = 0;
                }
			}
		}

		if (state == 0)
        {
			destination = player.position + Vector3.right * distanceFromPlayer;

			// segment check for destination
			chunks = FindObjectsOfType<DestructableChunk>();

			for (int i = 0; i < chunks.Length; i++)
            {
				if (chunks[i].gameObject.GetComponent<Rigidbody>())
                {
					destination = chunks[i].transform.position;
					state = 1;
					Debug.Log("test");
					break;
                }
            }
		}

		agent.SetDestination(destination);
	}
}