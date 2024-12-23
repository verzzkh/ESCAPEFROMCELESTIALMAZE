using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
public void PlayGame()
{
    // Hancurkan instance GameSession lama, jika ada
    GameSession oldGameSession = FindObjectOfType<GameSession>();
    if (oldGameSession != null)
    {
        Destroy(oldGameSession.gameObject);
    }

    // Muat Level Pertama
    SceneManager.LoadScene("SampleScene");
}

    public void QuitGame()
    {
        // Log a message in the editor to confirm Quit works, only applies in editor
        Debug.Log("Quit Game");

        // Exit the application in a built version
        Application.Quit();
    }
}
