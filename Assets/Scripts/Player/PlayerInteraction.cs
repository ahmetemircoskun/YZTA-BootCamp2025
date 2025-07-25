using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactDistance = 3f;
    public Camera playerCamera; // FPS kamerayý buraya Inspector’dan atamalýsýn

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Sol týk
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
