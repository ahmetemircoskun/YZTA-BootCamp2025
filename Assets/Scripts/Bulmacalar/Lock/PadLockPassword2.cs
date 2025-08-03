using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement; // Sahne yönetimi için

public class PadLockPassword2 : MonoBehaviour
{
    private MoveRuller _moveRull;
    private TimeBlinking _timeBlinking;
    public AudioClip puzzleSolvedSound; // Inspector’dan atanacak ses
    private AudioSource audioSource;

    [Range(0, 1)]
    public float solvedVolume = 1f;

    public int[] _numberPassword = { 0, 0, 0, 0 };

    public string nextSceneName = "NextScene"; // Inspector’dan hedef sahne adı

    private bool isSolved = false;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void Awake()
    {
        _moveRull = FindFirstObjectByType<MoveRuller>();
        _timeBlinking = FindFirstObjectByType<TimeBlinking>();
    }

    public void Password()
    {
        if (isSolved) return; // Zaten çözüldüyse tekrar işlem yapma

        if (_moveRull._numberArray.SequenceEqual(_numberPassword))
        {
            Debug.Log("Password correct");

            CheckSolution();

            // Emisyonları kapat
            for (int i = 0; i < _moveRull._rullers.Count; i++)
            {
                var emission = _moveRull._rullers[i].GetComponent<PadLockEmissionColor>();
                emission._isSelect = false;
                emission.BlinkingMaterial();
            }

            isSolved = true;

            // MoveRuller ve TimeBlinking’i devre dışı bırak
            if (_moveRull != null)
                _moveRull.enabled = false;
            if (_timeBlinking != null)
                _timeBlinking.enabled = false;

            // Doğru şifre girilince sahneyi değiştir
            SceneManager.LoadScene(nextSceneName);
        }
    }

    public void CheckSolution()
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
