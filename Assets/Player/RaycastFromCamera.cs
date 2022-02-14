using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastFromCamera : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;

    [SerializeField]
    NPCControl npcControl;

    private void Start()
    {
        Debug.Log(name);
    }

    void Update()
    {
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 1000.0f);
            Transform objectHit = hit.collider.transform;
            if (objectHit != null)
            {
                Debug.Log(objectHit.name);
                /*if (objectHit.tag == "Joint")
                {
                    Destroy(objectHit.gameObject);
                }*/
                switch (objectHit.tag)
                {
                    case "Joint":
                        Destroy(objectHit.gameObject);
                        break;
                    case "NPC":
                        npcControl.OpenMenu();
                        playerCamera.gameObject.SetActive(false);
                        break;
                }
            }
            
            Debug.Log("Ray has been cast");
        }
    }
}
