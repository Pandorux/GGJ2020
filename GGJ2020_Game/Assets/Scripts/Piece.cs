using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pot))]
public class Piece : Child
{
    [SerializeField]
    private Piece[] pieces;
}
