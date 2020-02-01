using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotController : MonoBehaviour
{
    public float speed = 5;
    public bool canPickUp = false;
   
    private Rigidbody rb;
    private int goldCount;

    public PieceEnum pieceNumber; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // talk to the rigid body
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); // move n shit
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    //// reactivate the corresponding piece on the bowl
    //public void AddPiece(Piece newPiece)
    //{
    //    GameObject newPieceObj = newPiece.gameObject;

    //    // Set Pot as Parent
    //    newPieceObj.GetComponent<Child>().ChangeParent(transform);

    //    // Make sure piece is in origin
    //    newPieceObj.transform.localPosition = Vector3.zero;
    //    newPieceObj.transform.localRotation = Quaternion.identity;

    //    //pieces.Add(newPiece);
    //}

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
}
