using UnityEngine;

public class FindLiar : MonoBehaviour
{
    public string sentence; // Kişinin söylediği cümle
    public bool isTellingTrue; // Doğruyu söyleyen kişi mi?

    public AudioClip puzzleSolvedSound;
    private AudioSource audioSource;
    public AudioClip wrongSound;
    private AudioSource wrongAudioSource;

    [Range(0, 1)]
    public float solvedVolume = 1f;

    [Range(0, 1)]
    public float wrongVolume = 1f;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.volume = solvedVolume;

        wrongAudioSource = gameObject.AddComponent<AudioSource>();
        wrongAudioSource.playOnAwake = false;
        wrongAudioSource.volume = wrongVolume;
    }
    void OnMouseEnter()
    {
        if (SentenceDisplayController.Instance != null)
        {
            SentenceDisplayController.Instance.ShowSentence($"\"{sentence}\"");
        }
    }

    void OnMouseExit()
    {
        if (SentenceDisplayController.Instance != null)
        {
            SentenceDisplayController.Instance.HideSentence();
        }
    }

    void OnMouseDown()
    {
        if (isTellingTrue)
        {
            Debug.Log("Bildin!");
            CheckSolution();
        }
        else
        {
            Debug.Log("Bilemedin.");
            audioSource.PlayOneShot(wrongSound);
        }
    }

    public void CheckSolution()
    {
        Debug.Log("Doğru çözüldü!");

        if (puzzleSolvedSound != null)
        {
            audioSource.PlayOneShot(puzzleSolvedSound);
        }

        if (PuzzleManager.Instance != null)
        {
            PuzzleManager.Instance.PuzzleSolved();
        }
    }
}
