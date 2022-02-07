using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableChunk : MonoBehaviour
{
    [SerializeField] private GameObject[] importantJoints;

    private bool jointsGone = true;
    private Rigidbody chunkRB;
    
    // Update is called once per frame
    void Update()
    {
        jointsGone = true;

        for (int ii = 0; ii < importantJoints.Length; ii++)
        {
            if (importantJoints[ii] != null)
            {
                jointsGone = false;
            }
        }
        if (jointsGone && gameObject.transform.parent != null)
        {
            gameObject.transform.parent = null;
            chunkRB = gameObject.AddComponent<Rigidbody>();
        }
    }
}
