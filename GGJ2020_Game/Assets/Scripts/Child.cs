using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Child : MonoBehaviour
{
    [SerializeField]
    private GameObject parent;

    public GameObject GetParent()
    {
        return parent;
    }

    public void Unparent()
    {
        transform.SetParent(null);
    }

    public void ChangeParent(Transform parentTransform) 
    {
        transform.SetParent(parentTransform);
    }

}
