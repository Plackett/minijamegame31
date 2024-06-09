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
    [SerializeField] private GameObject objective1;
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private ConstraintedMovement cm;
    [SerializeField] private PCSCript pc;
    public TimerScript ts;


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
        cm.movementActive = false;
        computerScreen.SetActive(true);
        objective1.SetActive(false);
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        Cursor.lockState = CursorLockMode.Confined;
        pc.StartUpPC();
        ts.StartTime(60);
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
