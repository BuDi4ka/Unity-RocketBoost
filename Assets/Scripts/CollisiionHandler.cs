using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisiionHandler : MonoBehaviour
{
    [SerializeField] Movement playerMovement; // Reference to the Movement script
    [SerializeField] private float reloadDelay = 2f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip finishSound;
    [SerializeField] float soundVolume = 1f;
    [SerializeField] ParticleSystem finishParticles;
    [SerializeField] ParticleSystem crashParticles;

    bool isTransitioning = false;

    private void OnCollisionEnter(Collision other)                                                                                                                                                                                                                                                                                                                                                                                                                                                         
    {
        if (isTransitioning) { return; } 
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly collision detected!");
                break;
            case "Finish":
                StartFinishSequence();  
                break;
            case "Fuel":
                Debug.Log("Fuel collision detected!");
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        playerMovement.DisableInput();
        Debug.Log("Collided");
        AudioSource.PlayClipAtPoint(crashSound, transform.position, soundVolume);
        crashParticles.Play();
        Invoke(nameof(ReloadLevel), reloadDelay);
    }

    void StartFinishSequence()
    {
        isTransitioning = true;
        playerMovement.DisableInput();
        Debug.Log("Finish");
        AudioSource.PlayClipAtPoint(finishSound, transform.position, soundVolume);
        finishParticles.Play(); 
        Invoke(nameof(LoadNextLevel), reloadDelay);
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
