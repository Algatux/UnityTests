using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [Header("Variables")]
    public float MouseSensitivity = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public bool CanInput() => Cursor.lockState == CursorLockMode.Locked;
    public float GetMovementStrafe() => CanInput() ? Input.GetAxis("Horizontal") : 0f;
    public float GetMovementAdvance() => CanInput() ? Input.GetAxis("Vertical") : 0f;
    public float GetRotation() => CanInput() ? Input.GetAxis("Mouse X") * MouseSensitivity : 0f;
    public float GetLook() => CanInput() ? Input.GetAxis("Mouse Y") * MouseSensitivity : 0f;

}
