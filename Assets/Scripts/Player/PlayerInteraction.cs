using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactDistance = 3f;
    public Camera playerCamera; // FPS kameray� buraya Inspector�dan atamal�s�n

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Sol t�k
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
