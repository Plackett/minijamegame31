using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClockScript : MonoBehaviour
{
    [SerializeField] private Transform hourHand;
    [SerializeField] private Transform minuteHand;
    [SerializeField] private Transform secondHand;
    public int hour = 0;
    public int minute = 0;
    public int second = 0;

    // Start is called before the first frame update
    void Start()
    {
        Update();   
    }

    // Update is called once per frame
    void Update()
    {
        DateTime localDate = DateTime.Now;
        hour = localDate.Hour;
        minute = localDate.Minute;
        second = localDate.Second;
        hourHand.rotation = Quaternion.Euler(0,90,30*localDate.Hour);
        minuteHand.rotation = Quaternion.Euler(0,90,6*localDate.Minute);
        secondHand.rotation = Quaternion.Euler(0,90,6*localDate.Second);
    }
}
