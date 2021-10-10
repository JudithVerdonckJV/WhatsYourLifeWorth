using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storyline : MonoBehaviour
{
    static private GameObject menu;
    [SerializeField] [TextArea] private string storyPart;
    private AudioSource audioSource = null;
    private BasePlayer playerInfo = null;

    private void Awake()
    {
        audioSource = GetComponentInParent<AudioSource>();
    }

    private void Start()
    {
        menu = GameObject.Find("MenuSystem").transform.Find("BowTextPopup").gameObject;
        playerInfo = GameObject.Find("Player").GetComponent<BasePlayer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (audioSource == null || playerInfo == null) return;
        
        if (other.gameObject.tag == "Player")
        {
            menu.SetActive(true);
            playerInfo.bowActive = false;
            audioSource.Play();
            Time.timeScale = 0f;
            menu.GetComponent<BowTalkMenu>().SetStoryText(ref storyPart);
            gameObject.SetActive(false);
        }
    }
}
