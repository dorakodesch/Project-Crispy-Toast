using UnityEngine;

public class ResourceSense : MonoBehaviour
{
	[SerializeField, Min(0f)]
	float minDist = 5f; // minimum distance for resource checks

	Collider[] colliders; // colliders grabbed by physics.overlapsphere

	private void Update()
	{
		// physics.overlapsphere grabs all colliders within the sphere defined by (center, radius)
		colliders = Physics.OverlapSphere(transform.position, minDist);

		// checking for object chunks
		for (int i = 0; i < colliders.Length; i++)
		{
			Collider collider = colliders[i];
			if (collider.gameObject.GetComponent<DestructableChunk>())
			{
				if (collider.gameObject.GetComponent<Rigidbody>())
				{
					Destroy(collider.gameObject);
				}
			}
		}
	}
}
