using UnityEngine;

public class AlwaysShowCursor : MonoBehaviour
{
    void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
