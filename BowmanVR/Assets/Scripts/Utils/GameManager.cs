using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private string currentLevelName = string.Empty;

    List<AsyncOperation> loadOperations;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        loadOperations = new List<AsyncOperation>();

    }

    void OnLoadOperationComplete(AsyncOperation ao)
    {
        if(loadOperations.Contains(ao))
        {
            loadOperations.Remove(ao);
        }
        Debug.Log("Load Complete.");
    }
    void OnUnloadOperationComplete(AsyncOperation ao)
    {
        Debug.Log("Unload Complete.");
    }

    public void LoadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);
        if(ao == null)
        {
            Debug.LogError("[GameManager] Unable to load level " + levelName);
            return;
        }

        ao.completed += OnLoadOperationComplete;
        loadOperations.Add(ao);
        currentLevelName = levelName;
    }

    public void UnloadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);
        ao.completed += OnUnloadOperationComplete;
    }

    public void QuitApp()
    {
        Application.Quit();
    }

}
