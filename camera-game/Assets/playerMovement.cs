using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
public class playerMovement : MonoBehaviour {
     
    public float moveSpeed;
    public float jumpHeight;
    public float groundDistance = 0.2f;
    public LayerMask Ground;
    public LayerMask Ground2;
    
    private Rigidbody _body;
    private Vector3 _inputs = Vector3.zero;
    public bool _isGrounded = false;
    private Transform _groundChecker;

 
     // Use this for initialization
    void Start () {
    	_body = GetComponent<Rigidbody>();
    	_groundChecker = transform.GetChild(0);
 
    }
     
     // Update is called once per frame
    void Update () {
        // look at downwards raycast for grounding
    	_isGrounded = Physics.CheckSphere(_groundChecker.position, groundDistance, Ground | Ground2, QueryTriggerInteraction.Ignore);

        _inputs = Vector3.zero;
        _inputs.x = Input.GetAxis("Horizontal");
        //_inputs.z = Input.GetAxis("Vertical");

        if(_inputs != Vector3.zero){
        	transform.forward = _inputs;
        }

        if (Input.GetKeyDown (KeyCode.W) && _isGrounded){
            _body.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
            _isGrounded = false;
        }

 
    }
 
    void FixedUpdate () {
        // else if (Input.GetKey(KeyCode.A)){
        //     GetComponent<Rigidbody>().velocity = new Vector2(-moveSpeed, 0);
        // }else if (Input.GetKey (KeyCode.D)){
        //     GetComponent<Rigidbody>().velocity = new Vector2(moveSpeed, 0);
        // }

        _body.MovePosition(_body.position + _inputs * moveSpeed * Time.fixedDeltaTime);
    }

     //fix jump or remove mechanic
    void Jump(){
    	GetComponent<Rigidbody>().velocity = new Vector2(0, jumpHeight);
    	_isGrounded = false;
    	Debug.Log("isGrounded = " + _isGrounded);
    }

     
   // private void OnCollisionEnter(Collision col)
   // {
   //  	if(col.collider.tag == "Ground")
   //  	{
   //  		GetComponent<Rigidbody>().velocity = Vector2.zero;
   //  		isGrounded = true;
   //  		//ReadyToDash = true;
   //  	}
   // }


    // private CharacterController controller;
    // private Vector2 playerVelocity;
    // private bool groundedPlayer;
    // public float playerSpeed = 2.0f;
    // public float jumpHeight = 1.0f;
    // public float gravityValue = -9.81f;

    // private void Start()
    // {
    //     controller = gameObject.AddComponent<CharacterController>();
    // }

    // void Update()
    // {
    //     groundedPlayer = controller.isGrounded;
    //     if (groundedPlayer && playerVelocity.y < 0)
    //     {
    //         playerVelocity.y = 0f;
    //     }

    //     Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    //     controller.Move(move * Time.deltaTime * playerSpeed);

    //     if (move != Vector2.zero)
    //     {
    //         gameObject.transform.forward = move;
    //     }

    //     // Changes the height position of the player..
    //     if (Input.GetButtonDown("Jump") && groundedPlayer)
    //     {
    //         playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
    //     }

    //     playerVelocity.y += gravityValue * Time.deltaTime;
    //     controller.Move(playerVelocity * Time.deltaTime);
    // }
} 
