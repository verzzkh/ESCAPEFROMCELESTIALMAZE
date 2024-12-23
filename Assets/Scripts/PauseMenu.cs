using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool isPaused = false;
    private static List<GameObject> dontDestroyObjects = new List<GameObject>();

    private AudioManager audioManager;

    void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void LoadMainMenu()
    {
        // Panggil fungsi untuk membuat karakter mati (seperti jika karakter mati)
        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        if (playerMovement != null)
        {
            Debug.Log("Player ditemukan, memanggil fungsi TriggerDeath.");
            playerMovement.TriggerDeath();  // Panggil metode TriggerDeath untuk menghentikan pergerakan player
        }

        // Reset sesi game dan hancurkan objek-objek dontDestroyOnLoad
        ResetGameSession();
    }

    public void ResetGameSession()
    {
        Time.timeScale = 1f;  // Pastikan waktu kembali normal
        Debug.Log("Time.timeScale di-reset ke 1.");

        // Matikan musik jika ada
        if (audioManager != null)
        {
            audioManager.StopMusic();
        }

        // Reset nilai nyawa dan skor
        GameSession gameSession = FindObjectOfType<GameSession>();
        if (gameSession != null)
        {
            gameSession.ResetGameSession();
        }

        // Hancurkan semua objek DontDestroyOnLoad yang sudah terdaftar
        ResetDontDestroyOnLoadObjects();

        // Muat ulang Main Menu
        SceneManager.LoadScene("MainMenu");
    }

    private void ResetDontDestroyOnLoadObjects()
    {
        // Hancurkan semua objek yang telah ditambahkan ke dontDestroyObjects
        foreach (GameObject obj in dontDestroyObjects)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
        dontDestroyObjects.Clear(); // Kosongkan daftar setelah reset
    }

    public static void RegisterDontDestroyOnLoad(GameObject obj)
    {
        dontDestroyObjects.Add(obj);
        DontDestroyOnLoad(obj);
    }
}
