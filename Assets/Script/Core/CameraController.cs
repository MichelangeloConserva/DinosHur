using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float mouseSensitivity;
    public Transform player;

    float maxHeight = 45f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        RotateCamera();
    }

    void RotateCamera() {

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float rotateAmountX = mouseX * mouseSensitivity;
        float rotateAmountY = mouseY * mouseSensitivity;
      
        Vector3 rotatePlayer = player.transform.rotation.eulerAngles;
        rotatePlayer.y += rotateAmountX;
        rotatePlayer.x -= rotateAmountY;

       if (rotatePlayer.x > maxHeight && rotatePlayer.x < 180)
        {
            rotatePlayer.x = maxHeight;
        }
       else if (rotatePlayer.x > 180 && rotatePlayer.x < 360 - maxHeight)
        {
            rotatePlayer.x = 360 - maxHeight;
        }
        rotatePlayer.z = 0;

        player.rotation = Quaternion.Euler(rotatePlayer);

    }
}
