using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pot))]
public class Piece : Child
{
    [SerializeField]
    private List<GrabPoint> grabPoints;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        // Make sure all grab points point to this as the parent 
        for(int i = 0; i < grabPoints.Count; i++)
        {
            grabPoints[i].GetComponent<Child>().ChangeParent(transform);
        }
    }
}
