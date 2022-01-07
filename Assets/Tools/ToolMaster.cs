using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ToolMaster : MonoBehaviour
{
    // Fire is called from parent object
    public virtual void Fire(Ray forward)
    {

    }

    // Aim is called froom parent object
    public virtual void Aim(Ray forward)
    {

    }
}
