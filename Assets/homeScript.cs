using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class homeScript : MonoBehaviour
{
    [SerializeField] private GameObject objective5;
    [SerializeField] private GameObject resultsScreen;
    [SerializeField] private GameObject blackOutSquare;
    [SerializeField] private ConstraintedMovement cm;
    [SerializeField] private TextMeshProUGUI completed;
    [SerializeField] private TextMeshProUGUI failed;
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private Interactible it;
    [SerializeField] private PCSCript pc;
    [SerializeField] private Transform plr;
    [SerializeField] private Transform doorTransform;

    void OnTriggerEnter(Collider collision)
    {
        StartCoroutine(FadeToCredits());
    }

    private IEnumerator FadeToCredits()
    {
        Color objColor = blackOutSquare.GetComponent<Image>().color;
        float fade;
        yield return new WaitForSeconds(3);
        while (blackOutSquare.GetComponent<Image>().color.a < 1)
        {
            fade = objColor.a + (Time.deltaTime);

            objColor = new Color(objColor.r, objColor.g, objColor.b, fade);
            blackOutSquare.GetComponent<Image>().color = objColor;
            yield return null;
        }
        objective5.SetActive(false);
        resultsScreen.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        cm.setMovementActive(false);
        it.isOnComputer = false;
        pc.shipped = false;
        pc.started = false;
        plr.position = new Vector3(4.32f,6.228f,-1.74f);
        plr.rotation = new Quaternion(0, 0, 0, 1);
        completed.text = "" + pc.contractsCompleted;
        failed.text = "" + pc.contractsFailed;
        score.text = "" + pc.overallScore;
        doorTransform.Rotate(0, -90, 0);
        pc.door.GetComponent<doorScript>().clickEnabled = false;
        pc.door.GetComponent<doorScript>().enabled = false;
        this.gameObject.SetActive(false);
    }
}
