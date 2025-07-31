using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner4_2 : MonoBehaviour
{
    public GameObject platformPrefab;
    public int columns = 9;
    public int rows = 15;
    public float spacing = 10f;

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
        Vector3 sceneOffset = new Vector3(110.18f, 0.14f, 286.14f);

        for (int x = 0; x < columns; x++)
        {
            for (int z = 0; z < rows; z++)
            {
                Vector3 oldPos = new Vector3(
                    z * spacing,
                    0,
                    (x - offsetX) * spacing
                );

                Vector3 pos = new Vector3(
                    oldPos.z,
                    0,
                    -oldPos.x
                ) + sceneOffset;

                GameObject plat = Instantiate(platformPrefab, pos, Quaternion.identity);

                var controller = plat.GetComponent<PlatformController4_2>();
                if (controller != null && safePositions.Contains(new Vector2Int(x, z)))
                {
                    controller.isSafe = true;
                }
            }
        }
    }
}