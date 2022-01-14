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

	NavMeshAgent agent;

	private void Start()
	{
		agent = GetComponent<NavMeshAgent>();
	}

	private void Update()
	{
		// multiplying by 1.1 corrects for cube size to avoid collisions
		agent.SetDestination(player.position + Vector3.right * distanceFromPlayer);
	}
}
