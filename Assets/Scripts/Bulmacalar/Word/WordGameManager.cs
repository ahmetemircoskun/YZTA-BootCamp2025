using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WordGameManager : MonoBehaviour
{
    private List<LetterCube> selectedLetters = new List<LetterCube>();
    private List<LetterCube> allCubes = new List<LetterCube>();
    private List<string> foundWords = new List<string>();

    public AudioClip puzzleSolvedSound;
    public AudioClip checkSound;
    private AudioSource audioSource;

    [Range(0, 1)]
    public float solvedVolume = 1f;

    [Range(0, 1)]
    public float checkVolume = 1f;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void RegisterCube(LetterCube cube)
    {
        allCubes.Add(cube);
    }

    public void AddSelectedLetter(LetterCube cube)
    {
        selectedLetters.Add(cube);
        CheckWords();
    }

    public void RemoveSelectedLetter(LetterCube cube)
    {
        selectedLetters.Remove(cube);
    }

    void CheckWords()
    {
        if (selectedLetters.Count < 2) return;

        string combined = string.Concat(selectedLetters.Select(c => c.letter));
        string reversed = new string(combined.Reverse().ToArray());

        GridGenerator grid = FindFirstObjectByType<GridGenerator>();  // GÜNCELLENDİ

        foreach (string word in grid.validWords)
        {
            if (combined == word || reversed == word)
            {
                Debug.Log($"Kelime bulundu: {word}");
                audioSource.PlayOneShot(checkSound, checkVolume);

                foreach (var cube in selectedLetters)
                {
                    cube.MarkAsFound();
                }

                foundWords.Add(word);
                selectedLetters.Clear();

                if (foundWords.Count == grid.validWords.Count)
                {
                    Debug.Log("Tüm kelimeler bulundu!");
                    audioSource.PlayOneShot(puzzleSolvedSound, solvedVolume);
                    CheckSolution();
                    break;
                }
            }
        }
    }

    public int GetFoundWordCount()
    {
        return foundWords.Count;
    }

    public int GetTotalWordCount()
    {
        GridGenerator grid = FindFirstObjectByType<GridGenerator>();
        return grid != null ? grid.validWords.Count : 0;
    }

    public static void CheckSolution()
    {
        Debug.Log("Doğru çözüldü!");

        if (PuzzleManager.Instance != null)
        {
            PuzzleManager.Instance.PuzzleSolved();
        }
    }

}
