using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoSceneTrigger : MonoBehaviour
{
    public KeyPromptDisplay keyPromptDisplay;       // Inspector’dan ata
    public GameObject videoPlayerObject;            // Inspector’dan ata (isteğe bağlı)
    public string sceneToLoad;                      // Geçilecek sahne adı

    private bool playerNear = false;
    private bool hasInteracted = false;
    private bool videoIsPlaying = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasInteracted)
        {
            playerNear = true;
            if (keyPromptDisplay != null) keyPromptDisplay.ShowPrompt();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            if (keyPromptDisplay != null) keyPromptDisplay.HidePrompt();
        }
    }

    private void Update()
    {
        if (playerNear && !hasInteracted && Input.GetKeyDown(KeyCode.E))
        {
            hasInteracted = true;
            if (keyPromptDisplay != null) keyPromptDisplay.HidePrompt();

            if (videoPlayerObject != null)
            {
                PlayVideo();
            }
            else
            {
                LoadScene();
            }
        }
    }

    void PlayVideo()
    {
        videoIsPlaying = true;
        videoPlayerObject.SetActive(true);
        var videoPlayer = videoPlayerObject.GetComponent<UnityEngine.Video.VideoPlayer>();
        if (videoPlayer != null)
        {
            videoPlayer.Play();
            videoPlayer.loopPointReached += OnVideoFinished;
        }
        else
        {
            // VideoPlayer component yoksa, direkt sahneyi değiştir
            LoadScene();
        }
    }

    void OnVideoFinished(UnityEngine.Video.VideoPlayer vp)
    {
        videoIsPlaying = false;
        if (videoPlayerObject != null)
            videoPlayerObject.SetActive(false);
        LoadScene();
    }

    void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
