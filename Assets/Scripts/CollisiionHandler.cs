using UnityEngine;

public class CollisiionHandler : MonoBehaviour
{
    [SerializeField] Movement playerMovement; // Reference to the Movement script


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
                playerMovement.DisableInput(); // Disable player movement input
                //Destroy(gameObject);
                break;
        }
    }
}
