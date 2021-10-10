using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AltarMenu : MonoBehaviour
{
    private GameObject player = null;
    private Altar usedAltar = null;
    [SerializeField] private TextMeshProUGUI changeableInfoText = null;


    private void Start()
    {
        player = GameObject.Find("Player");
    }

    public void SetUsedAltar(Altar altar)
    {
        usedAltar = altar;
    }

    public void UpgradeBow()
    {
        if (changeableInfoText == null || player == null || usedAltar == null) return;

        player.GetComponent<BasePlayer>().LevelUp();
        
        switch (player.GetComponent<BasePlayer>().level)
        {
            case 2:
                changeableInfoText.text = "Your bow got faster!";
                break;
            case 3:
                changeableInfoText.text = "You got a new skill! (right mousebutton)";
                break;
            case 4:
                changeableInfoText.text = "You are doing more damage!";
                break;
        }

        player.GetComponent<Health>().Damage(2);
        usedAltar.DeactivateAltar();
    }

    public void CloseUpgradeWindow()
    {
        if (usedAltar != null) usedAltar.SpawnBoss();
    }

    public void HealPlayer()
    {
        if (player != null) player.GetComponent<Health>().SetToMax();
        if (usedAltar != null) usedAltar.DeactivateAltar();
    }

    public void CloseMenu()
    {
        Time.timeScale = 1f;
    }
}
