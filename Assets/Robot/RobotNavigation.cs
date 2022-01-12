// robot movement. currently just follows the player around
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class RobotNavigation : MonoBehaviour
{
	[SerializeField]
	Transform player;

	NavMeshAgent agent;

	// grabbing any references + other setup
	private void Start()
	{
		agent = GetComponent<NavMeshAgent>();
	}

	//moving around
	private void Update()
	{
		// multiplying by 1.1 corrects for cube size to avoid collisions
		agent.SetDestination(player.position + Vector3.right * 1.1f);
	}
}
