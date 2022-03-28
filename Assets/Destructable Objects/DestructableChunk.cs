using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableChunk : MonoBehaviour
{
    public GameObject[] importantJoints;

    public bool jointsGone = false;
    private List<JoinedObject> connected = new List<JoinedObject>(0);
    public inventory.resources type = inventory.resources.Rope;

    // Start is called before first frame
    private void Start()
    {
        // list all siblings
        DestructableChunk[] sisters = this.transform.parent.GetComponentsInChildren<DestructableChunk>();

        // create list of current shared joints
        List<GameObject> sharedJoints = new List<GameObject>(0);
        foreach (DestructableChunk i in sisters)
        {
            if (i == this)
                continue;
            foreach (GameObject j in i.importantJoints)
            {
                foreach (GameObject k in importantJoints)
                {
                    if(j == k)
                    {
                        sharedJoints.Add(j);
                    }
                }
            }
            // check if there are any shared joints
            if(sharedJoints.Count > 0)
            {
                connected.Add(new JoinedObject(i, sharedJoints));
            }
            sharedJoints = new List<GameObject>(0);
        }

        // rigid links added
        foreach (JoinedObject i in connected)
        {
            i.joint = this.gameObject.AddComponent<FixedJoint>();
            i.joint.connectedBody = i.connectedObject.gameObject.GetComponent<Rigidbody>();
        }

        // link joint objects
        foreach (GameObject i in importantJoints)
        {
            FixedJoint newJoint = this.gameObject.AddComponent<FixedJoint>();
            newJoint.connectedBody = i.gameObject.GetComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // check for still connected
        foreach (JoinedObject i in connected)
        {
            foreach (GameObject j in i.jointsConnecting)
            {
                if(j != null)
                {
                    goto JOINED;
                }
            }
            Destroy(i.joint);
        JOINED:
            continue;
        }

        jointsGone = true;
        // check if joints are gone
        foreach (GameObject i in importantJoints)
        {
            if(i != null)
            {
                jointsGone = false;
            }
        }
    }
}

public class JoinedObject
{
    public DestructableChunk connectedObject;
    public List<GameObject> jointsConnecting;
    public FixedJoint joint;

    public JoinedObject(DestructableChunk connected, List<GameObject> joints)
    {
        this.connectedObject = connected;
        this.jointsConnecting = joints;
    }
}
