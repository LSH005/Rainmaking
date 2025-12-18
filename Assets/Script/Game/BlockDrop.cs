using UnityEngine;

public class BlockDrop : MonoBehaviour
{
    [Header("속도")]
    public float maxJumpPower;
    public float maxMovePower;
    public float spinSpeed;

    [Header("시간")]
    public float grabInterval;

    [Header("범위")]
    public float tryGrabDistance;
    public float grabDistance;

    [Header("소리")]
    public AudioClip[] grabSound;

    float spawnTime;

    void Start()
    {
        spawnTime = Time.time;
        Jump();
    }


    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerCamera.Instance.transform.position);
        if (distanceToPlayer <= tryGrabDistance) TryGrab();

        Vector3 rotation = Vector3.zero;
        rotation.y = spinSpeed * Time.deltaTime;
        transform.Rotate(rotation);
    }

    void TryGrab()
    {
        if (spawnTime + grabInterval >= Time.time)
        {
            return;
        }

        Vector3 targetPos = PlayerCamera.Instance.transform.position;

        float distanceToPlayer = Vector3.Distance(transform.position, targetPos);
        if (distanceToPlayer <= grabDistance) Grab();
        else transform.position = Vector3.Lerp(transform.position, targetPos, 5f * Time.deltaTime);
    }

    void Jump()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 velo = Vector3.zero;

        velo.x = maxMovePower * ((Random.value * 0.5f) + 0.5f) * ((Random.value > 0.5f) ? 1 : -1);
        velo.z = maxMovePower * ((Random.value * 0.5f) + 0.5f) * ((Random.value > 0.5f) ? 1 : -1);
        velo.y = maxJumpPower * ((Random.value * 0.5f) + 0.5f);

        rb.velocity = velo;
    }

    void Grab()
    {
        AudioClip clip = AudioManager.GetRandomSound(grabSound);
        AudioManager.Instance.Play3DSound(clip, transform.position);
        InventoryManager.instance.AddItem(ItemType.Block, 1);

        Destroy(gameObject);
    }

}
