using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;
    public static T Instance { get { return instance; } }

    protected virtual void Awake()
    {
        // Check if the instance already exists
        if (instance != null && this.gameObject != null)
        {
            Destroy(this.gameObject);
            return; // Exit the method if we destroy the object
        }
        else
        {
            instance = (T)this;
        }

        // Make sure the object persists across scenes
        if (!gameObject.transform.parent)
        {
            DontDestroyOnLoad(gameObject);
        }

        // Destroy the instance if we are in Scene 0 or Scene 1
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (sceneIndex == 0 || sceneIndex == 1)
        {
            Destroy(gameObject);
            instance = null; // Reset the instance to null
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        int sceneIndex = scene.buildIndex;
        if (sceneIndex == 0 || sceneIndex == 1)
        {
            Destroy(gameObject);
            instance = null; // Reset the instance to null
        }
    }
}
