using UnityEngine;
using UnityEngine.UI;

public class CandleInteraction : MonoBehaviour
{
    public float interactDistance = 3f;
    public Camera playerCamera; // FPS kameray� buraya Inspector�dan atamal�s�n

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // E
        {
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
            {
                Candle candle = hit.collider.GetComponent<Candle>();
                if (candle != null)
                {
                    candle.Toggle();
                }
            }
        }
    }
}
