/*// npc upgrades frontend
using UnityEngine;
using TMPro;

public class NPCMenu : MonoBehaviour
{
    // menu visuals
    [SerializeField]
    TextMeshProUGUI toolName, upgradeCosts;

    // reference for which tool to upgrade
    [SerializeField]
    NPCControl npcControl;

    [SerializeField]
    Transform player;

    [SerializeField]
    Canvas menuCanvas;

    private void Start()
    {
        menuCanvas.gameObject.SetActive(false);
    }

    // update for test purposes and also pretty sure this is how it'll work anyway
    private void Update()
    {
        inventory playerInventory = player.GetComponent<inventory>();
        inventory.tools toolToUpgrade = npcControl.toUpgrade;

        // setting all text

		// tool name at the top
        toolName.text = playerInventory.toolNames[(int)toolToUpgrade];
        toolName.alignment = TextAlignmentOptions.Center;

		// getting material costs
        resourceConsumption costs = 
            npcControl.levelCosts[playerInventory.toolLevels[(int)toolToUpgrade]];

		// updating cost display for each material
        upgradeCosts.text = costs.resources[0].ToString();
        for (int i = 1; i < costs.resources.Length; i++)
        {
            // 8 spaces between resources idk why but it works
			// probably should make this modular at some point
            upgradeCosts.text =
                upgradeCosts.text + "        " + costs.resources[i].ToString();
        }
    }
}
*/