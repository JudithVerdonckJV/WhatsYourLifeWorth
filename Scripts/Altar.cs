using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Altar : Interactable
{
    [SerializeField] GameObject boss = null;

    [SerializeField] GameObject altarMenu = null;
    private BasePlayer playerInfo = null;

    private Tilemap belowChar = null;
    [SerializeField] TileBase blueAltarTop = null;
    [SerializeField] Vector3Int blueAltarTopPos = new Vector3Int(0, 0, 0);
    [SerializeField] TileBase blueAltarBottom = null;

    [SerializeField] RoomIndexManagment roomInfo = null;

    private AudioSource audioSource = null;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        belowChar = GameObject.Find("Grid").transform.Find("WallsBelowChar").GetComponent<Tilemap>();
        playerInfo = GameObject.Find("Player").GetComponent<BasePlayer>();
    }

    override public void InteractWithObject()
    {
        Time.timeScale = 0f;

        if (altarMenu == null || audioSource == null || playerInfo == null) return;

        altarMenu.SetActive(true);
        playerInfo.bowActive = false;
        altarMenu.GetComponent<AltarMenu>().SetUsedAltar(this);
        audioSource.Play();
    }

    public void SpawnBoss()
    {
        if (boss == null || roomInfo == null) return;

        boss.SetActive(true);
        roomInfo.IncreaseEnemyCount();
    }

    public void DeactivateAltar()
    {
        if (belowChar == null || blueAltarBottom == null || blueAltarTop == null || blueAltarTopPos == null) return;
        
        belowChar.SetTile(blueAltarTopPos, blueAltarTop);
        belowChar.SetTile( new Vector3Int(blueAltarTopPos.x, blueAltarTopPos.y - 1, blueAltarTopPos.z), blueAltarBottom);
        gameObject.SetActive(false);
    }
}
