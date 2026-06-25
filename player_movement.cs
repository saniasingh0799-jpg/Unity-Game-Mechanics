using UnityEngine;

public class Player_movement : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;

    [Header("Gravity")]
    public float gravity = 9.81f;
    public bool IsSprinting { get; private set; }
    public bool IsMoving { get; private set; }

    private CharacterController _controller;
    private Animator _anim;
    private Fighter _fighter;

    private Vector3 _verticalVelocity;

    private static readonly int HashRun = Animator.StringToHash("run");
    private static readonly int HashWalk = Animator.StringToHash("walk");
    private static readonly int HashIdle = Animator.StringToHash("idle");
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _anim = GetComponent<Animator>();
        _fighter = GetComponent<Fighter>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        bool attacking = _fighter != null && _fighter.IsAttacking();
        HandleMovement(attacking);
        ApplyGravity();
        UpdateAnimator(attacking);
    }
    private void HandleMovement(bool attacking)
    {
        float moveX = attacking ? 0f : Input.GetAxisRaw("Horizontal");
        float moveZ = attacking ? 0f : Input.GetAxisRaw("Vertical");

        IsSprinting = !attacking && Input.GetKey(KeyCode.LeftShift);
        IsMoving = (moveX != 0f || moveZ != 0f);

        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        Vector3 moveDir = forward * moveZ + right * moveX;
        if (moveDir.sqrMagnitude > 1f) moveDir.Normalize();

        float speed = IsSprinting ? sprintSpeed : walkSpeed;
        _controller.Move(moveDir * (speed * Time.deltaTime));
    }
    private void ApplyGravity()
    {
        if (_controller.isGrounded && _verticalVelocity.y < 0f)
            _verticalVelocity.y = -2f;
        else
            _verticalVelocity.y -= gravity * Time.deltaTime;

        _controller.Move(_verticalVelocity * Time.deltaTime);
    }
    private void UpdateAnimator(bool attacking)
    {
        if (_anim == null) return;

        _anim.SetBool(HashRun, !attacking && IsSprinting);
        _anim.SetBool(HashWalk, !attacking && IsMoving && !IsSprinting);
        _anim.SetBool(HashIdle, !attacking && !IsMoving);
    }
}