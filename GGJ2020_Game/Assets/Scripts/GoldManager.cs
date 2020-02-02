using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public GoldContainer[] goldContainers;
    public GoldContainer activeGoldContainer;

    public /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        ActivateGoldContainer(1);
    }

    public void ActivateGoldContainer(int pieceId)
    {   
        // Deactivate currently active container
        if(activeGoldContainer != null)
            activeGoldContainer.gameObject.SetActive(false);

        // Activate new container
        activeGoldContainer = goldContainers[pieceId - 1];
        ActivateGoldPickUps();
    }   

    public void ActivateGoldPickUps()
    {
        for(int i = 0; i < activeGoldContainer.goldPickups.Length; i++)
        {
            activeGoldContainer.goldPickups[i].SetActive(true);
        }
    }
}
