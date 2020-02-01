using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabPoint : Child
{
    GameObject pot;
    public bool canGrab
    {
        get;
        private set;
    }

    public event OnGrabPointEventHandler onGrab;
    public void onGrabby(OnGrabPointEventArgs e)
    {
        OnGrabPointEventHandler handler = onGrab;
        if (onGrab != null)
            handler(this, e);
    }

    public event OnGrabPointEventHandler onNotGrab;
    public void onNotGrabby(OnGrabPointEventArgs e)
    {
        OnGrabPointEventHandler handler = onNotGrab;
        if (onNotGrab != null)
            handler(this, e);
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        // TODO: Not sure if I need this yet
        pot = GetAncestor(2);
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

            OnGrabPointEventArgs args = new OnGrabPointEventArgs();
            args.grabPoint = gameObject.GetComponent<GrabPoint>();

            Debug.Log($"{args.grabPoint.gameObject.name} can grab");
            onGrabby(args);


        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Climbable")
        {
            canGrab = false;
        }

        OnGrabPointEventArgs args = new OnGrabPointEventArgs();
        args.grabPoint = gameObject.GetComponent<GrabPoint>();
        onNotGrabby(args);

        Debug.Log($"{gameObject.name} cannot grab");
    }

}

public delegate void OnGrabPointEventHandler(object sender, OnGrabPointEventArgs e);
public class OnGrabPointEventArgs : EventArgs
{
    public GrabPoint grabPoint;
}
