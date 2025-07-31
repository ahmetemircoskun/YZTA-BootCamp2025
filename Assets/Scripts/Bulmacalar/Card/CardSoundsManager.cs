using UnityEngine;

public class CardSoundsManager : MonoBehaviour
{
    public static CardSoundsManager Instance;

    public AudioClip matchSound;
    public AudioClip mismatchSound;

    [Range(0, 1)]
    public float matchVolume = 1f;
    [Range(0, 1)]
    public float mismatchVolume = 1f;

    private AudioSource audioSource;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void PlayMatchSound()
    {
        if (matchSound != null)
            audioSource.PlayOneShot(matchSound, matchVolume);
    }

    public void PlayMismatchSound()
    {
        if (mismatchSound != null)
            audioSource.PlayOneShot(mismatchSound, mismatchVolume);
    }
}
