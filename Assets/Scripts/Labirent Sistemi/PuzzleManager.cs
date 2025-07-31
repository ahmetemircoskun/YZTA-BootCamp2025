using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void PuzzleSolved()
    {
        Debug.Log("Bulmaca çözüldü!");

        string doorId = GameStateManager.Instance.pendingDoorId;
        GameStateManager.Instance.SetDoorOpen(doorId);

        StartCoroutine(ReturnToMainScene());
    }

    private IEnumerator ReturnToMainScene()
    {
        yield return new WaitForSeconds(2f);

        string returnScene = GameStateManager.Instance.lastSceneBeforePuzzle;
        if (!string.IsNullOrEmpty(returnScene))
        {
            SceneManager.LoadScene(returnScene);
        }
        else
        {
            Debug.LogError("Geri dönülecek sahne adı boş!");
        }
    }
}