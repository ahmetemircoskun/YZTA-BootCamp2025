using UnityEngine;

public class FindLiar : MonoBehaviour
{
    public string sentence; // Kişinin söylediği cümle
    public bool isTellingLie; // Doğruyu söyleyen kişi mi?

    void OnMouseEnter()
    {
        Debug.Log($"Diyor ki: \"{sentence}\"");
    }

    void OnMouseDown()
    {
        if (isTellingLie)
        {
            Debug.Log("Bildin!");
        }
        else
        {
            Debug.Log("Bilemedin.");
        }
    }
}
