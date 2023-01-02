using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LoadScene : MonoBehaviour
{
    public GameObject LoadingScene;
    public GameObject Menu;
    public Slider slider;
    public Text loadingPercentage;
    public Text currentActivity;
    public float estimatedLoadingTime;
    
    public void LoadGame()
    {
        Debug.Log("Loading Game");
        StartCoroutine(LoadAsyncScene());
    }

    IEnumerator LoadAsyncScene()
    {
        Menu.SetActive(false);
        LoadingScene.SetActive(true);
        
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
        operation.allowSceneActivation = false;

        float loadingProgress = 0;
        float estimatedProgress;

        while ((loadingProgress % 60) <= estimatedLoadingTime)
        {
            estimatedProgress = Mathf.Clamp01(loadingProgress / estimatedLoadingTime);
            if ((Random.Range(0, 100)) > 80)
            {
                slider.value = estimatedProgress;
                loadingPercentage.text = (int)(estimatedProgress * 100) + "%";
            }
            
            String loadingStatus;
            loadingStatus = "Checking for existing save";

            if (estimatedProgress > 0.2f)
            {
                loadingStatus = "Setting time and position of the Sun.";
            }
            if (estimatedProgress > 0.3f)
            {
                loadingStatus = "Initializing the players body and stats.";
            }
            if (estimatedProgress > 0.4f)
            {
                loadingStatus = "Creating inventory";
            }
            if (estimatedProgress > 0.5f)
            {
                loadingStatus = "Initializing equipment of the player.";
            }
            if (estimatedProgress > 0.6f)
            {
                loadingStatus = "Loading magic abilities.";
            }
            if (estimatedProgress > 0.7f)
            {
                loadingStatus = "Loading Skill Points.";
            }
            if (estimatedProgress > 0.8f)
            {
                loadingStatus = "Initializing User Interface.";
            }
            if (estimatedProgress > 0.9f)
            {
                loadingStatus = "Loading loot.";
            }
            if (estimatedProgress > 0.95f)
            {
                loadingStatus = "Creating enemies.";
            }
            if (estimatedProgress > 0.96f)
            {
                loadingStatus = "Initializing the world.";
            }
            
            currentActivity.text = loadingStatus;
            
            if (estimatedProgress >= 0.985f)
            {
                Debug.Log("Game scene loaded!");
                operation.allowSceneActivation = true;
            }
            
            //wait for next frame
            yield return null;
            loadingProgress += Time.deltaTime;
        }
    }
}
