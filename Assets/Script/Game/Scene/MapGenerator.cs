using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("지형")]
    public GameObject terrain;

    [Header("지형 범위")]
    public int width = 20;
    public int depth = 20;
    public int terrainGap;
    public int maxHeight = 6;

    [Header("노이즈")]
    public float noiseScale = 20f;

    void Start()
    {
        float offsetX = Random.Range(-9999f, 9999f);
        float offsetY = Random.Range(-9999f, 9999f);
        int halfX = width / 2;
        int HalfZ = depth / 2;

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                float nx = (x + offsetX) / noiseScale;
                float nz = (z + offsetY) / noiseScale;

                float noise = Mathf.PerlinNoise(nx, nz);
                int y = Mathf.FloorToInt(noise * maxHeight);

                SetBlock(x - halfX, y, z - HalfZ);
            }
        }

    }

    void SetBlock(int x, int y, int z)
    {
        x *= terrainGap; z *= terrainGap;

        var go = Instantiate(terrain, new Vector3(x, y, z), Quaternion.identity, transform);
        go.name = $"Terrain : {x} // {y} // {z}";
        go.transform.SetParent(transform);
    }
}
