using System.Linq;
using UnityEngine;

public class PadLockPassword : MonoBehaviour
{
    private MoveRuller _moveRull;
    private TimeBlinking _timeBlinking;
    public AudioClip puzzleSolvedSound; // Inspector’dan atanacak ses
    private AudioSource audioSource;

    [Range(0, 1)]
    public float solvedVolume = 1f;

    public int[] _numberPassword = { 0, 0, 0, 0 };

    private bool isSolved = false;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void Awake()
    {
        _moveRull = FindFirstObjectByType<MoveRuller>();
        _timeBlinking = FindFirstObjectByType<TimeBlinking>(); // Eğer tek bir TimeBlinking varsa
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

            // ARTIK AKSİYON ALINAMAZ:
            isSolved = true;

            // MoveRuller ve TimeBlinking’i devre dışı bırak
            if (_moveRull != null)
                _moveRull.enabled = false;
            if (_timeBlinking != null)
                _timeBlinking.enabled = false;
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
