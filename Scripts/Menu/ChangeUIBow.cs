using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeUIBow : MonoBehaviour
{
    private BowUsable uiBowScript = null;

    private void Start()
    {
        uiBowScript = GameObject.Find("UI").transform.Find("Bow").GetComponent<BowUsable>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            uiBowScript.SetUnusable();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            uiBowScript.SetUsable();
        }
    }
}
