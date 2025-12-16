using UnityEngine;

public class PlayerHarvester : MonoBehaviour
{
    [Header("Ã¤±¼")]
    public float rayDistance = 5f;
    public LayerMask hitMask;
    public int toolDamage = 1;
    [Header("Ã¤±¼ Äð´Ù¿î")]
    public float hitCooldown = 0.15f;
    
    float _nextHitTime;

    Camera _cam;

    private void Awake()
    {
        _cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= _nextHitTime)
        {
            Harvest();
        }
    }

    void Harvest()
    {
        _nextHitTime = Time.time + hitCooldown;

        Ray ray = _cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // È­¸é Áß¾Ó
        if (Physics.Raycast(ray, out var hit, rayDistance, hitMask))
        {
            var block = hit.collider.GetComponent<Block>();
            if (block != null)
            {
                block.HitBlock(toolDamage);
            }
        }
    }

}
