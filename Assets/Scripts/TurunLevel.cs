using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurunLevel : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(LoadPreviousLevel());
        }
    }

    IEnumerator LoadPreviousLevel()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);

        string currentSceneName = SceneManager.GetActiveScene().name;
        Debug.Log("Current Scene: " + currentSceneName);

        // Reset ScenePersist untuk setiap transisi level
        ResetScenePersistIfNeeded();

        // Hitung level sebelumnya berdasarkan indeks
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int previousSceneIndex = currentSceneIndex - 1;

        SceneManager.LoadScene(previousSceneIndex);
    }

    void ResetScenePersistIfNeeded()
    {
        ScenePersist scenePersist = FindObjectOfType<ScenePersist>();
        if (scenePersist != null)
        {
            scenePersist.ResetScenePersist();
            Debug.Log("ScenePersist reset for new level.");
        }
    }
}
