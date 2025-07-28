using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WordGameManager : MonoBehaviour
{
    private List<LetterCube> selectedLetters = new List<LetterCube>();
    private List<LetterCube> allCubes = new List<LetterCube>();
    private List<string> foundWords = new List<string>();

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
            foreach (var cube in selectedLetters)
            {
                cube.MarkAsFound();
            }

            foundWords.Add(word);
            selectedLetters.Clear();

            if (foundWords.Count == grid.validWords.Count)
                Debug.Log("Tüm kelimeler bulundu!");
            break;
        }
    }
}

}
