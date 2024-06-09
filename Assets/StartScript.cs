using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StartScript : MonoBehaviour
{
    [SerializeField] private GameObject menuText;
    [SerializeField] private GameObject cameras;
    [SerializeField] private GameObject gameCamera;
    [SerializeField] private PlayableAsset menuCutscene;
    [SerializeField] private PlayableDirector menuDirector;
    public ConstraintedMovement plr;
    bool gameStarted = false;
    public audioController aC;

    // Update is called once per frame
    void Update()
    {
        if(gameStarted == false && Input.GetKey(KeyCode.Return))
        {
            Cursor.visible = false;
            cameras.SetActive(true);
            menuDirector.playableAsset = menuCutscene;
            menuDirector.Play();
            menuText.SetActive(false);
            cameras.SetActive(false);
            gameCamera.SetActive(true);
            gameStarted = true;
            plr.setMovementActive(true);
            aC.StopBackTrackCity();
            aC.StartBackTrackOffice();
        }
    }

    void Start()
    {
        aC.StartBackTrackCity();
    }
}
