using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DragSound : MonoBehaviour
{
    public LayerMask ground;
    //;public string soundEffect;
    public float groundDistance = 0.2f;
    public bool _isGrounded = false;
    public float minBoxSpeed;


    [SerializeField]
    private Transform[] groundCheckers;

    private Rigidbody _body;

    //public UnityEvent onDrag;
    public UnityEvent onDragStart;
    public UnityEvent onDragEnd;

   
    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Mag: " + _body.velocity.magnitude);
        
       // _isGrounded = IsGrounded();

        // Change is state detected ||Try again later
        //if (prevIsGrounded != _isGrounded && _body.velocity.magnitude > minBoxSpeed) {
        //    if (_isGrounded) onDragStart.Invoke();
        //    else onDragEnd.Invoke();
        //}

        //new idea.
        //if()
       
    }

    private void FixedUpdate()
    {//Vector3 remember
        //Debug.Log("Mag: " + _body.velocity.magnitude);
        bool prevIsGrounded = _isGrounded;

        _isGrounded = IsGrounded();
        Vector2 vel = _body.velocity;

        if (prevIsGrounded != _isGrounded)
        {

            _body.velocity = vel.normalized;

        }
       


        // Change is state detected ||Try again later
        //if (prevIsGrounded != _isGrounded && _body.velocity.magnitude > minBoxSpeed) {
        //    if (_isGrounded) onDragStart.Invoke();
        //    else onDragEnd.Invoke();
        //}
    }

    private bool IsGrounded()
    {
        foreach(Transform groundCheck in groundCheckers)
        {
            if (Physics.CheckSphere(groundCheck.position, groundDistance, ground, QueryTriggerInteraction.Ignore)) return true;
        }
        return false;
    }


    //private void OnCollisionEnter(Collision col)
    //{
    //    Debug.Log("Collided!");
    //    bool prevIsGrounded = _isGrounded;

       
    //    //if (prevIsGrounded != _isGrounded)
    //    //{
    //    //    Debug.Log("Tag: " + col.gameObject.tag);
    //    //    if (col.gameObject.CompareTag("Player"))
    //    //    {
               
    //    //        onDragStart.Invoke();
    //    //    }

    //    //}

    //    if (col.gameObject.tag == ("Player"))
    //    {
    //        //if (prevIsGrounded != _isGrounded)
    //        //{

    //            onDragStart.Invoke();
    //        //}
    //    }


    //}

    //private void OnCollisionExit(Collision col)
    //{



    //    if (col.gameObject.tag == ("Player"))
    //    {
    //        onDragEnd.Invoke();
    //    }


    //}
}
