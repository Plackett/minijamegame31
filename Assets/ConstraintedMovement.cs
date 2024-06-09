using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstraintedMovement : MonoBehaviour
{
    public bool movementActive = false;
    private bool playing = false;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    public void setMovementActive(bool enabled)
    {
        movementActive = enabled;
    }

    // Update is called once per frame
    void Update()
    {
        if(movementActive)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
            if(Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f)
            {
                playing = true;
                transform.Translate(Vector3.right * 4 * Time.deltaTime * Input.GetAxis("Horizontal"));
            }
            if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f)
            {
                playing = true;
                transform.Translate(Vector3.forward * 4 * Time.deltaTime * Input.GetAxis("Vertical"));
            }
            if(Mathf.Abs(Input.GetAxis("Horizontal")) < 0.1f && Mathf.Abs(Input.GetAxis("Vertical")) < 0.1f)
            {
                playing = false;
                this.GetComponent<AudioSource>().Pause();
            }
            else if(playing == true)
            {
                this.GetComponent<AudioSource>().UnPause();
            }
            if (Mathf.Abs(Input.GetAxis("Mouse X")) > 0.1f)
            {
                yaw += 2 * Input.GetAxis("Mouse X");
            }
            if (Mathf.Abs(Input.GetAxis("Mouse Y")) > 0.1f)
            {
                pitch -= 2 * Input.GetAxis("Mouse Y");
            }
            if (pitch > 90.0f) { pitch = 90.0f; }
            if (pitch < -90.0f) { pitch = -90.0f; }
            transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);
            transform.GetChild(0).eulerAngles = new Vector3(pitch,yaw,0.0f);
            transform.GetChild(0).position = new Vector3(0, 1, 0) + transform.position;
        }
        else { this.GetComponent<AudioSource>().Stop(); }
    }
}
