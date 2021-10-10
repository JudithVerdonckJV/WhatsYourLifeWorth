using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BowTalkMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI changeableInfoText = null;

    public void SetStoryText(ref string newText)
    {
        changeableInfoText.text = newText;
    }

    public void ClosePopup()
    {
        Time.timeScale = 1f;
    }
}
