using UnityEngine;

public class hammer : MonoBehaviour
{
    // Create variable for player camera
    [SerializeField]
    private Camera playerCamera;

    // Return null from aiming
    public void Aim(Ray forward, Vector3 position, Quaternion rotation, bool down)
    {
        return;
    }

    // Destroy objects from hammer hit with fire
    public void Fire(Ray forward, Vector3 position, Quaternion rotation)
    {
        RaycastHit hit;
        Physics.Raycast(forward, out hit, 1000.0f);
        Transform objectHit = hit.collider.transform;
        if (objectHit != null)
        {
            if (objectHit.tag == "Joint")
            {
                Destroy(objectHit.gameObject);
            }
        }
    }

    // Return null from instantiation
    public void Inst(Vector3 position, Quaternion rotation)
    {
        return;
    }
}
