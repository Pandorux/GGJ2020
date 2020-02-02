using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : Child
{
    [SerializeField]
    private List<GrabPoint> grabPoints;
    private List<GrabPoint> climbyGrabPoints;
    public PieceEnum pieceNumber;
    public bool hasClimbyGrabPoints
    {
        get;
        private set;
    }
    public GameObject brokenPiece;
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

        // Needs to be instantiated other NullPointerExceptions will be thrown
        // GrabPoint events are called
        climbyGrabPoints = new List<GrabPoint>(); 
    }

    protected void AddActiveGrabPoint(object sender, OnGrabPointEventArgs e)
    {
        climbyGrabPoints.Add(e.grabPoint);
        hasClimbyGrabPoints = true;

        // #if UNITY_EDITOR
        //     Debug.Log($"A new grab point can climb!!\n"
        //         + $"{gameObject.name} now has {climbyGrabPoints.Count} grab points active.");
        // #endif
    }

    protected void RemoveActiveGrabPoint(object sender, OnGrabPointEventArgs e)
    {
        climbyGrabPoints.Remove(e.grabPoint);
        hasClimbyGrabPoints = climbyGrabPoints.Count > 0 ? 
            true : false;

        // #if UNITY_EDITOR
        //     Debug.Log($"A grab point can no longer climb!!\n"
        //         + $"{gameObject.name} now has {climbyGrabPoints.Count} grab points active.");
        // #endif
    }

    public List<GrabPoint> GetGrabPoints()
    {
        return grabPoints;
    }

    public void SpawnBrokenPiece()
    {
        // Deactivate Active Piece
        gameObject.SetActive(false);

        // Spawn Broken Piece Equivalent in World Space
        Vector3 worldSpace = transform.TransformPoint(transform.localPosition);
        Instantiate(brokenPiece, worldSpace, transform.rotation);
    }
}
