using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MySceneManager : MonoBehaviour
{
    public static MySceneManager instance;
    [SerializeField] private Image losePanel;
    [SerializeField] private LifeManager lifeManager;

    private Scene targetScene;

    private void Awake()
    {
        InitSingleton();
        LoadScene(1);
    }

    private void InitSingleton()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void LoadScene(int buildIndex)
    {
        SceneManager.LoadSceneAsync(buildIndex, LoadSceneMode.Additive);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "PersistentScene")
        {
            lifeManager.RenderHearths();
            targetScene = scene;
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    public void LoadNextLevel(int index)
    {
        LoadScene(index);
    }
    public void RestartGame()
    {
        losePanel.gameObject.SetActive(false);
        int sceneBuildIndex = targetScene.buildIndex;
        SceneManager.UnloadSceneAsync(sceneBuildIndex);
        LoadScene(1);
    }

    public T InstantiateInTargetScene<T>(T prefab, Vector3 position, Quaternion rotation) where T : Component
    {
        T obj = Instantiate(prefab, position, rotation);
        if (targetScene.IsValid())
        {
            SceneManager.MoveGameObjectToScene(obj.gameObject, targetScene);
        }
        return obj;
    }
}
