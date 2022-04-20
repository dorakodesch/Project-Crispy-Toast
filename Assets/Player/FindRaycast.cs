using UnityEngine;

public class FindRaycast : MonoBehaviour
{
    [SerializeField]
    RaycastFromCamera raycastFrom;

    private void Start()
    {
        raycastFrom = FindObjectOfType<RaycastFromCamera>();

        Debug.Log("name: " + raycastFrom.gameObject.name);
    }
}
