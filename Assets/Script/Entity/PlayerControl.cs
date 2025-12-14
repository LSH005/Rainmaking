using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("움직임")]
    public float moveSpeed = 5;
    public float runSpeed = 10;
    public float jumpPower = 1;
    public float gravityPower = -19.62f;

    [Header("가속")]
    public float acceleration = 5f;
    public float deceleration = 10f;
    public float hopMultiple = 1.1f;

    [Header("카메라")]
    public Transform cam;

    float yVelocity;
    bool isRunning = false;
    bool isMoving = false;
    bool hasInput = false;
    CharacterController cont;
    Vector3 moveVelocity;
    Quaternion lastCameraRotation;

    private void Awake()
    {
        cont = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleMovement();
        HandleGravityAndJump();

        Vector3 finalMovement = (moveVelocity * (isRunning ? runSpeed : moveSpeed)) + (Vector3.up * yVelocity);
        cont.Move(finalMovement * Time.deltaTime);

        if (cam != null)
        {
            lastCameraRotation = cam.rotation;
        }
    }

    void HandleMovement()
    {
        Vector3 inputDirection = GetInput();

        Quaternion currentCameraRotation = cam.rotation;
        Quaternion deltaRotation = currentCameraRotation * Quaternion.Inverse(lastCameraRotation);
        Vector3 deltaEuler = deltaRotation.eulerAngles;
        deltaEuler.x = deltaEuler.z = 0f;
        Quaternion yRotationDelta = Quaternion.Euler(deltaEuler);
        moveVelocity = yRotationDelta * moveVelocity;

        if (hasInput)
        {
            float targetAngle = cam != null ? cam.eulerAngles.y : 0;
            Vector3 rotatedMoveDirection = Quaternion.Euler(0f, targetAngle, 0f) * inputDirection;
            moveVelocity += rotatedMoveDirection * acceleration * Time.deltaTime;
        }
        else if (cont.isGrounded)
        {
            moveVelocity = Vector3.Lerp(
            moveVelocity,
            Vector3.zero,
            deceleration * Time.deltaTime
            );
        }

        if (hasInput && moveVelocity.magnitude > 1)
        {
            moveVelocity.Normalize();
        }
        
        bool wasMoving = isMoving;
        isMoving = hasInput || moveVelocity.magnitude > 0.1f;

        if (!isMoving && wasMoving)
        {
            PlayerCamera.Instance.SetFov(60, 0.2f);
        }
        else if (isMoving && !wasMoving)
        {
            PlayerCamera.Instance.SetFov(70, 0.2f);
        }
        
        if (isMoving)
        {
            bool wasRunning = isRunning;
            isRunning = Pressing(KeyCode.LeftShift);

            if (wasRunning && !isRunning)
            {
                PlayerCamera.Instance.SetFov(70, 0.2f);
            }
            else if (!wasRunning && isRunning)
            {
                PlayerCamera.Instance.SetFov(90, 0.2f);
            }
        }
    }

    void HandleGravityAndJump()
    {
        if (cont.isGrounded)
        {
            yVelocity = -0.2f;

            if (Pressing(KeyCode.Space))
            {
                yVelocity = Mathf.Sqrt(jumpPower * -2f * gravityPower);

                if (!hasInput)
                {
                    moveVelocity *= hopMultiple;
                }
            }
        }
        else
        {
            yVelocity += gravityPower * Time.deltaTime;
        }
    }

    Vector3 GetInput()
    {
        Vector3 MoveInput = Vector3.zero;
        hasInput = false;

        if (isSingleInput(KeyCode.W, KeyCode.S))
        {
            if (Pressing(KeyCode.W)) MoveInput.z = 1;
            else MoveInput.z = -1;
            hasInput = true;
        }

        if (isSingleInput(KeyCode.D, KeyCode.A))
        {
            if (Pressing(KeyCode.D)) MoveInput.x = 1;
            else MoveInput.x = -1;
            hasInput = true;
        }

        return MoveInput;
    }

    bool isSingleInput(KeyCode key1, KeyCode key2) => Input.GetKey(key1) ^ Input.GetKey(key2);

    bool Pressing(KeyCode key) => Input.GetKey(key);
}
