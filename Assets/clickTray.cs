using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickTray : MonoBehaviour
{
    private bool isInteractable = false;
    public GameObject objective3;
    public GameObject objective4;
    public PCSCript pc;
    public Interactible it;

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
        if (isInteractable)
        {
            objective3.SetActive(false);
            objective4.SetActive(true);
            pc.shipped = true;
            it.isOnComputer = false;
            this.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if(isInteractable)
        {
            this.GetComponent<Outline>().enabled = true;
        }
        else
        {
            this.GetComponent<Outline>().enabled = false;
        }
    }
}
