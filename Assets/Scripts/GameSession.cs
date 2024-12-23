using UnityEngine;
using UnityEngine.SceneManagement;  // Untuk mengelola scene
using UnityEngine.UI;  // Untuk menangani UI

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 5;  // Ganti dengan 5 nyawa
    [SerializeField] int score = 0;

    [SerializeField] Text livesText;
    [SerializeField] Text scoreText; 

    private AudioManager audioManager;  // Tambahkan referensi ke AudioManager
   
    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        // Mengambil referensi AudioManager
        audioManager = FindObjectOfType<AudioManager>();
    }

 void Start() 
{
    livesText.text = playerLives.ToString();
    scoreText.text = score.ToString();    
    Debug.Log("GameSession: Start() dipanggil. Nyawa: " + playerLives + ", Skor: " + score);
}

 public void ProcessPlayerDeath()
{
    if (playerLives > 1)
    {
        TakeLife();
    }
    else
    {
        // Langsung menuju menu utama jika pemain mati
        ResetGameSession();
    }
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
    playerLives = 5;
    score = 0;
    Debug.Log("GameSession: Nyawa dan skor di-reset.");

    // Reset sesi game
    FindObjectOfType<ScenePersist>()?.ResetScenePersist();
    
    // Hancurkan semua objek DontDestroyOnLoad
    PauseMenu.RegisterDontDestroyOnLoad(gameObject);
    GameObject[] dontDestroyObjects = GameObject.FindGameObjectsWithTag("PersistentObject");
    foreach (GameObject obj in dontDestroyObjects)
    {
        Destroy(obj);
    }

    // Muat ulang Main Menu
    SceneManager.LoadScene("MainMenu");
    Destroy(gameObject); // Hancurkan GameSession sendiri
}





    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = score.ToString(); 
    }

    void TakeLife()
    {
        playerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);  // Reload the current level
        livesText.text = playerLives.ToString();
    }

}
