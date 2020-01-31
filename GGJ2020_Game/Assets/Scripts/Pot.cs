using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    [SerializeField]
    private List<Piece> pieces;
    
    public boolean isClimbing;
    public boolean canClimb;

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
    }

    public void AddPiece(Piece newPiece)
    {
        GameObject newPieceObj = newPiece.gameObject;

        // Set Pot as Parent
        newPieceObj.GetComponent<Child>().ChangeParent(transform);

        // Make sure piece is in origin
        newPieceObj.transform.localPosition = Vector3.zero;
        newPieceObj.transform.localRotation = Quaternion.identity;

        pieces.Add(newPiece);
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
    }

    #endregion

}
