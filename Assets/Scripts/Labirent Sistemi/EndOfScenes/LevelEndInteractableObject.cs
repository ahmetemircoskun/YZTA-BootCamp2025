using UnityEngine;

public class LevelEndInteractableObject : MonoBehaviour
{
    public KeyPromptDisplay keyPromptDisplay; // Inspector’dan ata!
    public LevelEndDoor linkedDoor; // Video bitince kapı açılacak
    public GameObject videoPlayerObject; // Inspector’dan ata

    private bool canInteract = true;
    private bool isPlayerNear = false;
    private bool videoIsPlaying = false;

    void Update()
    {
        // Sadece canInteract ve video oynatılmıyorken E tuşu çalışmalı
        if (canInteract && isPlayerNear && !videoIsPlaying && Input.GetKeyDown(KeyCode.E))
        {
            videoIsPlaying = true; // Video oynuyor olarak işaretle
            if (videoPlayerObject != null)
            {
                videoPlayerObject.SetActive(true);
                var videoPlayer = videoPlayerObject.GetComponent<UnityEngine.Video.VideoPlayer>();
                if (videoPlayer != null)
                {
                    videoPlayer.Play();
                    videoPlayer.loopPointReached += OnVideoFinished;
                }
            }
            // Promptu kapat
            if (keyPromptDisplay != null)
                keyPromptDisplay.HidePrompt();
        }
    }

    private void OnVideoFinished(UnityEngine.Video.VideoPlayer vp)
    {
        // Bir daha asla etkileşim olmasın!
        canInteract = false;
        videoIsPlaying = false;
        if (keyPromptDisplay != null)
            keyPromptDisplay.HidePrompt();
        if (linkedDoor != null)
            linkedDoor.EnableInteraction();
        if (videoPlayerObject != null)
            videoPlayerObject.SetActive(false);

        // Collider’ı disable et (oyuncu çıkınca tekrar triggera giremesin!)
        var collider = GetComponent<Collider>();
        if (collider != null)
            collider.enabled = false;

        // Script’i de istersen devre dışı bırakabilirsin
        // this.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canInteract && !videoIsPlaying)
        {
            isPlayerNear = true;
            if (keyPromptDisplay != null)
                keyPromptDisplay.ShowPrompt();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            if (keyPromptDisplay != null)
                keyPromptDisplay.HidePrompt();
        }
    }
}
