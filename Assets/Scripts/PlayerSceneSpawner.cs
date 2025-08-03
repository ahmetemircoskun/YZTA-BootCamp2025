using UnityEngine;

public class PlayerSceneSpawner : MonoBehaviour
{
    void Awake()
    {
        // Sahnede fazladan Player varsa, onları yok et (çakışma olmasın)
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var obj in players)
        {
            if (obj != this.gameObject)
                Destroy(obj);
        }
        // Pozisyonla hiç oynamıyoruz! Editörde koyduğun yerde doğar.
    }
}
