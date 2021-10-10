using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomIndexManagment : MonoBehaviour
{
    [SerializeField] private int indexOfGrid = 0;
    [SerializeField] private int enemyCount = 0;
    [SerializeField] GameObject door = null;

    private void Start()
    {
        if (enemyCount <= 0)
        {
            if (door != null) door.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<BasePlayer>().currentGridIndex = indexOfGrid;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<BasePlayer>().currentGridIndex = -1;
        }
    }

    public int GetEnemyCount()
    {
        return enemyCount;
    }

    public void ReduceEnemyCount()
    {
        enemyCount--;
        if (enemyCount <= 0)
        {
            if (door != null) door.SetActive(true);
        }
    }

    public void IncreaseEnemyCount()
    {
        enemyCount++;
        if (door != null) door.SetActive(false);
    }

    public int GetRoomIndex()
    {
        return indexOfGrid;
    }
}
