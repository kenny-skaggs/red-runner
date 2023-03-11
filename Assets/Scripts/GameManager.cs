using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private Vector3 position;
    private string m_SceneName;
    private Dictionary<string, HashSet<string>> m_SceneStateMap = new Dictionary<string, HashSet<string>>();
    // key are scene names, value is a list of object names that should
    //     no longer be in the scene

    private void Awake()
    {
        if (Instance != null) {
            Destroy(gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnLevelFinishedLoading;
        }
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (m_SceneName == sceneName) {
            GameObject player = GameObject.Find("Player");
            player.transform.position = position;
        }

        HashSet<string> removedItemNames;
        m_SceneStateMap.TryGetValue(sceneName, out removedItemNames);
        if (removedItemNames != null) {
            foreach (var name in removedItemNames) {
                GameObject objectToRemove = GameObject.Find(name);
                Destroy(objectToRemove);
            }
        }
    }

    public void EnterDungeon(Vector3 playerPosition)
    {
        m_SceneName = SceneManager.GetActiveScene().name;
        position = playerPosition;
        SceneManager.LoadScene("SampleDungeon");
    }

    public void ExitDungeon()
    {
        SceneManager.LoadScene(m_SceneName);
    }

    public void RemoveFromScene(GameObject gameObject)
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (!m_SceneStateMap.ContainsKey(sceneName)) {
            m_SceneStateMap.Add(sceneName, new HashSet<string>());
        }
        m_SceneStateMap[sceneName].Add(gameObject.name);

        Destroy(gameObject);
    }
}
