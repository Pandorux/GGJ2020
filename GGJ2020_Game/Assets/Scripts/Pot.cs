using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PotController))]
public class Pot : MonoBehaviour
{
    public PotController controller; 
    [SerializeField]
    private List<Piece> pieces;   
    public bool canClimb
    {
        get
        {
            for(int i = 0; i < pieces.Count; i++)
            {
                if(pieces[i].hasClimbyGrabPoints)
                    return true;
            }

            return false;
        }
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        // Make sure all pieces point to this as the parent 
        for(int i = 0; i < pieces.Count; i++)
        {
            pieces[i].GetComponent<Child>().ChangeParent(transform);
        }

        controller.GetComponent<PotController>();
        GetGrabPointExtents(ref controller.leftGrabPoint, ref controller.rightGrabPoint);
        DeactivateUnusedGrabPoints(); 
    }

    public void AddPiece(Piece newPiece)
    {
        GameObject newPieceObj = newPiece.gameObject;

        // Set Pot as Parent
        newPieceObj.GetComponent<Child>().ChangeParent(transform);

        // Make sure piece is in origin
        newPieceObj.transform.localPosition = Vector3.zero;
        newPieceObj.transform.localRotation = Quaternion.identity;

        // Add new piece, and reasign grab points
        pieces.Add(newPiece);
        GetGrabPointExtents(ref controller.leftGrabPoint, ref controller.rightGrabPoint);
        DeactivateUnusedGrabPoints(); 
    }

    public void GetGrabPointExtents(ref GrabPoint grabPoint00, ref GrabPoint grabPoint01)
    {
        List<GrabPoint> grabPoints = GetAllGrabPoints();
        float longestDistance = 0.0f;

        for(int i = 0; i < grabPoints.Count; i++)
        {
            for(int j = i + 1; j < grabPoints.Count; j++)
            {
                Vector3 a = grabPoints[i].gameObject.transform.position;
                Vector3 b = grabPoints[j].gameObject.transform.position;

                float distance = Vector3.Distance(a, b);

                if(distance > longestDistance)
                {
                    longestDistance = distance;

                    grabPoint00 = grabPoints[i];
                    grabPoint01 = grabPoints[j];
                }

                #if UNITY_EDITOR
                    Debug.Log($"Compared {grabPoints[i].gameObject.name} and {grabPoints[j].gameObject.name}");
                #endif
            }
        }
    }


    // TODO:  Potential cause of poor performance
    /// <summary>
    /// Deactivates all grab points that are not currently being used for climbing movement.
    /// </summary>
    public void DeactivateUnusedGrabPoints()
    {
        List<GrabPoint> grabPoints = GetAllGrabPoints();

        for(int i = 0; i < grabPoints.Count; i++)
        {
            if(grabPoints[i] != controller.leftGrabPoint && grabPoints[i] != controller.rightGrabPoint)
            {
                grabPoints[i].gameObject.SetActive(false);
            }
            else
            {
                grabPoints[i].gameObject.SetActive(true);
            }
        }
    }

    public List<GrabPoint> GetAllGrabPoints()
    {
        List<GrabPoint> grabPoints = new List<GrabPoint>();

        for(int i = 0; i < pieces.Count; i++)
        {
            grabPoints.AddRange(pieces[i].GetGrabPoints());
        }

        return grabPoints;
    }

    #region Testing

    [Header("Testing Variables")]

    public Piece testPiece00;
    public Piece testPiece01;
    public Piece testPiece02;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        #if UNITY_EDITOR
            // Tests Snapping Functionality 
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                AddPiece(testPiece00);
                testPiece00.gameObject.SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                AddPiece(testPiece01);
                testPiece01.gameObject.SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                AddPiece(testPiece02);
                testPiece02.gameObject.SetActive(true);
            }   

            // Tests if Climbable Objects can be detected
            else if (Input.GetKeyDown(KeyCode.C))
            {
                if(canClimb)
                    Debug.Log($"{gameObject.name} can climb!!");
                else
                    Debug.LogError($"{gameObject.name} cannot climb!!");
            }

            else if (Input.GetKeyDown(KeyCode.G))
            {
                GrabPoint gp1 = new GrabPoint();
                GrabPoint gp2 = new GrabPoint();
                GetGrabPointExtents(ref gp1, ref gp2);

                Debug.Log($"The extents are {gp1.gameObject.name} and {gp2.gameObject.name}");
            }
        #endif
    }

    #endregion

}
