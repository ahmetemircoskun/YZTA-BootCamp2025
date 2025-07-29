using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridGenerator : MonoBehaviour
{
    [Header("10x10 Grid (0 = random harf)")]
    public string[] letterGrid = new string[100];

    [Header("Prefab ve Ayarlar")]
    public GameObject letterCubePrefab;
    public float spacing = 1.2f;

    [Header("Anlamlı Kelimeler")]
    public List<string> validWords = new List<string>();

    private WordGameManager manager;

    private char[] turkceHarfler = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZ".ToCharArray();

void Start()
{
    manager = FindFirstObjectByType<WordGameManager>();  

    for (int y = 0; y < 10; y++)
    {
        for (int x = 0; x < 10; x++)
        {
            int index = y * 10 + x;
            string letter = letterGrid[index];
            if (string.IsNullOrEmpty(letter) || letter == "0")
            {
                letter = turkceHarfler[Random.Range(0, turkceHarfler.Length)].ToString();
            }

            Vector3 pos = new Vector3(x * spacing, 0, y * spacing);
            GameObject cube = Instantiate(letterCubePrefab, pos, Quaternion.identity, transform);

            cube.GetComponentInChildren<TextMeshPro>().text = letter.ToUpper();
            cube.name = $"Letter_{x}_{y}_{letter}";

            LetterCube lc = cube.GetComponent<LetterCube>();
            lc.letter = letter.ToUpper();
            lc.gridPos = new Vector2Int(x, y);
            lc.manager = manager;

            manager.RegisterCube(lc);
        }
    }
}

}
