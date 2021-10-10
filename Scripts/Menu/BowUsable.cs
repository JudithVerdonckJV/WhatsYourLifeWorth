using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BowUsable : MonoBehaviour
{
    private Image bowImg = null;
    [SerializeField] Image crossImg = null;

    private void Start()
    {
        bowImg = GetComponent<Image>();
    }

    public void SetUsable()
    {
        if (bowImg == null || crossImg == null) return;
        
        bowImg.color = new Color(1f, 1f, 1f, 1f);
        crossImg.enabled = false;
        crossImg.color = new Color(1f, 1f, 1f, 1f);
    }

    public void SetUnusable()
    {
        if (bowImg == null || crossImg == null) return;

        bowImg.color = new Color(1f, 1f, 1f, 0.3f);
        crossImg.enabled = true;
        crossImg.color = new Color(1f, 1f, 1f, 0.3f);
    }
}
