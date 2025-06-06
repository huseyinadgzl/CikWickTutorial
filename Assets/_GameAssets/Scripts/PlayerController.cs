using System;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _orientitionTransform;

    [Header("Movement Settings")]
    [SerializeField] private float _movementSpeed;
    [SerializeField] private KeyCode _movementKey;

    [Header("Jump Settings")]
    [SerializeField] private KeyCode _jumpKey;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpCooldown;
    [SerializeField] private bool _canJump;

    [Header("Ground Check Setting")]
    [SerializeField] private float _playerHeight;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _groundDrag;

    [Header("Sliding Settings")]
    [SerializeField] private KeyCode _slideKey;
    [SerializeField] private float _slideMultiplier;
    [SerializeField] private float _slideDrag;
    private Rigidbody _playerRigidbody;

    private float _horizontalInput, _verticalInput;
    private Vector3 _movementDirections;
    private bool _isSliding;

    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerRigidbody.freezeRotation = true;

    }

    private void Update()
    {
        setInputs();
        SetPlayerDrag();
    }
    private void FixedUpdate()
    {
        SetPlayerMovement();
    }

    private void setInputs()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(_slideKey))
        {
            _isSliding = true;
            Debug.Log("Player is Sliding!");
        }
        else if (Input.GetKeyDown(_movementKey))
        {
            _isSliding = false;
            Debug.Log("Player is Moving");
        }
        else if (Input.GetKey(_jumpKey) && _canJump && IsGrounded())
        {
            _canJump = false;
            SetPlayerJumping();
            Invoke(nameof(ResetJumping), _jumpCooldown);
        }

    }

    private void SetPlayerMovement()
    {
        _movementDirections = _orientitionTransform.forward * _verticalInput
        + _orientitionTransform.right * _horizontalInput;

        if (_isSliding)
        {
            _playerRigidbody.AddForce(_movementDirections.normalized * _movementSpeed * _slideMultiplier, ForceMode.Force);
        }
        else
        {
             _playerRigidbody.AddForce(_movementDirections.normalized * _movementSpeed, ForceMode.Force);
        }
    }

    private void SetPlayerDrag()
    {
        if (_isSliding)
        {
            _playerRigidbody.linearDamping = _slideDrag;
        }
        else
        {
            _playerRigidbody.linearDamping = _groundDrag;        
        }
    }

    private void SetPlayerJumping()
    {
        //zıplamadan önce oyuncunun y ekseninde hızını sıfırlamak için
        _playerRigidbody.linearVelocity = new Vector3(_playerRigidbody.linearVelocity.x, 0f, _playerRigidbody.linearVelocity.z);
        _playerRigidbody.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
    }

    private void ResetJumping()
    {
        _canJump = true;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _groundLayer);
    }
}
