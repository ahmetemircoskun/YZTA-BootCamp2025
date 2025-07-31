using TMPro;
using UnityEngine;

public class WordCountDisplayController : MonoBehaviour
{
    public WordGameManager wordGameManager;
    public TextMeshPro textMeshPro;

    void Update()
    {
        if (wordGameManager == null || textMeshPro == null) return;

        int found = wordGameManager.GetFoundWordCount();
        int total = wordGameManager.GetTotalWordCount();
        textMeshPro.text = found + " / " + total;
    }
}
