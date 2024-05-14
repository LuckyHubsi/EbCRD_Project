using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

internal enum MovementType
{
    TransformBased,
    PhysicsBased
}

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float _velocity = 1;
    [SerializeField]
    private ForceMode selectedForceMode;
    [SerializeField]
    private MovementType movementType;
    [SerializeField]
    private float jumpForce = 5f;

    private Vector3 movementDirection3d;
    private Rigidbody _rigidbody;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        /* if (Input.GetKey(KeyCode.W))
             gameObject.transform.position += new Vector3(0, 0, -1f) * _velocity;*/
        Debug.Log(movementDirection3d);
        PerformMovement();
    }

    void PerformMovement()
    {
        if (movementType == MovementType.TransformBased)
        {
            gameObject.transform.position += movementDirection3d * _velocity;
        }
        else if (movementType == MovementType.PhysicsBased)
        {
            _rigidbody.AddForce(movementDirection3d, selectedForceMode);
        }
    }

    void OnMovement(InputValue inputValue)
    {
        Vector2 movementDirection = inputValue.Get<Vector2>();
        movementDirection3d = new Vector3(movementDirection.x, 0, movementDirection.y);
    }

    void OnJump(InputValue inputValue)
    {
        if (inputValue.isPressed && isGrounded)
        {
            _rigidbody.AddForce(Vector3.up * jumpForce, selectedForceMode);
            isGrounded = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

}
