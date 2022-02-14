using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory : MonoBehaviour
{
    // create resource counters
    [HideInInspector]
    public enum resources { rope, scrap, wire, glass, batteries, crystals, lithium }
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
