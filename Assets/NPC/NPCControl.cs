using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NPCControl : MonoBehaviour
{
    // cost to upgrade INDEX IS FOR CURRENT LEVEL, NOT LEVEL UPGRADED TO
    public resourceConsumption[] levelCosts;

    public void upgrade(int currentLevel, inventory.tools toUpgrade, inventory playerInventory)
    {
        if (checkResources(levelCosts[currentLevel], playerInventory))
        {
            switch(toUpgrade)
            {
                case inventory.tools.batteryMaker:
                    playerInventory.battteryMakerLevel++;
                    break;
                case inventory.tools.grapple:
                    playerInventory.grappleLevel++;
                    break;
                case inventory.tools.laser:
                    playerInventory.laserLevel++;
                    break;
            }
        }
    }

    // check if resources are adequate
    public bool checkResources(resourceConsumption needed, inventory owned)
    {
        // iterate through resources checking if needed exceeds owned
        foreach(int i in Enum.GetValues(typeof(inventory.resources)))
        {
            if (owned.resourceCounters[i] < needed.resources[i])
                return false;
        }
        return true;
    }
}

// resources needed for crafting class
public class resourceConsumption
{
    public int[] resources;

    public resourceConsumption()
    {
        resources = new int[7] { 0, 0, 0, 0, 0, 0, 0 };
    }

}
