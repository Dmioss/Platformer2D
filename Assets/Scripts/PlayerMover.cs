using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMover : MonoBehaviour
{
    private const string HORISONTAL_AXIS = "Horizontal";
    private const string VERTICAL_AXIS = "Vertical";

    [Header("Movement Settings")]
    [SerializeField] private float _speed = 1f;    
    [SerializeField] private float _dashSpeed = 1f;
    [SerializeField] private float _dashDuration = 1f;


    private Rigidbody2D _rigidbody;
    private Vector2 _direction;
    private bool _isDashing = false;
    private Vector2 _dashDirection;
    private float _dashTimeLeft = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();        
    }


    private void Update()
    {
        _direction = new Vector2(Input.GetAxis(HORISONTAL_AXIS), Input.GetAxis(VERTICAL_AXIS));
        if (_direction.magnitude > 1)
        {
            _direction.Normalize();
        }

        if (_isDashing)
        {
            _dashTimeLeft -= Time.deltaTime;
            if (_dashTimeLeft <= 0)
            {
                EndDash();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space)  && _direction != Vector2.zero)
        {
            StartDash();
        }

    }
    
    void FixedUpdate()
    {
        if (_isDashing)
        {            
            _rigidbody.velocity = _dashSpeed * _dashDirection;
        }
        else
        {
            _rigidbody.velocity = _speed * _direction;
        }

        
    }

    private void StartDash() {
        _dashDirection = _direction;
        _isDashing = true;
        _dashTimeLeft = _dashDuration;
    }

    private void EndDash()
    {
        _isDashing = false;
    }
}
