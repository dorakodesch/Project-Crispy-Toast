using UnityEngine;
using System;

public class NPCControl : MonoBehaviour
{
    // cost to upgrade INDEXED BY CURRENT LEVEL
    public resourceConsumption[] levelCosts;

    // public variable for tool type to upgrade for this NPC
    public inventory.tools toUpgrade;

    // upgrade tool based on current level
    public void upgrade(int currentLevel, inventory playerInventory)
    {
        // check for enough resources
        if (checkResources(levelCosts[currentLevel], playerInventory))
        {
            // switch for different possible tools to upgrade
            switch(toUpgrade)
            {
                case inventory.tools.batteryMaker:
                    playerInventory.batteryMakerLevelUp();
                    break;
                case inventory.tools.grapple:
                    playerInventory.grappleLevelUp();
                    break;
                case inventory.tools.laser:
                    playerInventory.laserLevelUp();
                    break;
            }
            // remove resources from player
            removeResources(levelCosts[currentLevel], playerInventory);
        }
    }

    // check if resources are adequate
    bool checkResources(resourceConsumption needed, inventory owned)
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
    [HideInInspector]
    public int[] resources;
    public int rope;
    public int scrap;
    public int wire;
    public int glass;
    public int batteries;
    public int crystals;
    public int lithium;

    public resourceConsumption()
    {
        resources = new int[7] { rope, scrap, wire, glass, batteries, crystals, lithium };
    }

}
