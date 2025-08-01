using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleTrigger : MonoBehaviour
{
    public string targetDoorId;
    public string targetPuzzleScene;

    private bool playerNear = false;

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