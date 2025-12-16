using UnityEngine;

public class Terrain : MonoBehaviour
{
    [Header("오브젝트")]
    public GameObject toggle;

    public void ToggleObj(bool on)
    {
        toggle.SetActive(on);
    }
}
