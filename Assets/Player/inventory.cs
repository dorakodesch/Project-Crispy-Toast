using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class inventory : MonoBehaviour
{
    // create resource counters
    [HideInInspector]
    public enum resources { Rope, Scrap, Wire, Glass, Batteries, Crystals, Lithium }
    [HideInInspector]
    public int[] resourceCounters = new int[7];

    // create tool types enum
    [HideInInspector]
    public enum tools { laser, grapple, batteryMaker }

    [HideInInspector]
    public string[] toolNames = { "Laser", "Grapple", "Battery Maker" };

    // create tool levels
    [HideInInspector]
    public int[] toolLevels = { 0, 0, 0 };

    // TMP_Text objects for values
    [SerializeField]
    private TMP_Text rope;
    [SerializeField]
    private TMP_Text scrap;
    [SerializeField]
    private TMP_Text wire;
    [SerializeField]
    private TMP_Text glass;
    [SerializeField]
    private TMP_Text batteries;
    [SerializeField]
    private TMP_Text crystals;
    [SerializeField]
    private TMP_Text lithium;

    private void Update()
    {
        for(int i = 0; i < resourceCounters.Length; i++)
        {
            resources j = (resources)i;
            TMP_Text txt = GameObject.Find(j.ToString()).GetComponent<TMP_Text>();
            txt.text = resourceCounters[(int)i].ToString();
        }
    }

    // increment function
    public void changeResourceCount(resources type, int increment = 1)
    {
        resourceCounters[(int)type] += increment;
    }

    // tool level up functions
    public void levelUpTool(tools tool)
    {
        toolLevels[(int)tool]++;
    }
}
