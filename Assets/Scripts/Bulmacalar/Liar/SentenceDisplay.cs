using TMPro;
using UnityEngine;

public class SentenceDisplayController : MonoBehaviour
{
    public static SentenceDisplayController Instance;

    [SerializeField] private TextMeshPro textMeshPro;

    private void Awake()
    {
        Instance = this;
        if (textMeshPro == null)
            textMeshPro = GetComponent<TextMeshPro>();
    }

    public void ShowSentence(string sentence)
    {
        textMeshPro.text = sentence;
    }

    public void HideSentence()
    {
        textMeshPro.text = "";
    }
}
