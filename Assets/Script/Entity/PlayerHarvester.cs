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
        if (Input.GetMouseButtonDown(1))
        {
            Place();
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

    void Place()
    {
        InventorySlot slot = InventoryManager.instance.GetSelectedInventorySlot();
        if (slot == null) return;
        if (slot.isEmptySlot || !slot.canPlace) return;

        Ray ray = _cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // È­¸é Áß¾Ó
        if (Physics.Raycast(ray, out var hit, rayDistance, hitMask))
        {
            //Vector3 blockPos = AdjacentCellOnHitFace(hit);

            MapGenerator.Instance.SetBlockVector3(hit.point);
            slot.AddItemCount(-1);
        }
    }

    static Vector3Int AdjacentCellOnHitFace(in RaycastHit hit)
    {
        Vector3 baseCenter = hit.collider.transform.position; // ¸ÂÃá ºí·ÏÀÇ Áß½É(Á¤¼ö ÁÂÇ¥(x,y,z)
        Vector3 adjCenter = baseCenter + hit.normal; // ±× ¸éÀÇ ¹Ù±ùÂÊÀ¸·Î Á¤È®È÷ ÇÑ Ä­ ÀÌµ¿
        return Vector3Int.RoundToInt(adjCenter);
    }

}
