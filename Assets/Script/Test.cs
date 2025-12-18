using UnityEngine;

public class Test : MonoBehaviour
{
    public static Test Instance { get; private set; }
    public bool isTest;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
