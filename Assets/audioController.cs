using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioController : MonoBehaviour
{
    [SerializeField] private AudioSource pcBack;
    [SerializeField] private AudioSource Good;
    [SerializeField] private AudioSource Bad;
    [SerializeField] private AudioSource Perfect;
    [SerializeField] private AudioSource Card;
    [SerializeField] private AudioSource Printer;
    [SerializeField] private AudioSource Click;
    [SerializeField] private AudioSource Door;
    [SerializeField] private AudioSource Office;
    [SerializeField] private AudioSource City;
    public ConstraintedMovement cm;
    public GameObject pauseScreen;
    public bool paused = false;
    public bool debounce = true;
    bool reenablemovement = false;

    IEnumerator fixDebounce()
    {
        yield return new WaitForSeconds(1);
        if(paused == true)
        {
            Time.timeScale = 0;
        }
        debounce = true;
    }

    public void PlayGood()
    {
        Good.Play();
    }

    public void PlayBad()
    {
        Bad.Play();
    }

    public void PlayPerfect()
    {
        Perfect.Play();
    }

    public void StartBackTrackPC()
    {
        pcBack.Play();
    }

    public void StopBackTrackPC()
    {
        pcBack.Stop();
    }

    public void StartBackTrackOffice()
    {
        Office.Play();
    }

    public void StopBackTrackOffice()
    {
        Office.Stop();
    }

    public void StartBackTrackCity()
    {
        City.Play();
    }

    public void StopBackTrackCity()
    {
        City.Stop();
    }

    public void PrinterTrack()
    {
        Printer.Play();
    }

    public void CardCollect()
    {
        Card.Play();
    }

    public void PlayClick()
    {
        Click.Play();
    }

    public void PlayDoor()
    {
        Door.Play();
    }

    public void Pause()
    {
        if(debounce)
        {
            debounce = false;
            pauseScreen.SetActive(true);
            paused = true;
            if(cm.movementActive == true)
            {
                reenablemovement = true;
                cm.movementActive = false;
                Cursor.lockState = CursorLockMode.Confined;
            }
            StartCoroutine(fixDebounce());
        }
    }

    public void Resume()
    {
        if(debounce)
        {
            Time.timeScale = 1;
            debounce = false;
            pauseScreen.SetActive(false);
            paused = false;
            if(reenablemovement)
            {
                reenablemovement = false;
                cm.setMovementActive(true);
                Cursor.lockState = CursorLockMode.Locked;
            }
            StartCoroutine(fixDebounce());
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Backspace) && debounce)
        {
            if(paused == true)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    void Start()
    {
        debounce = true;
    }

}
