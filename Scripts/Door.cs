using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : Interactable
{
    private BasePlayer playerInfo = null;

    private void Start()
    {
        GameObject player = GameObject.Find("Player");
        playerInfo = player.GetComponent<BasePlayer>();
    }

    override public void InteractWithObject()
    {
        playerInfo.PlayerNextLevel();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
