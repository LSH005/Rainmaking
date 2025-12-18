using UnityEngine;

public class TerrainToggle : MonoBehaviour
{
    [Header("À§Ä¡")]
    public Transform posTarget;

    private void Start()
    {
        if (PlayerCamera.Instance != null && posTarget == null) posTarget = PlayerCamera.Instance.transform;
    }

    private void Update()
    {
        if (posTarget != null) transform.position = posTarget.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Terrain>(out Terrain terrain))
        {
            terrain.ToggleObj(true);
        }
        else if (other.gameObject.TryGetComponent<TerrainSpawner>(out TerrainSpawner terrainSpawner))
        {
            terrainSpawner.SpawnTerrain();
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
