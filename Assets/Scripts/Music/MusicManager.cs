using UnityEngine;
using UnityEngine.Video;

public class MusicManager : MonoBehaviour
{
    [Header("Müzik Ayarları")]
    public AudioClip sceneMusic;      // Bu sahneye özel müzik
    [Range(0, 1)]
    public float musicVolume = 1.0f;  // Ses seviyesi (Inspector’dan ayarla)
    public bool pauseMusicOnVideo = true; // Video oynarken müzik dursun mu?

    private AudioSource audioSource;

    void Awake()
    {
        // Sahneye ikinci kez eklenirse, eskisini sil (isteğe bağlı)
        var others = FindObjectsOfType<MusicManager>();
        if (others.Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        // Oyun boyunca çalsın dersen aç:
        // DontDestroyOnLoad(gameObject);

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = sceneMusic;
        audioSource.loop = true;
        audioSource.volume = musicVolume;
        audioSource.playOnAwake = false;
        audioSource.Play();
    }

    // Inspector’dan gerçek zamanlı ses ayarı için
    void Update()
    {
        audioSource.volume = musicVolume;
    }

    // Video oynarken çağır
    public void SetMusicPaused(bool paused)
    {
        if (pauseMusicOnVideo)
        {
            if (paused) audioSource.Pause();
            else audioSource.UnPause();
        }
    }
}
