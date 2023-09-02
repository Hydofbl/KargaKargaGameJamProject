using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private CollisionDetect _collDetect;
    private Vector2 _movementDir;

    [Header("Stats")]
    public float MovementSpeed = 12;
    public float JumpForce = 15;
    [Range(0f, 1f)]
    public float JumpHeightModifier = 0.5f;

    [Space]
    [Header("Booleands")]
    public bool CanJump;

    [Space]
    [Header("Jumping")]
    [Header("Jump Buffering")]
    public float JumpPressedRemember = 0f;
    public float JumpPressedRememberTime = 0.2f;
    [Header("Coyote Time")]
    public float GroundedRemember = 0f;
    public float GroundedRememberTime = 0.2f;

    private void Start()
    {
        InputManager.Instance.OnMove += OnMove;
        InputManager.Instance.OnJumpButtonPressed += OnJumpButtonPressed;
        InputManager.Instance.OnJumpButtonReleased += OnJumpButtonReleased;

        _rb = GetComponent<Rigidbody2D>();
        _collDetect = GetComponent<CollisionDetect>();
    }

    private void OnDestroy()
    {
        InputManager.Instance.OnMove -= OnMove;
        InputManager.Instance.OnJumpButtonPressed -= OnJumpButtonPressed;
        InputManager.Instance.OnJumpButtonReleased -= OnJumpButtonReleased;
    }

    private void Update()
    {
        JumpPressedRemember -= Time.deltaTime;
        GroundedRemember -= Time.deltaTime;

        if(_collDetect.onGround)
        {
            GroundedRemember = GroundedRememberTime;
        }

        if ((GroundedRemember > 0f) && (JumpPressedRemember > 0f))
        {
            JumpPressedRemember = 0;
            GroundedRemember = 0;

            _rb.velocity = new Vector2(_rb.velocity.x, 0);
            _rb.velocity += Vector2.up * JumpForce;
        }

        _rb.velocity = new Vector2(_movementDir.x * MovementSpeed, _rb.velocity.y);
    }

    #region Movement
    private void OnMove(Vector2 dir)
    {
        _movementDir = dir;

        UpdateFacing(dir);
    }

    // Facing
    private void UpdateFacing(Vector2 dir)
    {
        if (dir.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(dir.x) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
    #endregion

    #region Jumping
    private void OnJumpButtonPressed()
    {
        JumpPressedRemember = JumpPressedRememberTime;
    }

    // Variable Height Jump
    private void OnJumpButtonReleased()
    {
        if(_rb.velocity.y > 0)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * JumpHeightModifier);
        }
    }
    #endregion
}
