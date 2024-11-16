using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ScenePreloader : MonoBehaviour
{
    public static ScenePreloader Instance;
    // Array of scene names to preload
    public string[] scenesToPreload;

    // Loading screen UI elements
    public GameObject loadingScreen;
    public TextMeshProUGUI loadingText;
    public Slider loadingProgressBar;

    // Dictionary to keep track of async operations for each scene
    private Dictionary<string, AsyncOperation> preloadOperations = new Dictionary<string, AsyncOperation>();

    private void Start()
    {
        // Start the preloading process
        StartCoroutine(PreloadScenes());
        Instance = this;
    }

    private IEnumerator PreloadScenes()
    {
        loadingScreen.SetActive(true);

        foreach (string sceneName in scenesToPreload)
        {
            if (Application.CanStreamedLevelBeLoaded(sceneName))
            {
                // Load the scene additively in the background
                AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
                asyncLoad.allowSceneActivation = false; // Don't activate the scene immediately
                preloadOperations[sceneName] = asyncLoad;

                // Wait until the scene is fully loaded
                while (!asyncLoad.isDone)
                {
                    // Update the loading screen UI
                    float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
                    loadingText.text = "Loading " + sceneName + ": " + (progress * 100).ToString("F0") + "%";
                    loadingProgressBar.value = progress;

                    // Check if the loading is done
                    if (asyncLoad.progress >= 0.9f)
                    {
                        loadingProgressBar.value = 100f;
                        break;
                    }
                    yield return null;
                }

                Debug.Log("Scene " + sceneName + " preloaded.");
            }
            else
            {
                Debug.LogError("Scene " + sceneName + " cannot be preloaded. Check if it's in the Build Settings.");
            }
        }
        Debug.Log("All scenes preloaded.");
        //Testing purposes
        StartCoroutine(LoadLevelAfterTime(100));
    }

    // Method to activate a preloaded scene
    public void ActivateScene(string sceneName)
    {
        if (preloadOperations.TryGetValue(sceneName, out AsyncOperation asyncLoad))
        {
            asyncLoad.allowSceneActivation = true;
            StartCoroutine(WaitForActivation(sceneName));
        }
        else
        {
            Debug.LogError("Scene " + sceneName + " is not preloaded.");
        }
    }

    private IEnumerator WaitForActivation(string sceneName)
    {
        // Wait until the scene is fully activated
        AsyncOperation asyncLoad = preloadOperations[sceneName];
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Set the newly activated scene as the active scene
        Scene scene = SceneManager.GetSceneByName(sceneName);
        if (scene.IsValid())
        {
            SceneManager.SetActiveScene(scene);
            Debug.Log("Scene " + sceneName + " activated and set as active.");
        }
        else
        {
            Debug.LogError("Scene " + sceneName + " is not valid after activation.");
        }
    }

    private IEnumerator LoadLevelAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        ActivateScene(scenesToPreload[0]);
        loadingScreen.SetActive(false);
    }
