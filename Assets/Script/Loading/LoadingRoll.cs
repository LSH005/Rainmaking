using UnityEngine;

public class LoadingRoll : MonoBehaviour
{
    public float rotationSpeed = 1.0f;


    void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
