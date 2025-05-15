using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisiionHandler : MonoBehaviour
{
    [SerializeField] Movement playerMovement; // Reference to the Movement script
    [SerializeField] private float reloadDelay = 2f;

    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly collision detected!");
                break;
            case "Finish":
                LoadNextLevel();
                break;
            case "Fuel":
                Debug.Log("Fuel collision detected!");
                break;      
            default:
                //Destroy(gameObject);
                //ReloadLevel();
                playerMovement.DisableInput(); // Disable player movement input
                Debug.Log("Player input disabled.");
                Invoke(nameof(ReloadLevel), reloadDelay);
                break;
        }
    }

    void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;

        if (nextScene < SceneManager.sceneCountInBuildSettings)
    {
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            Debug.Log("No more levels to load");
        }
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
