// A* robot target (easier to set its position than have the robot move to a set position)
using UnityEngine;

public class RobotDestination : MonoBehaviour
{
	[SerializeField]
	Transform player;

	// int in case we need >2 states later
	int state;

	DestructableChunk[] chunks;

	private void Start()
	{
		// following player at the beginning
		state = 0;
	}

	private void Update()
	{
		UpdateState();

		// following player
		if (state == 0)
		{
			transform.position = player.position;
		}
		// following chunk
		else
		{

		}
	}

	private void UpdateState()
	{
		state = 0;
		chunks = FindObjectsOfType<DestructableChunk>();

		foreach (DestructableChunk i in chunks)
		{
			if (i.jointsGone)
			{
				state = 1;
				break;
			}
		}
	}
}
