using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    [SerializeField]
    private Piece[] pieces;
    
    public void AddPiece(Piece newPiece)
    {
        GameObject newPieceObj = newPiece.gameObject;

        // Set Pot as Parent
        newPieceObj.transform.SetParent(transform);
        newPieceObj.GetComponent<Child>().ChangeParent(transform);

        // Make sure piece is in origin
        newPieceObj.transform.localPosition = Vector3.zero;
        newPieceObj.transform.localRotation = Quaternion.identity;
        
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
