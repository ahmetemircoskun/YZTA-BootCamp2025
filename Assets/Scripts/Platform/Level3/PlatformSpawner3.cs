using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner3 : MonoBehaviour
{
    public GameObject platformPrefab;
    public int columns = 9;
    public int rows = 15;
    public float spacing = 5f;

    public List<Vector2Int> safePositions = new List<Vector2Int>()
    {
        new Vector2Int(4, 0),
        new Vector2Int(4, 1),
        new Vector2Int(4, 2),
        new Vector2Int(4, 3),
        new Vector2Int(4, 4),
        new Vector2Int(2, 5),
        new Vector2Int(4, 6),
        new Vector2Int(4, 8),
        new Vector2Int(4, 9),
        new Vector2Int(4, 10),
        new Vector2Int(4, 11),
        new Vector2Int(4, 12),
        new Vector2Int(4, 13),
        new Vector2Int(4, 14)
    };

    void Start()
    {
        int offsetX = 4;
        Vector3 sceneOffset = new Vector3(96.78f, -0.1f, 67f);

        for (int x = 0; x < columns; x++)
        {
            for (int z = 0; z < rows; z++)
            {
                // 7. satýr tamamen boþ kalsýn
                if (z == 7) continue;

                // Platformlarý sahneye yerleþtir
                Vector3 pos = new Vector3(
                    z * spacing,
                    0,
                    (x - offsetX) * spacing
                ) + sceneOffset;

                GameObject plat = Instantiate(platformPrefab, pos, Quaternion.identity);

                var controller = plat.GetComponent<PlatformController>();
                if (controller != null && safePositions.Contains(new Vector2Int(x, z)))
                {
                    controller.isSafe = true;
                }
            }
        }
    }
}
