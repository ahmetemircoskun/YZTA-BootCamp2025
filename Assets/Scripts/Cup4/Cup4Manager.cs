using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cup4Manager : MonoBehaviour
{
    public Transform cup0;
    public Transform cup1;
    public Transform cup2;
    public Transform cup3;
    public Rigidbody ballRb;

    public int shuffleCount = 5;
    public float shuffleDuration = 1f;

    private Vector3 cup1StartPos;
    private Vector3 cup1UpPos;

    private Vector3 ballStartPos;
    private Vector3 ballTargetPos;

    private List<Transform> cups = new List<Transform>();

    private void Start()
    {
        cup1StartPos = cup1.position;
        cup1UpPos = cup1StartPos + Vector3.up * 1f;

        ballStartPos = ballRb.position;
        ballTargetPos = new Vector3(cup1.position.x, ballRb.position.y, cup1.position.z);

        cups.Add(cup0);
        cups.Add(cup1);
        cups.Add(cup2);
        cups.Add(cup3);

        StartCoroutine(StartGameSequence());
    }

    IEnumerator StartGameSequence()
    {
        float duration = 1f;
        float elapsed = 0f;

        // Cup1 yukarý kaldýr
        while (elapsed < duration)
        {
            cup1.position = Vector3.Lerp(cup1StartPos, cup1UpPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        cup1.position = cup1UpPos;

        // Top cup1'in altýna Z ekseninde hareket ediyor
        elapsed = 0f;
        while (elapsed < duration)
        {
            Vector3 newPos = Vector3.Lerp(ballStartPos, ballTargetPos, elapsed / duration);
            ballRb.MovePosition(newPos);
            elapsed += Time.deltaTime;
            yield return null;
        }
        ballRb.MovePosition(ballTargetPos);

        // Cup1 aþaðý iniyor
        elapsed = 0f;
        while (elapsed < duration)
        {
            cup1.position = Vector3.Lerp(cup1UpPos, cup1StartPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        cup1.position = cup1StartPos;

        yield return new WaitForSeconds(1f);

        // Topun pozisyonunu bardak altýna ayarla ve parent yap
        ballRb.transform.position = cup1.position + new Vector3(0, -0.4f, 0);
        ballRb.transform.SetParent(cup1, true);

        // Shuffle baþlýyor
        yield return ShuffleCups();

        Debug.Log("Shuffle tamamlandý. Oyuncu seçim yapabilir.");
    }

    Vector3 CircleArc(Vector3 center, float radius, float angle, float y)
    {
        float x = center.x + radius * Mathf.Cos(angle);
        float z = center.z + radius * Mathf.Sin(angle);
        return new Vector3(x, y, z);
    }

    IEnumerator ShuffleCups()
    {
        for (int i = 0; i < shuffleCount; i++)
        {
            int a = Random.Range(0, 4);
            int b;
            do { b = Random.Range(0, 4); } while (b == a);

            Vector3 posA = cups[a].position;
            Vector3 posB = cups[b].position;

            Vector3 center = (posA + posB) / 2f;
            float radius = Vector3.Distance(posA, posB) / 2f;
            float y = posA.y; // Y sabit

            // Baþlangýç açýlarýný hesapla
            float startAngleA = Mathf.Atan2(posA.z - center.z, posA.x - center.x);
            float startAngleB = Mathf.Atan2(posB.z - center.z, posB.x - center.x);

            // Burada biri pozitif yönde, diðeri negatif yönde dönecek

            float elapsed = 0f;
            while (elapsed < shuffleDuration)
            {
                float t = elapsed / shuffleDuration;

                float angleA = Mathf.LerpAngle(startAngleA * Mathf.Rad2Deg, (startAngleA * Mathf.Rad2Deg) + 180f, t) * Mathf.Deg2Rad;
                float angleB = Mathf.LerpAngle(startAngleB * Mathf.Rad2Deg, (startAngleB * Mathf.Rad2Deg) - 180f, t) * Mathf.Deg2Rad;

                cups[a].position = CircleArc(center, radius, angleA, y);
                cups[b].position = CircleArc(center, radius, angleB, y);

                elapsed += Time.deltaTime;
                yield return null;
            }

            cups[a].position = posB;
            cups[b].position = posA;

            var temp = cups[a];
            cups[a] = cups[b];
            cups[b] = temp;

            yield return new WaitForSeconds(0.1f);
        }
    }
}
