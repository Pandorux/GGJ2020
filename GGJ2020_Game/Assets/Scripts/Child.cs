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
        parent = null;
    }

    public void ChangeParent(Transform parentTransform) 
    {
        transform.SetParent(parentTransform);
        parent = parentTransform.gameObject;
    }

    public GameObject GetAncestor(int steps)
    {
        GameObject ancestor = parent;

        if(steps > 1)
        {
            for(int i = 0; i < steps - 1; i++)
            {
                if(ancestor.parent.GetComponent<Child>())
                {
                    ancestor = ancestor.parent.GameObject;
                }
                else
                {
                    ancestor = null;
                    break;
                }
            }
        }

        return ancestor;
    }

}
