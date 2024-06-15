using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public static MySceneManager instance;
    private Scene targetScene;

    private void Awake()
    {
        InitSingleton();
        LoadScene("Level_0");
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
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Level_0")
        {
            targetScene = scene;
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
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
