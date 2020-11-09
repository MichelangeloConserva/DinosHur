using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float mouseSensitivity;
    public Transform player;

    float maxHeight = 45f;
    float minHeight = -45f;
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


       if (rotatePlayer.x > 45 && rotatePlayer.x < 180)
        {
            rotatePlayer.x = 45;
        }
       else if (rotatePlayer.x > 180 && rotatePlayer.x < 315)
        {
            rotatePlayer.x = 315;
        }
        rotatePlayer.z = 0;

        player.rotation = Quaternion.Euler(rotatePlayer);

    }
}
