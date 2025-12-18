using UnityEngine;

public class Portal : MonoBehaviour
{
    public float targetY = -100f;
    public string nextSceneName;

    Transform player;

    private void Start()
    {
        player = PlayerCamera.Instance.transform;
    }

    private void Update()
    {
        if (player == null)
        {
            this.enabled = false;
            Destroy(gameObject);
            return;
        }

        if (player.position.y <= targetY)
        {
            ScreenTransition.JustLoadScene(nextSceneName, "LoadingScene");
            this.enabled = false;
            Destroy(gameObject);
        }
    }
}
