// all npc upgrade stuff
using UnityEngine;
using System;
using TMPro;

public class NPCControl : MonoBehaviour
{
    // cost to upgrade INDEXED BY CURRENT LEVEL
    public resourceConsumption[] levelCosts;

    // public variable for tool type to upgrade for this NPC
    public inventory.tools toUpgrade;

    [SerializeField]
    inventory playerInventory;

    // menu visuals
    [SerializeField]
    TextMeshProUGUI toolName, upgradeCosts;

    [SerializeField]
    Transform player;

    [SerializeField]
    Canvas menuCanvas;

    [SerializeField]
    Canvas playerCanvas;

    public bool menuOpen;

    private void Start()
    {
        menuCanvas.gameObject.SetActive(false);
        menuOpen = false;
    }

    private void Update()
    {
        if (menuCanvas.gameObject.activeSelf)
        {
            UpdateMenu();
        }
    }

    private void UpdateMenu()
    {
        inventory.tools toolToUpgrade = toUpgrade;

        // setting all texts
        toolName.text = playerInventory.toolNames[(int)toolToUpgrade];
        toolName.alignment = TextAlignmentOptions.Center;

        resourceConsumption costs =
            levelCosts[playerInventory.toolLevels[(int)toolToUpgrade]];

        upgradeCosts.text = costs.resources[0].ToString();

        for (int i = 1; i < costs.resources.Length; i++)
        {
            // 8 spaces between resources idk why but it works
            upgradeCosts.text =
                upgradeCosts.text + "        " + costs.resources[i].ToString();
        }
    }

    // upgrade tool based on current level
    public void upgrade()
    {
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

    public void OpenMenu()
    {
        menuCanvas.gameObject.SetActive(true);
        Time.timeScale = 0f;
        playerCanvas.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        menuOpen = true;
    }

    public void CloseMenu()
    {
        menuCanvas.gameObject.SetActive(false);
        Time.timeScale = 1f;
        playerCanvas.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        menuOpen = false;
    }
}

// resources needed for crafting class
[Serializable]
public class resourceConsumption
{
    public int[] resources;
}
