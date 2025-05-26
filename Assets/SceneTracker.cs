using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTracker : MonoBehaviour
{
    public static int previousSceneIndex = -1;

    private void Awake()
    {
        // SceneTracker sahnede baþka varsa sil, sadece bir tane olsun
        if (FindObjectsOfType<SceneTracker>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnSceneUnloaded(Scene scene)
    {
        previousSceneIndex = scene.buildIndex;
    }
}
