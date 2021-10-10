using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableBow : MonoBehaviour
{
    private BasePlayer playerInfo = null;
    
    public void Start()
    {
        playerInfo = GameObject.Find("Player").GetComponent<BasePlayer>();
    }

    public void DisableBowInMenu()
    {
        if (playerInfo == null) return;
        playerInfo.bowActive = false;
    }

    public void EnableBow()
    {
        if (playerInfo == null) return;
        playerInfo.bowActive = true;
    }
}
