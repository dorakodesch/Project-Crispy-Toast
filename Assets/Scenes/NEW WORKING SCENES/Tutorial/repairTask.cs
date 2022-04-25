/* Script by Kaleb that keeps track of the repair task for the tutorial level. 
 * In order to complete the task you must find various parts around the level then return to the robot to repair it.
 * 
 * Assign this script to the broken robot object.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class repairTask : MonoBehaviour
{
    [SerializeField] private GameObject[] taskObjects;
    private bool partsGathered = false;
    [SerializeField] private bool isTaskObject = false; // True if item needs to be collected to complete the task, false if the object is what is being repaired.

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTaskObject)
        {
            if (!partsGathered) // If not all the parts are gathered then check if they have been gathered
            {
                for (int ii = 0; ii < taskObjects.Length; ii++)
                {
                    if (taskObjects[ii] == null)
                    {
                        partsGathered = true;
                        Debug.Log("Parts collected");
                    }
                }                
            }
            if (partsGathered && Interact())
            {
                RepairRobot();
            }
        }
        

        if (isTaskObject && Interact()) // Functionality for collecting repair part
        {
            Destroy(gameObject);
        }
    }

    void RepairRobot() // Instantiates the bot and removes broken bot object
    {
        // Instantiate robot prefab here
        Destroy(gameObject);
        Debug.Log("Robot repaired.");
    }

    bool Interact() // Checks if player is withing interaction distance and for user input, returns true if both happen
    {
        bool interacted = false;

        float dist = Vector3.Distance(player.transform.position, transform.position);
        if (Input.GetKeyDown(KeyCode.E) && dist < 3)
        {
            interacted = true;
        }

        return interacted;
    }
}
