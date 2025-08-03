using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Card : MonoBehaviour
{
    public enum CardType { A, B }
    public CardType cardType;

    private bool isRevealed = false;
    private Renderer rend;

    private Quaternion faceDownRotation;
    private Quaternion faceUpRotation;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = Color.gray;

        faceDownRotation = Quaternion.Euler(0, 0, 0);
        faceUpRotation = Quaternion.Euler(180, 0, 0); // X ekseninde 180 derece dönme
    }

    private void OnMouseDown()
    {
        if (isRevealed) return;

        isRevealed = true;
        StartCoroutine(FlipToFront());
        GridManager.Instance.CardSelected(this);
    }

    IEnumerator FlipToFront()
    {
        float duration = 0.5f;
        float time = 0f;

        while (time < duration)
        {
            float t = time / duration;
            transform.rotation = Quaternion.Slerp(faceDownRotation, faceUpRotation, t);

            if (t > 0.5f)
            {
                rend.material.color = (cardType == CardType.A) ? Color.blue : Color.red;
            }

            time += Time.deltaTime;
            yield return null;
        }

        transform.rotation = faceUpRotation;
    }

    public void Hide()
    {
        StartCoroutine(FlipBack());
    }

    IEnumerator FlipBack()
    {
        float duration = 0.5f;
        float time = 0f;

        while (time < duration)
        {
            float t = time / duration;
            transform.rotation = Quaternion.Slerp(faceUpRotation, faceDownRotation, t);

            if (t > 0.5f)
            {
                rend.material.color = Color.gray;
            }

            time += Time.deltaTime;
            yield return null;
        }

        transform.rotation = faceDownRotation;
        isRevealed = false;
    }

    public CardType GetCardType()
    {
        return cardType;
    }

    public bool IsRevealed()
    {
        return isRevealed;
    }

    // DOĞRU DURUMDA ÇALIŞACAK FONKSİYON:
    public static void CheckSolutionStatic()
    {
        Debug.Log("Doğru çözüldü!");

        if (PuzzleManager.Instance != null)
        {
            PuzzleManager.Instance.PuzzleSolved();
        }
    }
}
