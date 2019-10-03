using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }

    public RotationAxes axes = RotationAxes.MouseXAndY;

    //Sensitivity controls
    public float sensitivityX = 15f;
    public float sensitivityY = 15f;

    //Margin controls
    public float minimumX = -360f;
    public float maximumX = 360f;
    public float minimumY = -60f;
    public float maximumY = 60f;

    //Y offset
    public float rotationY = 0f;
    
    void Update()
    {
        switch (axes)
        {
            case RotationAxes.MouseXAndY: //Omni-directional controls

                float rotationX = transform.localEulerAngles.y + (Input.GetAxis("Mouse X") * sensitivityX);
                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
                transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
                break;

            case RotationAxes.MouseX: //Horizontal only controls

                transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
                break;

            case RotationAxes.MouseY: //Vertical only controls

                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
                transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
                break;
        }
    }
}