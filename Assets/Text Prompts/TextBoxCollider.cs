using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TextBoxCollider : MonoBehaviour
{
    public TextPromptCreator promptCreator;

    public int index;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            promptCreator.CollisionDetected(index);
        }
    }
}
