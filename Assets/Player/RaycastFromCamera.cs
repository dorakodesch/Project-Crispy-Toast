using UnityEngine;

public class RaycastFromCamera : MonoBehaviour
{
    public Camera playerCamera;

    [SerializeField]
    NPCControl npcControl;

    void Update()
    {
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward,
                out hit);
            Transform objectHit = hit.collider.transform;
            Debug.Log(objectHit.name);
            switch (objectHit.tag)
            {
                case "NPC":
                    break;
            }
        }
    }
}
