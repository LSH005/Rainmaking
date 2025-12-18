using UnityEditor.SceneTemplate;
using UnityEngine;

public class GamePause : MonoBehaviour
{
    public static bool isPaused = false;

    void Update()
    {
        if (Press(KeyCode.Escape))
        {
            PauseToggle(isPaused ? false : true);
        }
    }

    private void PauseToggle(bool pause)
    {
        Time.timeScale = pause ? 0.000000f : 1.0000000f;
        Cursor.lockState = pause ? CursorLockMode.None : CursorLockMode.Locked;
        isPaused = pause;
    }

    bool Press(KeyCode key) => Input.GetKeyDown(key);
}
