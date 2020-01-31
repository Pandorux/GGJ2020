using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Piece))]
public class GrabPoint : Child
{
    GameObject pot;
    public boolean canGrab
    {
        get;
        private set;
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        godObject = GetAncestor(2);
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Climbable")
        {
            canGrab = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Climbable")
        {
            canGrab = false;
        }
    }

}
