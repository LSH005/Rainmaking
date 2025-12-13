using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("움직임")]
    public float moveSpeed = 5;
    public float jumpPower = 1;
    public float gravityPower = -19.62f;

    [Header("카메라")]
    public Transform cam;

    CharacterController cont;
    Vector3 moveDirection;
    float yVelocity;

    private void Awake()
    {
        cont = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleMovement();
        HandleGravityAndJump();

        Vector3 finalMovement = (moveDirection * moveSpeed) + (Vector3.up * yVelocity);
        cont.Move(finalMovement * Time.deltaTime);
    }

    void HandleMovement()
    {
        Vector3 inputDirection = GetInput();

        float targetAngle = cam != null ? cam.eulerAngles.y : 0;
        Vector3 rotatedMoveDirection = Quaternion.Euler(0f, targetAngle, 0f) * inputDirection;
        moveDirection = rotatedMoveDirection;
    }

    void HandleGravityAndJump()
    {
        if (cont.isGrounded)
        {
            yVelocity = -0.2f;

            if (Pressing(KeyCode.Space))
            {
                yVelocity = Mathf.Sqrt(jumpPower * -2f * gravityPower);
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
        
        if (isSingleInput(KeyCode.W, KeyCode.S))
        {
            if (Pressing(KeyCode.W)) MoveInput.z = 1;
            else MoveInput.z = -1;
        }

        if (isSingleInput(KeyCode.D, KeyCode.A))
        {
            if (Pressing(KeyCode.D)) MoveInput.x = 1;
            else MoveInput.x = -1;
        }

        return MoveInput;
    }

    bool isSingleInput(KeyCode key1, KeyCode key2) => Input.GetKey(key1) ^ Input.GetKey(key2);

    bool Pressing(KeyCode key) => Input.GetKey(key);
}
