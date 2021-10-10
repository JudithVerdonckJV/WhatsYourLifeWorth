using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : Interactable
{
    private Animator animator;
    bool isOpened;
    [SerializeField] GameObject storyTriggerChest = null;
    [SerializeField] GameObject storyLineSassy = null;
    [SerializeField] GameObject storyLineFountain = null;
    [SerializeField] GameObject altar = null;

    private Image uiBowImg = null;
    private BasePlayer playerInfo = null;
    private AudioSource audioSource = null;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        uiBowImg = GameObject.Find("UI").transform.Find("Bow").GetComponent<Image>();
        playerInfo = GameObject.Find("Player").GetComponent<BasePlayer>();
    }

    override public void InteractWithObject() 
    {
        if (!isOpened)
        {
            isOpened = true;
            HandleAnimation();
            audioSource.Play();
            storyTriggerChest.SetActive(true);
            storyLineSassy.SetActive(true);
            storyLineFountain.SetActive(true);

            playerInfo.bowPickedUp = true;
            gameObject.transform.Find("InteractableGlow").gameObject.SetActive(false);

            altar.SetActive(true);
        }
    }

    private void HandleAnimation()
    {
        animator.SetBool("Open", true);
        uiBowImg.enabled = true;
    }
}
