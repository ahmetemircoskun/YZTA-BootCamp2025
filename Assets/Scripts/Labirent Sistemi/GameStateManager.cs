using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    private Dictionary<string, bool> doorStates = new Dictionary<string, bool>();
    [HideInInspector] public Vector3 lastPlayerPosition;
    [HideInInspector] public bool hasLastPosition;
    [HideInInspector] public string pendingDoorId;
    [HideInInspector] public string lastSceneBeforePuzzle;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetDoorOpen(string doorId)
    {
        doorStates[doorId] = true;
    }

    public bool IsDoorOpen(string doorId)
    {
        return doorStates.ContainsKey(doorId) && doorStates[doorId];
    }
}