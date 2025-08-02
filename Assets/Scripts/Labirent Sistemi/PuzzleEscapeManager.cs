using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleEscapeManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            string returnScene = GameStateManager.Instance.lastSceneBeforePuzzle;
            if (!string.IsNullOrEmpty(returnScene))
            {
                SceneManager.LoadScene(returnScene);
            }
        }
    }
}
