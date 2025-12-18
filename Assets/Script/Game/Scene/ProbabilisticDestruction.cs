using UnityEngine;

public class ProbabilisticDestruction : MonoBehaviour
{
    [Header("È®·ü")]
    public int destroyProbability = 0;

    void Start()
    {
        if (CheckProbability(destroyProbability))
        {
            Destroy(gameObject);
        }
        else
        {
            this.enabled = false;
        }
    }

    bool CheckProbability(float percent)
    {
        float probability = Mathf.Clamp01(percent * 0.01f);
        return Random.value <= probability;
    }
}
