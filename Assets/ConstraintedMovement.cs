using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstraintedMovement : MonoBehaviour
{
    public bool movementActive = false;
    private bool playing = false;
    Vector3 moveAxis;
    public float speed = 1;
    public float CamSpeed = 1;

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
            moveAxis = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            moveAxis = Vector3.ClampMagnitude(moveAxis, 1);
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f)
            {
                playing = true;
            }
            if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f)
            {
                playing = true;
            }
            if(Mathf.Abs(Input.GetAxis("Horizontal")) < 0.1f && Mathf.Abs(Input.GetAxis("Vertical")) < 0.1f)
            {
                playing = false;
                this.GetComponent<AudioSource>().Stop();
            }
            else if(playing == true)
            {
                this.GetComponent<AudioSource>().Play();
            }
            
            if (Mathf.Abs(Input.GetAxis("Mouse X")) > 0.1f)
            {
                yaw += CamSpeed * Input.GetAxis("Mouse X");
            }
            if (Mathf.Abs(Input.GetAxis("Mouse Y")) > 0.1f)
            {
                pitch -= CamSpeed * Input.GetAxis("Mouse Y");
            }
            if (pitch > 90.0f) { pitch = 90.0f; }
            if (pitch < -90.0f) { pitch = -90.0f; }
        }
        else { this.GetComponent<AudioSource>().Stop(); }
    }

    private void FixedUpdate()
    {
        if(movementActive == true)
        {
            this.GetComponent<Rigidbody>().rotation = Quaternion.Euler(new Vector3(0f,yaw,0f));
            transform.GetChild(0).GetComponent<Rigidbody>().rotation = Quaternion.Euler(new Vector3(pitch, yaw, 0.0f));
            this.GetComponent<Rigidbody>().MovePosition(this.GetComponent<Rigidbody>().position + transform.forward * moveAxis.z * speed * Time.deltaTime);
            this.GetComponent<Rigidbody>().MovePosition(this.GetComponent<Rigidbody>().position + transform.right * moveAxis.x * speed * Time.deltaTime);
            transform.GetChild(0).position = new Vector3(0, 1, 0) + transform.position;
        }
    }
}
