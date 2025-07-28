using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab;
    public int columns = 9;
    public int rows = 15;
    public float spacing = 2.5f;

    public List<Vector2Int> safePositions = new List<Vector2Int>()
    {
        new Vector2Int(0, 0),
        new Vector2Int(1, 1),
        new Vector2Int(2, 2),
        new Vector2Int(1, 3),
        new Vector2Int(0, 4),
        new Vector2Int(2, 5),
        new Vector2Int(1, 6),
        new Vector2Int(1, 7),
        new Vector2Int(1, 8)
    };

    void Start()
    {
        int offsetX = 4; // 5. platform 0,0,3.3 pozisyonunda olsun diye ofset

        for (int x = 0; x < columns; x++)
        {
            for (int z = 0; z < rows; z++)
            {
                Vector3 pos = new Vector3(
                    (x - offsetX) * spacing,
                    0,
                    z * spacing + 3.3f
                );

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
