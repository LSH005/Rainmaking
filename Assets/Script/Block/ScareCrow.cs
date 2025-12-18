using UnityEngine;

public class ScareCrow : MonoBehaviour
{
    [Header("¹üÀ§")]
    public float minSpeed = 0.8f;
    public float maxSpeed = 2.0f;

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        anim.speed = Random.Range(minSpeed, maxSpeed);
    }


    public void ReAnim()
    {
        anim.speed = Random.Range(minSpeed, maxSpeed);
        anim.SetTrigger("doAnimation");
    }
}
