using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PadLockPassword2 : MonoBehaviour
{
    private MoveRuller2 _moveRull;
    private TimeBlinking _timeBlinking;
    public AudioClip puzzleSolvedSound;
    private AudioSource audioSource;

    [Range(0, 1)]
    public float solvedVolume = 1f;

    public int[] _numberPassword = { 0, 0, 0, 0 };
    public string nextSceneName = "NextScene"; // Inspector’dan geçilecek sahne adı

    private bool isSolved = false;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void Awake()
    {
        _moveRull = FindFirstObjectByType<MoveRuller2>();
        _timeBlinking = FindFirstObjectByType<TimeBlinking>();
    }

    public void Password()
    {
        if (isSolved) return;

        if (_moveRull._numberArray.SequenceEqual(_numberPassword))
        {
            Debug.Log("Password correct");

            // Doğru şifre girildiğinde sahne geçişi:
            if (!string.IsNullOrEmpty(nextSceneName))
            {
                Debug.Log("Sahne geçişi: " + nextSceneName);
                SceneManager.LoadScene(nextSceneName);
            }

            // Ses ve efektler
            CheckSolution();

            // Emisyonları kapat
            for (int i = 0; i < _moveRull._rullers.Count; i++)
            {
                var emission = _moveRull._rullers[i].GetComponent<PadLockEmissionColor>();
                emission._isSelect = false;
                emission.BlinkingMaterial();
            }

            isSolved = true;

            if (_moveRull != null) _moveRull.enabled = false;
            if (_timeBlinking != null) _timeBlinking.enabled = false;
        }
    }

    public void CheckSolution()
    {
        Debug.Log("Doğru çözüldü!");

        if (puzzleSolvedSound != null)
            audioSource.PlayOneShot(puzzleSolvedSound, solvedVolume);
    }
}
