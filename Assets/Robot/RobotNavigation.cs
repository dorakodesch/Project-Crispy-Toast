using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class RobotNavigation : MonoBehaviour
{
	[SerializeField]
	Transform player;

	NavMeshAgent agent;

	private void Start()
	{
		agent = GetComponent<NavMeshAgent>();
	}

	private void Update()
	{
		agent.SetDestination(player.position + Vector3.right * 1.1f);
	}
}
