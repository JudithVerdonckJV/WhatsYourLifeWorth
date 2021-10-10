using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractBehaviour : MonoBehaviour
{
    private bool canInteract;
    private Interactable interactObject = null;

    public void Interact()
    {
        if (canInteract)
        {
            interactObject.InteractWithObject();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Interactable")
        {
            interactObject = collision.gameObject.GetComponent<Interactable>();
            if (interactObject == null)
            {
                Debug.Log("Forgot to add the interactable script!");
                return;
            }
            Debug.Log("Base Interact called");
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canInteract = false;
    }
}
