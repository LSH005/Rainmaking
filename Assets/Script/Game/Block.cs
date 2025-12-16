using UnityEngine;

public class Block : MonoBehaviour
{
    [Header("소리")]
    public AudioClip[] destroySound;

    [Header("드롭")]
    public GameObject drop;

    [Header("HP")]
    public int HP;

    int currentHP;

    private void Start()
    {
        currentHP = HP;
    }

    public void HitBlock(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Destroy();
        }
    }

    private void Destroy()
    {
        PlayDestorySound();
        Instantiate(drop, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    void PlayDestorySound()
    {
        AudioClip clip = AudioManager.GetRandomSound(destroySound);
        AudioManager.Instance.Play3DSound(clip, transform.position);
    }

}
