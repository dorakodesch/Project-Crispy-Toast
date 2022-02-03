using UnityEngine;

public class MousePosSymbol : MonoBehaviour
{
	private void Update()
	{
		transform.position = Event.current.mousePosition;
	}
}
