using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class resultScreen : MonoBehaviour
{
    [SerializeField] private GameObject resultsScreen;
    [SerializeField] private GameObject blackOutSquare;
    [SerializeField] private GameObject objective1;
    [SerializeField] private ConstraintedMovement cm;
    [SerializeField] private PCSCript pc;

    IEnumerator FadeToBlack()
    {
        Color objColor = blackOutSquare.GetComponent<Image>().color;
        float fade;
        yield return new WaitForSeconds(1);
        while (blackOutSquare.GetComponent<Image>().color.a > 0)
        {
            fade = objColor.a - (Time.deltaTime);

            objColor = new Color(objColor.r, objColor.g, objColor.b, fade);
            blackOutSquare.GetComponent<Image>().color = objColor;
            yield return null;
        }
        resultsScreen.SetActive(false);
        Cursor.visible = true;
        cm.setMovementActive(true);
        pc.shipped = false;
        pc.started = false;
        pc.contractsCompleted = 0;
        pc.contractsFailed = 0;
        pc.overallScore = 0;
        objective1.SetActive(true);
    }

    public void ExitResults()
    {
        StartCoroutine(FadeToBlack());
    }
}
