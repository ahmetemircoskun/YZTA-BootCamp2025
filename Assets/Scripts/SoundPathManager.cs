using UnityEngine;

public class SoundPathManager : MonoBehaviour
{
    public GameObject[] soundRooms;
    public float maxDistance = 30f;

    private Transform playerTransform;
    private int currentClosestRoomIndex = -1;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            playerTransform = playerObj.transform;
        else
            Debug.LogWarning("Player bulunamadý!");

        // Bütün sesleri baþlat, ama volume 0 olsun
        foreach (var room in soundRooms)
        {
            var audio = room.GetComponent<AudioSource>();
            if (audio != null)
            {
                audio.loop = true;
                audio.volume = 0f;
                audio.Play();
            }
        }
    }

    void Update()
    {
        if (playerTransform == null) return;

        float closestDistance = Mathf.Infinity;
        int closestRoomIndex = -1;

        // En yakýn odayý bul
        for (int i = 0; i < soundRooms.Length; i++)
        {
            float dist = Vector3.Distance(playerTransform.position, soundRooms[i].transform.position);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                closestRoomIndex = i;
            }
        }

        // Sesleri güncelle
        for (int i = 0; i < soundRooms.Length; i++)
        {
            var audio = soundRooms[i].GetComponent<AudioSource>();
            if (audio == null) continue;

            if (i == closestRoomIndex && closestDistance <= maxDistance)
            {
                float t = Mathf.Clamp01(closestDistance / maxDistance);
                audio.volume = 1f - t;
                // audio.pitch = 1f; // Gerek yok, sabit kalýr
            }
            else
            {
                audio.volume = 0f;
            }
        }
    }
}
