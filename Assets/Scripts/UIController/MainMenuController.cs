using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject creditsPanel; // Inspector'dan bağlanacak

    public void StartGame()
    {
        Debug.Log("Start butonuna basıldı");
        SceneManager.LoadScene("1-Giriş Avlusu");
    }

    public void CloseCredits()
    {
        Debug.Log("Credits paneli kapatıldı");
        creditsPanel.SetActive(false); // Paneli gizle
    }

    public void OpenCredits()
    {
        Debug.Log("Credits butonuna basıldı");
        creditsPanel.SetActive(true); // Paneli görünür yap
    }

    public void QuitGame()
    {
        Debug.Log("Quit butonuna basıldı");
        Application.Quit();
    }
}
