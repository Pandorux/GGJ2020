using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : Child
{
    [SerializeField]
    private List<GrabPoint> grabPoints;

    private List<GrabPoint> climbyGrabPoints;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        
        for(int i = 0; i < grabPoints.Count; i++)
        {
            // Make sure all grab points point to this as the parent 
            grabPoints[i].GetComponent<Child>().ChangeParent(transform);

            grabPoints[i].onGrab += new OnGrabPointEventHandler(AddActiveGrabPoint);
            grabPoints[i].onNotGrab += new OnGrabPointEventHandler(RemoveActiveGrabPoint);
        }
    }

    protected void AddActiveGrabPoint(object sender, OnGrabPointEventArgs e)
    {
        climbyGrabPoints.Add(e.grabPoint);
    }

    protected void RemoveActiveGrabPoint(object sender, OnGrabPointEventArgs e)
    {
        climbyGrabPoints.Remove(e.grabPoint);
    }

    
}
