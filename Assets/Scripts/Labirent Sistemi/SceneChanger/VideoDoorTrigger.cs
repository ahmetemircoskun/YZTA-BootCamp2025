using UnityEngine;

public class VideoDoorTrigger : MonoBehaviour
{
    public KeyPromptDisplay keyPromptDisplay;        // Inspector’dan ata
    public GameObject videoPlayerObject;             // Inspector’dan ata (VideoPlayer component içeren obje, isteğe bağlı)
    public Door doorScript;                          // Inspector’dan ata (kapının Door scripti)
    public float openDelay = 0f;                     // Video bitince kapı açılmasını geciktirmek istersen

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
            hasInteracted = true; // Bir daha etkileşime girilmesin
            if (keyPromptDisplay != null) keyPromptDisplay.HidePrompt();
            PlayVideoOrOpenDoor();
        }
    }

    void PlayVideoOrOpenDoor()
    {
        if (videoPlayerObject != null)
        {
            videoPlayerObject.SetActive(true);
            var videoPlayer = videoPlayerObject.GetComponent<UnityEngine.Video.VideoPlayer>();
            if (videoPlayer != null)
            {
                videoIsPlaying = true;
                videoPlayer.Play();
                videoPlayer.loopPointReached += OnVideoFinished;
            }
            else
            {
                // VideoPlayer component yoksa, direkt kapıyı aç
                OpenDoorAfterDelay();
            }
        }
        else
        {
            // Video nesnesi yoksa, direkt kapıyı aç
            OpenDoorAfterDelay();
        }
    }

    void OnVideoFinished(UnityEngine.Video.VideoPlayer vp)
    {
        videoIsPlaying = false;
        if (videoPlayerObject != null)
            videoPlayerObject.SetActive(false);

        OpenDoorAfterDelay();
    }

    void OpenDoorAfterDelay()
    {
        if (openDelay > 0f)
            Invoke(nameof(OpenDoor), openDelay);
        else
            OpenDoor();
    }

    void OpenDoor()
    {
        if (doorScript != null)
            doorScript.OpenDoor();
    }
}
