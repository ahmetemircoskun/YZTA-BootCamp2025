using UnityEngine;
using System.Collections;

public class CandleManager : MonoBehaviour
{
    public static CandleManager Instance;

    public Candle[] candles;
    public bool[] correctCombination;

    public Transform door;

    private Quaternion closedRotation;
    private Quaternion openRotation;

    public float rotationAngleY = 90f;
    public float rotateSpeed = 2f;  

    private Coroutine doorCoroutine;

    void Awake()
    {
        Instance = this;

        closedRotation = door.rotation;
        openRotation = Quaternion.Euler(door.eulerAngles + new Vector3(0, rotationAngleY, 0));
    }

    public void CheckSolution()
    {
        if (candles.Length != correctCombination.Length)
        {
            return;
        }

        for (int i = 0; i < candles.Length; i++)
        {
            if (candles[i].isLit != correctCombination[i])
            {
                // Yanlış kombinasyonsa kapıyı kapat
                if (doorCoroutine != null) StopCoroutine(doorCoroutine);
                doorCoroutine = StartCoroutine(RotateDoor(closedRotation));
                return;
            }
        }

        // Doğru kombinasyonsa kapıyı aç
        if (doorCoroutine != null) StopCoroutine(doorCoroutine);
        doorCoroutine = StartCoroutine(RotateDoor(openRotation));
    }

    IEnumerator RotateDoor(Quaternion targetRotation)
    {
        while (Quaternion.Angle(door.rotation, targetRotation) > 0.1f)
        {
            door.rotation = Quaternion.Slerp(door.rotation, targetRotation, Time.deltaTime * rotateSpeed);
            yield return null;
        }
        door.rotation = targetRotation;
    }
}
