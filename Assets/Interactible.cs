using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Interactible : MonoBehaviour
{
    public bool isInteractable = false;
    public bool isOnComputer = false;
    [SerializeField] private PlayableDirector director;
    [SerializeField] private PlayableAsset cutscene;
    [SerializeField] private GameObject computerCamera;
    [SerializeField] private GameObject computerScreen;
    [SerializeField] private GameObject objective1;
    [SerializeField] private GameObject objective4;
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private ConstraintedMovement cm;
    [SerializeField] private PCSCript pc;
    public TimerScript ts;

    IEnumerator openPCDelay()
    {
        yield return new WaitForSeconds(1);
        computerScreen.SetActive(true);
        objective1.SetActive(false);
#if UNITY_WEBGL
                Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.ForceSoftware);
#else
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
#endif
        Cursor.lockState = CursorLockMode.Confined;
        pc.StartUpPC();
        ts.StartTime(60);
    }

    IEnumerator returnPCDelay()
    {
        yield return new WaitForSeconds(1);
        computerScreen.SetActive(true);
#if UNITY_WEBGL
                Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.ForceSoftware);
#else
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
#endif
        Cursor.lockState = CursorLockMode.Confined;
        pc.page = 3;
        pc.nextPage();
    }

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
        if (isInteractable && pc.started == false)
        {
            computerCamera.SetActive(true);
            isOnComputer = true;
            director.playableAsset = cutscene;
            director.Play();
            director.stopped += OnComputerStart;
        } else if(isInteractable && pc.shipped == true)
        {
            pc.shipped = false;
            computerCamera.SetActive(true);
            isOnComputer = true;
            director.playableAsset = cutscene;
            director.Play();
            director.stopped -= OnComputerStart;
            director.stopped += OnComputerReturn;
            objective4.SetActive(false);
            objective1.SetActive(false);
            cm.setMovementActive(false);
        }    
    }

    public void OnComputerStart(PlayableDirector d)
    {
        cm.setMovementActive(false);
        StartCoroutine(openPCDelay());
    }

    public void OnComputerReturn(PlayableDirector d)
    {
        StartCoroutine(returnPCDelay());
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
