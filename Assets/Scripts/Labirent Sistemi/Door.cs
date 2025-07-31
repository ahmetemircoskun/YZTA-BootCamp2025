using UnityEngine;

public class Door : MonoBehaviour
{
    public string doorId;
    float addYRotation = -110f; // Yalnızca mevcut Y rotasyonuna eklenecek değer

    private void Start()
    {
        if (GameStateManager.Instance.IsDoorOpen(doorId))
        {
            OpenDoor();
        }
    }

    public void OpenDoor()
    {
        // Mevcut Y rotasyonuna belirli bir açı ekle
        Vector3 rot = transform.rotation.eulerAngles;
        rot.y += addYRotation;
        transform.rotation = Quaternion.Euler(rot);

        Debug.Log("Kapı açıldı: " + doorId);
    }
}
