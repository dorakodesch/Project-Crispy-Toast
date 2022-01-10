using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class RobotNavigation : MonoBehaviour
{
	[SerializeField]
	movement player;

	private void Update()
	{
		NavMeshAgent agent = GetComponent<NavMeshAgent>();
		agent.destination = player.gameObject.transform.position;

		Debug.Log(agent.destination);
	}
}
