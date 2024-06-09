using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorScript : MonoBehaviour
{
    private bool isInteractable = false;
    public bool clickEnabled = false;
    bool isOpen = false;
    public GameObject exit;
    public audioController aC;

    private void OnMouseOver()
    {
        //turns on interactivity 
        isInteractable = true;
    }

    private void OnMouseExit()
    {
        //turns off interactivity 
        isInteractable = false;
    }

    private void OnMouseDown()
    {
        if (isInteractable && !isOpen && clickEnabled)
        {
            aC.PlayDoor();
            exit.SetActive(true);
            isOpen = true;
            transform.Rotate(0, 90, 0);
        }
    }

    void Update()
    {
        if (isInteractable)
        {
            this.GetComponent<Outline>().enabled = true;
        }
        else
        {
            this.GetComponent<Outline>().enabled = false;
        }
    }
}
