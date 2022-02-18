using UnityEngine;

public class RaycastFromCamera : MonoBehaviour
{
    public Camera playerCamera, menuCamera, currentCamera;

    [SerializeField]
    Canvas playerCanvas;

    [SerializeField]
    NPCControl npcControl;

    private void Start()
    {
        currentCamera = playerCamera;
        menuCamera.gameObject.SetActive(false);
    }

    void Update()
    {        
        // checking if player has clicked on stuff
        if (Input.GetMouseButtonDown(0) && currentCamera == playerCamera)
        {
            RaycastHit hit;
            Physics.Raycast(currentCamera.transform.position, currentCamera.transform.forward,
                out hit);
            Transform objectHit = hit.collider.transform;
            Debug.Log(objectHit.name);
            switch (objectHit.tag)
            {
                case "NPC":
                    npcControl.OpenMenu();
                    currentCamera = menuCamera;
                    break;
            }
        }
    }
}
