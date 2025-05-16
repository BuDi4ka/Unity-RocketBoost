using UnityEngine;
using UnityEngine.InputSystem;  // <-- new input system namespace

public class QuitOnEscape : MonoBehaviour
{
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Application.Quit();

            // For editor testing:
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        }
    }
}
