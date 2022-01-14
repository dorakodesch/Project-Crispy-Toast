using UnityEngine;
using UnityEngine.Events;

public class ToolMaster : MonoBehaviour
{
    // Create unity events for function calls
    public UnityEvent<Ray, Vector3, Quaternion, bool> aimFunc;
    public UnityEvent<Ray, Vector3, Quaternion> fireFunc;
    public UnityEvent<Vector3, Quaternion> instFunc;

    // Create transform for tool attach point
    [SerializeField]
    private Transform toolAttach;

    // Awake is called when the object is instantiated
    private void Awake()
    {
        instFunc.Invoke(this.transform.position, this.transform.rotation);
    }

    // Fire is called from parent object
    public void Fire(Ray forward)
    {
        fireFunc.Invoke(forward, this.transform.position, this.transform.rotation);
    }

    // Aim is called froom parent object
    public void Aim(Ray forward, bool down)
    {
        aimFunc.Invoke(forward, this.transform.position, this.transform.rotation, down);
    }
}
