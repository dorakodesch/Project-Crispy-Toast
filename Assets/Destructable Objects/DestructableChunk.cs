using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableChunk : MonoBehaviour
{
    public GameObject[] importantJoints;

    public bool jointsGone = false;
    private List<GameObject> connected;

    // Update is called once per frame
    void Update()
    {
        // set connected to null
        connected = new List<GameObject>(0);

        // get all siblings and add connected ones to list
        DestructableChunk[] siblings = this.gameObject.GetComponentsInChildren<DestructableChunk>();
        foreach (DestructableChunk i in siblings)
        {
            // iterate important joints
            foreach (GameObject j in i.importantJoints)
            {
                // cross compare lists of important joints
                foreach (GameObject k in importantJoints)
                {
                    if (j == k)
                    {
                        connected.Add(i.GetComponent<GameObject>());
                        goto NEW_CHUNK;
                    }
                }
            }
            NEW_CHUNK:
                continue;
        }

        // set joints gone to true if there are no joints
        jointsGone = true;
        foreach(GameObject i in importantJoints)
        {
            if(i != null)
            {
                jointsGone = false;
            }
        }

        // remove fixed joints with non connected objects
        FixedJoint[] joints = this.gameObject.GetComponents<FixedJoint>();
        foreach (FixedJoint i in joints)
        {
            // check for unnecessary fixed joints
            foreach (GameObject j in connected)
            {
                if (j == i.connectedBody)
                {
                    goto GOODJOINT;
                }
            }
            // destroy unused joints
            Destroy(i);
            // continue if joint is in use
            GOODJOINT:
                continue;
        }

        // add new joints for non created joints
        foreach (GameObject i in connected)
        {
            foreach (FixedJoint j in joints)
            {
                if (i == j.connectedBody)
                {
                    goto EXISTINGJOINT;
                }
            }
            Rigidbody connectedRB = i.GetComponent<Rigidbody>();
            // create new joint if not continued
            FixedJoint newJoint = this.gameObject.AddComponent(typeof(FixedJoint)) as FixedJoint;
            newJoint.connectedBody = connectedRB;
            // continue if joint already exists
            EXISTINGJOINT:
                continue;
        }
    }
}
