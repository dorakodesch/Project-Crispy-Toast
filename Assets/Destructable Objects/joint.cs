using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class joint : MonoBehaviour
{
    public GameObject breakParticles;

    private void OnDestroy()
    {
        //Trigger particle system
        Instantiate(breakParticles, transform.position, Quaternion.identity);
    }
}
