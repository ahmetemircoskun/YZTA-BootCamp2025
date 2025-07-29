using UnityEngine;

public class Candle : MonoBehaviour
{
    public GameObject flame; // Mumun alevi için referans
    public bool isLit = false;

    void Start()
    {
        flame.SetActive(isLit);
    }

    public void Toggle()
    {
        isLit = !isLit;
        flame.SetActive(isLit);

        // PuzzleManager'a info ver
        if (CandleManager.Instance != null)
            CandleManager.Instance.CheckSolution();
    }
}
