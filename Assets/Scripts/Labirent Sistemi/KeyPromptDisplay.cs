using UnityEngine;

public class KeyPromptDisplay : MonoBehaviour
{
    public Transform targetObject;      // Üstünde göstermek istediğin obje (kapı)
    public GameObject promptPrefab;     // "E" butonu prefabı
    public Vector3 offset = new Vector3(0, 2, 0); // Yükseklik ayarı

    private GameObject promptInstance;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        promptInstance = Instantiate(promptPrefab);
        promptInstance.SetActive(false); // Başta görünmesin
    }

    void Update()
    {
        if (promptInstance != null && promptInstance.activeSelf)
        {
            // Promptu targetObject'in üstüne konumlandır
            promptInstance.transform.position = targetObject.position + offset;

            // Canvas'ı her zaman kameraya düzgün baksın (ters durmasın)
            promptInstance.transform.rotation = Quaternion.LookRotation(
                promptInstance.transform.position - mainCamera.transform.position,
                Vector3.up
            );
        }
    }


    public void ShowPrompt()
    {
        promptInstance.SetActive(true);
    }

    public void HidePrompt()
    {
        promptInstance.SetActive(false);
    }
}
