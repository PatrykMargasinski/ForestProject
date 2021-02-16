using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour //skrypt odpowiedzialny na poruszanie "głową" postaci, dołączony do kamery
{
    public float mouseSensitivity = 100.0f;
    public Transform playerBody;
    float xRotation = 0.0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);//zablokowana jest możliwość obrócenia się o 360 stopnii wzdłuż osi X - byłby to nienaturny ruch

        transform.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);
        playerBody.Rotate(Vector3.up * mouseX);//obrot kamery jest wykonywany na podstawie ruchów myszy
    }
}
