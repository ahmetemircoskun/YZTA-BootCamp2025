using UnityEngine;

public class PlayerRespawner : MonoBehaviour
{
    void Start()
    {
        if (GameStateManager.Instance != null && GameStateManager.Instance.hasLastPosition)
        {
            transform.position = GameStateManager.Instance.lastPlayerPosition;
        }
    }
}
