using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleTrigger : MonoBehaviour
{
    public string targetDoorId;
    public string targetPuzzleScene;

    private bool playerNear = false;


    void Start()
    {
        // Eğer kapı açıksa, collider ve prompt kapanmalı
        if (GameStateManager.Instance != null &&
            GameStateManager.Instance.IsDoorOpen(targetDoorId))
        {
            if (ePromptDisplay != null) ePromptDisplay.HidePrompt();
            var collider = GetComponent<Collider>();
            if (collider != null)
                collider.enabled = false;
            enabled = false; // Script'i de devre dışı bırakabilirsin
        }
    }

    private void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                GameStateManager.Instance.lastPlayerPosition = player.transform.position;
                GameStateManager.Instance.hasLastPosition = true;
            }

            GameStateManager.Instance.pendingDoorId = targetDoorId;
            GameStateManager.Instance.lastSceneBeforePuzzle = SceneManager.GetActiveScene().name;

            SceneManager.LoadScene(targetPuzzleScene);
        }
    }

    public KeyPromptDisplay ePromptDisplay; // Inspector’dan atanacak

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Kapı açıksa tekrar aktive olmasın
            if (GameStateManager.Instance != null &&
                GameStateManager.Instance.IsDoorOpen(targetDoorId))
            {
                if (ePromptDisplay != null) ePromptDisplay.HidePrompt();
                var collider = GetComponent<Collider>();
                if (collider != null)
                    collider.enabled = false;
                enabled = false;
                return;
            }
            playerNear = true;
            if (ePromptDisplay != null) ePromptDisplay.ShowPrompt();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            if (ePromptDisplay != null) ePromptDisplay.HidePrompt();
        }
    }

}