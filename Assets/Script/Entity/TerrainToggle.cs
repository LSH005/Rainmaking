using UnityEngine;

public class TerrainToggle : MonoBehaviour
{
    [Header("À§Ä¡")]
    public Transform posTarget;

    private void Update()
    {
        transform.position = posTarget.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Terrain>(out Terrain terrain))
        {
            terrain.ToggleObj(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<Terrain>(out Terrain terrain))
        {
            terrain.ToggleObj(false);
        }
    }
}
