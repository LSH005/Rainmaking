using UnityEngine;

public class RainMakingPos : MonoBehaviour
{
    Vector3 pos;

    private void Awake()
    {
        RainMakingManager.allRainMakingPos.Add(this);
    }

    private void OnDestroy()
    {
        RainMakingManager.allRainMakingPos.Remove(this);
    }

    private void Start()
    {
        pos = transform.position;
    }

    public bool CanRainMaking(Vector3 playerPos, float distance)
    {
        float distanceToPlayer = Vector3.Distance(playerPos, pos);
        return distanceToPlayer <= distance;
    }
}
