using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstraintedMovement : MonoBehaviour
{
    public bool movementActive = false;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if(movementActive)
        {
            Cursor.lockState = CursorLockMode.Locked;
            if(Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f)
            {
                transform.Translate(Vector3.right * 2 * Time.deltaTime * Input.GetAxis("Horizontal"));
            }
            if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f)
            {
                transform.Translate(Vector3.forward * 2 * Time.deltaTime * Input.GetAxis("Vertical"));
            }
            if (Mathf.Abs(Input.GetAxis("Mouse X")) > 0.1f)
            {
                yaw += 2 * Input.GetAxis("Mouse X");
            }
            if (Mathf.Abs(Input.GetAxis("Mouse Y")) > 0.1f)
            {
                pitch -= 2 * Input.GetAxis("Mouse Y");
            }
            if(yaw < -180.0f) { yaw = -180.0f; }
            if (pitch > 90.0f) { pitch = 90.0f; }
            if (yaw > 0) { yaw = 0; }
            if (pitch < -90.0f) { pitch = -90.0f; }
            transform.eulerAngles = new Vector3(pitch,yaw,0.0f);
            transform.GetChild(0).rotation = transform.rotation;
        }
    }
}
