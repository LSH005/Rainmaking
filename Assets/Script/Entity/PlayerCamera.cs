using System.Collections;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera Instance;

    [Header("¼³Á¤")]
    public float mouseSensitivity = 100f;

    [Header("¸ö")]
    public Transform playerBody;

    float xRotation = 0f;
    Coroutine fovCoroutine;
    Camera cam;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            this.enabled = false;
            Destroy(gameObject);
            return;
        }

        cam = GetComponent<Camera>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX, Space.World);
    }

    public void SetFov(float fov, float duration)
    {
        if (fovCoroutine != null) StopCoroutine(fovCoroutine);

        if (duration > 0) StartCoroutine(FovChange(fov, duration));
        else
        {
            fovCoroutine = null;
            cam.fieldOfView = fov;
        }
    }

    IEnumerator FovChange(float fov, float duration)
    {
        float time = 0;
        float startFov = cam.fieldOfView;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            cam.fieldOfView = Mathf.Lerp(startFov, fov, t);
            yield return null;
        }
    }
}
