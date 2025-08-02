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

    private Transform correctCup;
    private bool canSelect = false;
    private bool gameStarted = false;

    public AudioClip puzzleSolvedSound;
    public AudioClip wrongSound;
    private AudioSource audioSource;

    [Range(0, 1)]
    public float solvedVolume = 1f;
    [Range(0, 1)]
    public float wrongVolume = 1f;


    void Start()
    {

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        cup1StartPos = cup1.position;
        cup1UpPos = cup1StartPos + Vector3.up * 1f;

        ballStartPos = ballRb.position;
        ballTargetPos = new Vector3(cup1.position.x, ballRb.position.y, cup1.position.z);

        cups.Add(cup0);
        cups.Add(cup1);
        cups.Add(cup2);
        cups.Add(cup3);

        correctCup = cup1;

        StartCoroutine(StartGameSequence());
    }

    IEnumerator StartGameSequence()
    {
        canSelect = false;

        float duration = 1f;
        float elapsed = 0f;

        // cup1 yukarı kaldır
        while (elapsed < duration)
        {
            cup1.position = Vector3.Lerp(cup1StartPos, cup1UpPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        cup1.position = cup1UpPos;

        // Top cup1'in altına hareket ediyor
        elapsed = 0f;
        while (elapsed < duration)
        {
            Vector3 newPos = Vector3.Lerp(ballStartPos, ballTargetPos, elapsed / duration);
            ballRb.MovePosition(newPos);
            elapsed += Time.deltaTime;
            yield return null;
        }
        ballRb.MovePosition(ballTargetPos);

        // cup1 aşağı iniyor
        elapsed = 0f;
        while (elapsed < duration)
        {
            cup1.position = Vector3.Lerp(cup1UpPos, cup1StartPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        cup1.position = cup1StartPos;

        yield return new WaitForSeconds(0.5f);

        // topun pozisyonu cup1 altında, parent yap
        ballRb.transform.position = cup1.position + new Vector3(0, -1.12f, 0);
        ballRb.transform.SetParent(cup1, true);

        yield return ShuffleCups();

        Debug.Log("Karıştırma tamamlandı.");
        canSelect = true;
        gameStarted = true;
    }

    IEnumerator ShuffleCups()
    {
        canSelect = false;

        for (int i = 0; i < shuffleCount; i++)
        {
            int a = Random.Range(0, cups.Count);
            int b;
            do { b = Random.Range(0, cups.Count); } while (b == a);

            Vector3 posA = cups[a].position;
            Vector3 posB = cups[b].position;

            Vector3 center = (posA + posB) / 2f;
            float radius = Vector3.Distance(posA, posB) / 2f;
            float y = posA.y;

            float startAngleA = Mathf.Atan2(posA.z - center.z, posA.x - center.x);
            float startAngleB = Mathf.Atan2(posB.z - center.z, posB.x - center.x);

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

        correctCup = ballRb.transform.parent;

        canSelect = true;
    }

    Vector3 CircleArc(Vector3 center, float radius, float angle, float y)
    {
        float x = center.x + radius * Mathf.Cos(angle);
        float z = center.z + radius * Mathf.Sin(angle);
        return new Vector3(x, y, z);
    }

    void Update()
    {
        if (!canSelect || !gameStarted) return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 10f))
            {
                Transform clickedCup = hit.transform;

                int index = cups.IndexOf(clickedCup);
                if (index != -1)
                {
                    PlayerSelectCup(index);
                }
            }
        }
    }

    public void PlayerSelectCup(int selectedCupIndex)
    {
        if (!canSelect)
        {
            Debug.Log("Seçim yapamazsın, bardaklar karışıyor.");
            return;
        }

        canSelect = false;

        if (cups[selectedCupIndex] == correctCup)
        {
            Debug.Log("Tebrikler! Doğru bardağı seçtin.");

            // Top parentlıktan çıkar
            ballRb.transform.SetParent(null);

            StartCoroutine(LiftCorrectCup(cups[selectedCupIndex]));
            CheckSolution();
        }
        else
        {
            Debug.Log("Yanlış seçim! Doğru bardak gösteriliyor...");
            audioSource.PlayOneShot(wrongSound, wrongVolume);
            StartCoroutine(RestartShuffle());
        }
    }

    IEnumerator LiftCorrectCup(Transform cup)
    {
        float duration = 1f;
        Vector3 startPos = cup.position;
        Vector3 upPos = startPos + Vector3.up * 1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            cup.position = Vector3.Lerp(startPos, upPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        cup.position = upPos;
    }

    IEnumerator RestartShuffle()
    {
        // Topun bulunduğu bardağı belirle
        correctCup = ballRb.transform.parent;

        // Topu bardaktan ayır (bardak yukarı kalkınca top sabit kalır)
        ballRb.transform.SetParent(null);

        // Doğru bardağı kaldır
        yield return LiftCorrectCup(correctCup);

        yield return new WaitForSeconds(1f);

        // Doğru bardağı tekrar indir
        Vector3 startPos = correctCup.position;
        Vector3 downPos = startPos - Vector3.up * 1f;
        float duration = 1f;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            correctCup.position = Vector3.Lerp(startPos, downPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        correctCup.position = downPos;

        // Topu tekrar bardağın altına ve parent yap
        ballRb.transform.position = correctCup.position + new Vector3(0, -1.12f, 0);
        ballRb.transform.SetParent(correctCup, true);

        yield return new WaitForSeconds(0.5f);

        yield return ShuffleCups();

        Debug.Log("Karıştırma tamamlandı. Tekrar seçim yapabilirsin.");
        canSelect = true;
    }

    public void CheckSolution()
    {
        if (true)
        {
            Debug.Log("Doğru çözüldü!");

            if (puzzleSolvedSound != null)
            {
                audioSource.PlayOneShot(puzzleSolvedSound, solvedVolume);
            }

            if (PuzzleManager.Instance != null)
            {
                PuzzleManager.Instance.PuzzleSolved();
            }
        }
    }


}
