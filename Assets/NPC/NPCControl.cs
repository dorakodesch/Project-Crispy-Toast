using UnityEngine;
using System;

public class NPCControl : MonoBehaviour
{
    // cost to upgrade INDEXED BY CURRENT LEVEL
    public resourceConsumption[] levelCosts;

    // public variable for tool type to upgrade for this NPC
    public inventory.tools toUpgrade;

    [SerializeField]
    inventory playerInventory; 

    // upgrade tool based on current level
    public void upgrade()
    {
        Debug.Log("upgrade button pressed");
        int currentLevel = playerInventory.toolLevels[(int)toUpgrade];
        // check for enough resources
        if (checkResources(levelCosts[currentLevel], playerInventory))
        {
            // switch for different possible tools to upgrade
            playerInventory.levelUpTool(toUpgrade);
            // remove resources from player
            removeResources(levelCosts[currentLevel], playerInventory);
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

    // remove resources spent
    void removeResources(resourceConsumption removed, inventory owned)
    {
        foreach(int i in Enum.GetValues(typeof(inventory.resources)))
        {
            owned.resourceCounters[i] -= removed.resources[i];
        }
    }
}

// resources needed for crafting class
[Serializable]
public class resourceConsumption
{
    public int[] resources;
}
