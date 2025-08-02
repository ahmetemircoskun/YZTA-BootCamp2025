using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndDoor : MonoBehaviour
{
    public KeyPromptDisplay keyPromptDisplay;
    public string nextSceneName;
    public Door doorScript;

    private bool canInteract = false;
    private bool isPlayerNear = false;

    public void EnableInteraction()
    {
        canInteract = true;
        if (keyPromptDisplay != null)
            keyPromptDisplay.ShowPrompt();
        if (doorScript != null)
            doorScript.OpenDoor();
        // E promptu sürekli açık kalacak
    }

    void Update()
    {
        // E’ye basınca sadece oyuncu kapının trigger’ındaysa ve etkileşim açıksa sahne değişsin
        if (canInteract && isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            // İster prompt tekrar göster, ister gösterme — sürekli açıksa burada zorunlu değil
            // if (keyPromptDisplay != null)
            //     keyPromptDisplay.ShowPrompt();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            // İster prompt kapat, ister açık bırak
            // if (keyPromptDisplay != null)
            //     keyPromptDisplay.HidePrompt();
        }
    }
}
