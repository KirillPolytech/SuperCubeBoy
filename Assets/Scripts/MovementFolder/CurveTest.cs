using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CurveTest : MonoBehaviour
{
    [SerializeField] AnimationCurve MovementCurve;
    [SerializeField] AnimationCurve JumpCurve;
    Rigidbody Rb;
    Vector3 MoveDirection, JumpDirection;
    float Speed = 0, jumpForce = 0;
    float jumpTime = 1, movementTime = 1;
    bool IsJumped = false;
    float _isMovement = 0;
    private void Start()
    {
        Rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        IsJumped = Input.GetButtonDown("Jump");
    }
    private void FixedUpdate()
    {
        JumpTimeManagement();
        MovementTimeManager();
        Movement();
        Jump();

        Rb.AddForce(MoveDirection + JumpDirection) ;
    }
    void Movement()
    {
        Speed = MovementCurve.Evaluate(movementTime);

        _isMovement = Input.GetAxis("Horizontal") * Speed;

        MoveDirection = new(_isMovement, 0, 0);
    }
    void Jump()
    {
        jumpForce = JumpCurve.Evaluate(jumpTime);
        if (IsJumped)
        {
            JumpDirection = new(0, jumpForce, 0);
        }
        else JumpDirection = Vector3.zero;

    }
    void JumpTimeManagement()
    {
        if (IsJumped)
            jumpTime = 0;
        jumpTime = Mathf.Clamp(jumpTime, 0, 2);

        jumpTime += Time.fixedDeltaTime;
    }
    void MovementTimeManager()
    {
        if (_isMovement != 0)
            movementTime = 0;
        movementTime = Mathf.Clamp(jumpTime, 0, 2);

        movementTime += Time.fixedDeltaTime;
    }
}

// AxisY = ( Input.GetButton("Jump") ? 1 : 0 ) * jumpForce;
//Rb.AddForce(JumpDirection);