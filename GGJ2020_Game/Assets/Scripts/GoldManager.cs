using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public GameObject[] goldContainers;
    public GameObject activeGoldContainer;

    public void ActivateGoldContainer(int pieceId)
    {   
        // Deactivate currently active container
        activeGoldContainer.SetActive(false);

        // Activate new container
        activeGoldContainer = goldContainers[pieceId - 1];
        ActivateGoldPickUps();
    }   

    public void ActivateGoldPickUps()
    {
        GameObject[] goldPickups = activeGoldContainer.GetComponentsInChildren<GameObject>();

        for(int i = 0; i < goldPickups.Length; i++)
        {
            goldPickups[i].SetActive(true);
        }
    }
}
