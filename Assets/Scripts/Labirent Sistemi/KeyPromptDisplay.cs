using UnityEngine;

public class KeyPromptDisplay : MonoBehaviour
{
    public Transform targetObject;      // Üstünde göstermek istediğin obje (kapı)
    public GameObject promptPrefab;     // "E" butonu prefabı
    public Vector3 offset = new Vector3(0, 2, 0);

    private GameObject promptInstance;
    private Camera mainCamera;
    private bool playerNear = false;

    void Start()
    {
        mainCamera = Camera.main;
        promptInstance = Instantiate(promptPrefab);
        promptInstance.SetActive(false);
    }

    void Update()
    {
        if (promptInstance != null && promptInstance.activeSelf)
        {
            promptInstance.transform.position = targetObject.position + offset;
            promptInstance.transform.rotation = Quaternion.LookRotation(
                promptInstance.transform.position - mainCamera.transform.position,
                Vector3.up
            );
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            ShowPrompt();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            HidePrompt();
        }
    }

    public void ShowPrompt()
    {
        if (promptInstance != null)
            promptInstance.SetActive(true);
    }

    public void HidePrompt()
    {
        if (promptInstance != null)
            promptInstance.SetActive(false);
    }
}
