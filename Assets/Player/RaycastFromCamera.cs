using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastFromCamera : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;

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
                if (objectHit.tag == "Joint")
                {
                    Destroy(objectHit.gameObject);
                }
            }
            
            Debug.Log("Ray has been cast");
        }
    }
}
