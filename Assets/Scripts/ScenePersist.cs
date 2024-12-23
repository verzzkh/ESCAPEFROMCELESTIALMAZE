using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    void Awake()
    {
        int numScenePersists = FindObjectsOfType<ScenePersist>().Length;
        if (numScenePersists > 1)
        {
            Destroy(gameObject);
            Debug.Log("Extra ScenePersist destroyed.");
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Debug.Log("ScenePersist created and preserved.");
        }
    }

    public void ResetScenePersist()
    {
        Destroy(gameObject);
        Debug.Log("ScenePersist destroyed.");
    }
}
