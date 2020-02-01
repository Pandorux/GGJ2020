using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{

    public float speed = 5;
    public bool canPickUp = false;
    public GameObject climbPivotPrefab;
    private GameObject climbPivot;
    private Rigidbody rb;
    private int goldCount;

    public PieceEnum pieceNumber;

    [SerializeField]
    private List<Piece> pieces = new List<Piece>();

    public GrabPoint rightGrabPoint;
    public GrabPoint leftGrabPoint;
    public bool isClimbing;
    public bool canClimb
    {
        get
        {
            if (rightGrabPoint.canGrab)
                return true;

            if (leftGrabPoint.canGrab)
                return true;

            return false;
        }

    }

    public int getNumberOfGrabbingPoints
    {
        get 
        {
            int i = 0;

            if(rightGrabPoint.isGrabbing)
                i++;

            if(leftGrabPoint.isGrabbing)
                i++;

            return i;
        }
    }

    public bool isOnGround
    {
        get
        {
            if (rb.velocity.y > 0.25)
                return false;

            if (rb.velocity.y < -0.25)
                return false;

            return true;
        }
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // talk to the rigid body

        // Make sure all pieces point to this as the parent 
        for (int i = 0; i < pieces.Count; i++)
        {
            pieces[i].GetComponent<Child>().ChangeParent(transform);
        }

        GetGrabPointExtents(ref leftGrabPoint, ref rightGrabPoint);
        DeactivateUnusedGrabPoints(); 
    }

    #region Movement

    // Update is called once per frame
    void Update()
    {
        // WASD
        if (isOnGround)
        {
            GroundMovement();
        }
        
        // Wall Swivel Movement
        if(isClimbing)
        {
            ClimbMovement();
        }

        // Mouse0 and Mouse1 Wall Grabbing
        if (isClimbing || canClimb)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && rightGrabPoint.canGrab)
            {
                rightGrabPoint.isGrabbing = !rightGrabPoint.isGrabbing;
                GrabWall();
            }

            if (Input.GetKeyDown(KeyCode.Mouse1) && leftGrabPoint.canGrab)
            {
                leftGrabPoint.isGrabbing = !leftGrabPoint.isGrabbing;
                GrabWall();
            }

            isClimbing = rightGrabPoint.isGrabbing || leftGrabPoint.isGrabbing ?
                true : false;
        }
    }

    protected void GroundMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); // move n shit
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    protected void GrabWall()
    {
        if(getNumberOfGrabbingPoints == 2)
        {
            DestroyClimbPivot();
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        else if(getNumberOfGrabbingPoints == 1)
        {
            CreateClimbPivot();
            rb.constraints = RigidbodyConstraints.None;
        }
        else if(getNumberOfGrabbingPoints == 0)
        {
            DestroyClimbPivot();
            rb.constraints = RigidbodyConstraints.None;
        }
    }

    protected void ClimbMovement()
    {
        if(climbPivot != null)
        {
            if(Input.GetKeyDown(KeyCode.A));
            {
                climbPivot.GetComponent<RigidBody>().AddTorque(-Vector3.forward, ForceMode.Force);
            }

            if(Input.GetKeyDown(KeyCode.D));
            {
                climbPivot.GetComponent<RigidBody>().AddTorque(Vector3.forward, ForceMode.Force);
            }
        }
    }

    
    public void CreateClimbPivot(Vector3 spawnLoc)
    {
        // Spawn Climb Pivot
        GameObject climbPivot = Instantiate(climbPivotPrefab);
        climbPivot.transform.position = spawnLoc;

        // Set Climb Parent as pivot
        gameObject.transform.SetParent(climbPivot.transform);
    }

    public void DestroyClimbPivot()
    {
        gameObject.transform.SetParent(null);
        Destroy(climbPivot);
    }

    #endregion

    #region Broken Pieces & Grab Points

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
        GetGrabPointExtents(ref leftGrabPoint, ref rightGrabPoint);
        DeactivateUnusedGrabPoints(); 
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
            if(grabPoints[i] != leftGrabPoint && grabPoints[i] != rightGrabPoint)
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

                // #if UNITY_EDITOR
                //     Debug.Log($"Compared {grabPoints[i].gameObject.name} and {grabPoints[j].gameObject.name}");
                // #endif
            }
        }
    }

    #endregion

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Gold")) // **GOLD** when you hit gold, they deactivate and add 1 to your count
        {
            other.gameObject.SetActive(false);
            goldCount = goldCount + 1;


        }

        // check if you can pick up (is goldCount = 3?)
        if (goldCount == 3)
        {
            //other.gameObject.SetActive(false); //deactivate piece on the floor
            canPickUp = true;

        }

        //if (other.gameObject.CompareTag("Piece") & canPickUp = true) //AND canPickUp = true         // **PIECES** if the piece you hit is a Piece, check if you can pick up, they deactivate and you reactivate a piece to bowl
        //{
        //    AddPiece(other.g)
        //}
    }


    #region Testing

    //[Header("Testing Variables")]

    //public Piece testPiece00;
    //public Piece testPiece01;
    //public Piece testPiece02;

    ///// <summary>
    ///// Update is called every frame, if the MonoBehaviour is enabled.
    ///// </summary>
    //void Update()
    //{
    //    #if UNITY_EDITOR
    //        // Tests Snapping Functionality 
    //        if (Input.GetKeyDown(KeyCode.Alpha1))
    //        {
    //            AddPiece(testPiece00);
    //            testPiece00.gameObject.SetActive(true);
    //        }
    //        else if (Input.GetKeyDown(KeyCode.Alpha2))
    //        {
    //            AddPiece(testPiece01);
    //            testPiece01.gameObject.SetActive(true);
    //        }
    //        else if (Input.GetKeyDown(KeyCode.Alpha3))
    //        {
    //            AddPiece(testPiece02);
    //            testPiece02.gameObject.SetActive(true);
    //        }   

    //        // Tests if Climbable Objects can be detected
    //        else if (Input.GetKeyDown(KeyCode.C))
    //        {
    //            if(canClimb)
    //                Debug.Log($"{gameObject.name} can climb!!");
    //            else
    //                Debug.LogError($"{gameObject.name} cannot climb!!");
    //        }

    //        else if (Input.GetKeyDown(KeyCode.G))
    //        {
    //            GrabPoint gp1 = new GrabPoint();
    //            GrabPoint gp2 = new GrabPoint();
    //            GetGrabPointExtents(ref gp1, ref gp2);

    //            Debug.Log($"The extents are {gp1.gameObject.name} and {gp2.gameObject.name}");
    //        }
    //    #endif
    //}

    #endregion

}
