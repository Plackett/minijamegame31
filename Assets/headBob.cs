using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headBob : MonoBehaviour
{
    public float walkingBobbingSpeed = 4.8f;
    public float bobbingAmount = 0.05f;

    float timer = Mathf.PI / 2;

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f)
        {
            transform.position = new Vector3(0, 1, 0) + transform.parent.position;
            //Player is moving
            timer += Time.deltaTime * walkingBobbingSpeed;
            transform.position += Vector3.up * (Mathf.Sin(timer) * bobbingAmount);
        }
        else
        {
            transform.position = new Vector3(0, 1, 0) + transform.parent.position;
            timer = Mathf.PI / 2;
        }
        if (timer > Mathf.PI * 2)
        {
            timer = 0;
        }
    }
}
