using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rotateDoor : MonoBehaviour
{
    bool rotating = false;
    [SerializeField] private GameObject blackOutSquare;
    [SerializeField] private GameObject objective1;

    IEnumerator CameraWait()
    {
        yield return new WaitForSeconds(2);
        rotating = true;
    }

    IEnumerator FadeToBlack()
    {
        Color objColor = blackOutSquare.GetComponent<Image>().color;
        float fade;
        yield return new WaitForSeconds(3);
        while(blackOutSquare.GetComponent<Image>().color.a < 1)
        {
            fade = objColor.a + (Time.deltaTime);

            objColor = new Color(objColor.r, objColor.g, objColor.b, fade);
            blackOutSquare.GetComponent<Image>().color = objColor;
            yield return null;
        }
        yield return new WaitForSeconds(1);
        while (blackOutSquare.GetComponent<Image>().color.a > 0)
        {
            fade = objColor.a - (Time.deltaTime);

            objColor = new Color(objColor.r, objColor.g, objColor.b, fade);
            blackOutSquare.GetComponent<Image>().color = objColor;
            yield return null;
        }
        objective1.SetActive(true);
    }

    void Update()
    {
        if(rotating && transform.rotation.y > -120)
        {
            transform.Rotate(0f,-180*Time.deltaTime,0f);
        }
        else
        {
            rotating = false;
        }
    }

    public void Rotate()
    {
        StartCoroutine(CameraWait());
        StartCoroutine(FadeToBlack());
    }
}
