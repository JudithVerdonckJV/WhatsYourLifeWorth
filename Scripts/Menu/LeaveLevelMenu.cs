using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveLevelMenu : MonoBehaviour
{
    private GameObject leaveLevel = null;
    private BasePlayer playerInfo = null;

    private void Start()
    {
        leaveLevel = transform.Find("LeaveLevel").gameObject;
        playerInfo = GameObject.Find("Player").GetComponent<BasePlayer>();
    }

    void Update()
    {
        if (leaveLevel == null || playerInfo == null) return;
        if (Input.GetButtonDown("Escape"))
        {
            leaveLevel.SetActive(true);
            playerInfo.bowActive = false;
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("QUIT");
    }
}
