using UnityEngine;

public class CanvasInstance : MonoBehaviour
{
    public static CanvasInstance Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

}
