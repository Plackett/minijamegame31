using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public RectTransform timeBar;
    public Image back;
    bool countingDown = false;
    public float TIME = 0;
    public float elapsed = 0;

    public void StartTime(float inTIME)
    {
        elapsed = 0;
        countingDown = true;
        TIME = inTIME;
    }

    public void StopTime()
    {
        elapsed = 0;
        countingDown = false;
        TIME = 0;
    }

    public void Update()
    {
        if(elapsed < TIME && countingDown == true)
        {
            elapsed += Time.deltaTime;
            timeBar.sizeDelta = new Vector2(100 * (elapsed / TIME), 15);

            // normal colors
            if (elapsed <= TIME / 2)
            {
                back.color = new Color(23f, 255f, 0f, 100f);
            }

            // warning colors
            if (elapsed > TIME / 2)
            {
                back.color = new Color(100f, 100f, 0f, 100f);
            }

            // final colors
            if (elapsed > (3*TIME) / 4)
            {
                back.color = new Color(100f, 0f, 0f, 100f);
            }
        } else
        {
            countingDown = false;
        }
    }
}
