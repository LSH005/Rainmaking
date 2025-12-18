using UnityEngine;

public class Manual : MonoBehaviour
{
    public GameObject togglePanel;
    public GameObject text;
    bool isActived = false;

    private void Start()
    {
        togglePanel.SetActive(false);
    }

    void Update()
    {
        if ((isActived && !Pressing(KeyCode.Tab)) || ReleaseConditions())
        {
            TogglePanel(false);
        }
        else if (!isActived && Pressing(KeyCode.Tab) && !ReleaseConditions())
        {
            TogglePanel(true);
        }

        text.SetActive(!ReleaseConditions());
    }

    void TogglePanel(bool on)
    {
        togglePanel.SetActive(on);
        isActived = on;
    }

    
    bool ReleaseConditions()
    {
        return ScreenTransition.isTransitioning || GamePause.isPaused;
    }

    bool Pressing(KeyCode key) => Input.GetKey(key);
}
