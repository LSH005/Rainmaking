using UnityEngine;

public class TerrainSpawner : MonoBehaviour
{
    public GameObject[] normalTerrains;
    public GameObject mulmeoggaeTerrains;

    [Header("¹°¸Ô°³ È®·ü")]
    public float mulmeoggaeProbability;

    public void SpawnTerrain()
    {
        GameObject target;

        if (CheckProbability(mulmeoggaeProbability))
        {
            target = mulmeoggaeTerrains;
        }
        else
        {
            target = GetRandomGameObject(normalTerrains);
        }

        Instantiate(target, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    bool CheckProbability(float percent)
    {
        float probability = Mathf.Clamp01(percent * 0.01f);
        return Random.value <= probability;
    }

    GameObject GetRandomGameObject(GameObject[] obj)
    {
        if (obj == null || obj.Length == 0) return null;

        int randomIndex = Random.Range(0, obj.Length);
        return obj[randomIndex];
    }
}
