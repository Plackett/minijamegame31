using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Interactible : MonoBehaviour
{
    private bool isInteractable = false;
    public bool isOnComputer = false;
    [SerializeField] private PlayableDirector director;
    [SerializeField] private PlayableAsset cutscene;
    [SerializeField] private GameObject computerCamera;
    [SerializeField] private GameObject computerScreen;
    [SerializeField] private UnityEngine.Video.VideoPlayer vp;
    [SerializeField] private GameObject objective1;


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
            computerCamera.SetActive(true);
            isOnComputer = true;
            director.playableAsset = cutscene;
            director.Play();
            director.stopped += OnComputerStart;
        }
    }

    public void OnComputerStart(PlayableDirector d)
    {
        computerScreen.SetActive(true);
        objective1.SetActive(false);
        vp.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(isInteractable && !isOnComputer)
        {
            computerCamera.SetActive(false);
            computerScreen.SetActive(false);
            this.GetComponent<Outline>().enabled = true;
        }
        else
        {
            this.GetComponent<Outline>().enabled = false;
        }
    }
}
