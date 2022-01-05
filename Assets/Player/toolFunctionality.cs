using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toolFunctionality : MonoBehaviour
{
    public enum objects {none, grapple};
    public objects inHand;
    private Ray forward;
    // Start is called before the first frame update
    void Start()
    {
        inHand = objects.none;
    }

    // Update is called once per frame
    void Update()
    {
        Ray forward = new Ray(this.transform.position, this.transform.GetChild(0).transform.forward);
        if (Input.GetButtonDown("Attack"))
            selectAttack();
        if (Input.GetButtonDown("Aim"))
            selectAim();
    }

    // Select fire function
    void selectAttack()
    {
        if (inHand == objects.none)
            return;
        if (inHand == objects.grapple)
        {
            this.GetComponent<grapple>().fire(forward);
            return;
        }
    }

    // Select aim function
    void selectAim()
    {
        if (inHand == objects.none)
            return;
        if (inHand == objects.grapple)
            return;
    }
}
