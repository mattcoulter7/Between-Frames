using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DragSound : MonoBehaviour
{
    public LayerMask ground;
    public float groundDistance = 0.2f;
    public bool _isGrounded = false;

    public float speedDragThreshold = 1f;
    public float horizontalDragThreshold = 0.1f;

    private bool _isDragging = false;

    [SerializeField]
    private Transform[] groundCheckers;

    private Rigidbody _body;

    public UnityEvent onDragStart;
    public UnityEvent onDrag;
    public UnityEvent onDragEnd;

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _isGrounded = IsGrounded();

        Vector2 velocity = _body.velocity;
        float speed = Mathf.Abs(velocity.magnitude);
        float horizontalSpeed = Mathf.Abs(velocity.x);

        if (_isDragging)
        {
            // while the drag is happening
            Drag();

            // check if dragging has stopped
            if (!_isGrounded || (speed < speedDragThreshold && horizontalSpeed < horizontalDragThreshold))
            {
                DragEnd();
            }
        } 
        else
        {
            // check if dragging has started
            if (_isGrounded && (speed >= speedDragThreshold && horizontalSpeed >= horizontalDragThreshold))
            {
                DragStart();
            }
        }
    }

    private void DragStart()
    {
        onDragStart.Invoke();
        _isDragging = true;
    }

    private void DragEnd()
    {
        onDragEnd.Invoke();
        _isDragging = false;
    }

    private void Drag()
    {
        onDrag.Invoke();
    }

    private bool IsGrounded()
    {
        foreach(Transform groundCheck in groundCheckers)
        {
            if (Physics.CheckSphere(groundCheck.position, groundDistance, ground, QueryTriggerInteraction.Ignore)) return true;
        }
        return false;
    }
}
