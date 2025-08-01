using UnityEngine;
using UnityEngine.UI;

public class CandleInteraction : MonoBehaviour
{
    public float interactDistance = 3f;
    public Camera playerCamera; // FPS kameray� buraya Inspector�dan atamal�s�n

    Candle lastCandle = null;

    void Update()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            Candle candle = hit.collider.GetComponent<Candle>();
            if (candle != null)
            {
                // ShowPrompt
                var prompt = candle.GetComponent<KeyPromptDisplay>();
                if (prompt != null) prompt.ShowPrompt();

                lastCandle = candle;

                // E tuşu ile yak
                if (Input.GetKeyDown(KeyCode.E))
                {
                    candle.Toggle();
                }
            }
            else if (lastCandle != null)
            {
                // Raycast'ta candle yoksa en son gösterilen promptu gizle
                var prompt = lastCandle.GetComponent<KeyPromptDisplay>();
                if (prompt != null) prompt.HidePrompt();
                lastCandle = null;
            }
        }
        else if (lastCandle != null)
        {
            // Raycast hiçbir şeye değmiyorsa, promptu yine gizle
            var prompt = lastCandle.GetComponent<KeyPromptDisplay>();
            if (prompt != null) prompt.HidePrompt();
            lastCandle = null;
        }
    }
}
