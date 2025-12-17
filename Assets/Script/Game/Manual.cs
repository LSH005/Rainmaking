using UnityEngine;

public class Manual : MonoBehaviour
{
    public GameObject togglePanel;
    bool isActived = false;

    private void Start()
    {
        togglePanel.SetActive(false);
    }

    void Update()
    {
        if (isActived && !Pressing(KeyCode.Tab))
        {
            TogglePanel(false);
        }
        else if (!isActived && Pressing(KeyCode.Tab))
        {
            TogglePanel(true);
        }
    }

    void TogglePanel(bool on)
    {
        togglePanel.SetActive(on);
        isActived = on;
    }

    

    bool Pressing(KeyCode key) => Input.GetKey(key);
}
