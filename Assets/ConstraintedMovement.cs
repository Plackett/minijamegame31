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
            transform.Translate(Vector3.right * 2 * Time.deltaTime * Input.GetAxis("Horizontal"));
            transform.Translate(Vector3.forward * 2 * Time.deltaTime * Input.GetAxis("Vertical"));
            yaw += 2*Input.GetAxis("Mouse X");
            pitch -= 2*Input.GetAxis("Mouse Y");
            if(yaw < -180.0f) { yaw = -180.0f; }
            if (pitch > 90.0f) { pitch = 90.0f; }
            if (yaw > 0) { yaw = 0; }
            if (pitch < -90.0f) { pitch = -90.0f; }
            transform.eulerAngles = new Vector3(pitch,yaw,0.0f);
            transform.GetChild(0).position = new Vector3(transform.position.x, transform.position.y+1, transform.position.z);
            transform.GetChild(0).rotation = transform.rotation;
        }
    }
}
