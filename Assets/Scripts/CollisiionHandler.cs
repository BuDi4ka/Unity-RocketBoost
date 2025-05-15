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
                Debug.Log("Finish collision detected!");
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

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
