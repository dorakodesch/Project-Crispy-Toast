using UnityEngine;

public class TextBoxCollider : MonoBehaviour
{
	public TextPromptCreator creator;

	public int index;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			creator.OnPlayerEnter(index);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			creator.OnPlayerExit(index);
		}
	}
}
