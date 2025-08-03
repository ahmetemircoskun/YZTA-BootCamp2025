using UnityEngine;
using UnityEngine.Video;

public class VideoMusicControl : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Inspector’dan ata
    public MusicManager musicManager; // Inspector’dan ata (veya sahneden bul)

    void Start()
    {
        if (videoPlayer == null)
            videoPlayer = GetComponent<VideoPlayer>();
        if (musicManager == null)
            musicManager = FindObjectOfType<MusicManager>();

        // Video başladığında müziği durdur
        videoPlayer.started += OnVideoStarted;
        // Video bittiğinde müziği devam ettir
        videoPlayer.loopPointReached += OnVideoEnded;
    }

    void OnVideoStarted(VideoPlayer vp)
    {
        musicManager?.SetMusicPaused(true);
    }

    void OnVideoEnded(VideoPlayer vp)
    {
        musicManager?.SetMusicPaused(false);
    }
}
