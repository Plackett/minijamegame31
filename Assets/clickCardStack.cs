using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickCardStack : MonoBehaviour
{
    private bool isInteractable = false;
    public GameObject objective2;
    public GameObject objective3;
    public GameObject tray;
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
        if(isInteractable)
        {
            aC.CardCollect();
            objective2.SetActive(false);
            objective3.SetActive(true);
            tray.SetActive(true);
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
