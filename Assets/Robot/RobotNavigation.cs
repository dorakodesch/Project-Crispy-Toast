// robot movement, currently just follows the player around
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class RobotNavigation : MonoBehaviour
{
	[SerializeField]
	Transform player;

	[SerializeField, Min(1f)]
	float distanceFromPlayer = 10f;

	[SerializeField, Min(0f)]
	float resourceBreakdownRadius = 5f; // minimum distance for resource checks

	Vector3 destination;

	NavMeshAgent agent;

	DestructableChunk[] chunks;

	private void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		destination = transform.position;
	}

	private void Update()
	{
		// segment check
        // set destination to player
        if(Vector3.Distance(player.position, this.transform.position) >= distanceFromPlayer)
		{
			destination = player.position + Vector3.Normalize(player.position - this.transform.position) * distanceFromPlayer;
		}
        else
        {
			destination = this.transform.position;
        }

		// segment check for destination and set destination to chunk
		chunks = FindObjectsOfType<DestructableChunk>();
		foreach (DestructableChunk i in chunks)
        {
			if (i.jointsGone)
            {
				destination = i.transform.position;
				break;
            }
        }

		agent.SetDestination(destination);
	}
}