using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour
{
    public CharacterController controller;
    private Vector3 playerVelocity;
    public bool groundedPlayer;
    private float playerSpeed = 2.0f;
    private float jumpHeight = 7f;
    private float gravityValue = -9.81f * 4f;
    private bool isGrounded;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public LayerMask waterMask;

    RaycastHit hit;
    Player thisPlayer;

    private void Start()
    {
        //controller = gameObject.AddComponent<CharacterController>();
        thisPlayer = gameObject.GetComponent<Player>();
    }

    void Update()
    {
        //isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //Debug.Log(Vector3.Distance(groundCheck.transform.position, transform.position));

        //bool isHit = Physics.Raycast(transform.position, -Vector3.up, out hit, (Vector3.Distance(groundCheck.transform.position, transform.position)+groundDistance), groundMask );
        

        groundedPlayer = Physics.CheckSphere(transform.position, (Vector3.Distance(groundCheck.transform.position, transform.position) + groundDistance), groundMask);//Physics.Raycast(transform.position, -Vector3.up, out hit, (Vector3.Distance(groundCheck.transform.position, transform.position) + groundDistance), groundMask);
        thisPlayer.isSwimming = Physics.CheckSphere(transform.position, (Vector3.Distance(groundCheck.transform.position, transform.position)), waterMask);
        Debug.DrawLine(transform.position, hit.point, Color.red);
        //if (thisPlayer.isSwimming) { Debug.Log("Water"); }



        if (groundedPlayer && playerVelocity.y < 0 && !thisPlayer.isSwimming)
        {
            playerVelocity.y = -2.0f;
        }
        else if (thisPlayer.isSwimming && playerVelocity.y < 0) 
        {
            playerVelocity.y = -1f;
        }

        // Changes the height position of the player..
        if (groundedPlayer && !thisPlayer.isSwimming && Input.GetKeyDown("space"))
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue); 
        }
        else if (thisPlayer.isSwimming && Input.GetKey("space"))
        {
            playerVelocity.y += Mathf.Sqrt((jumpHeight * 0.01f) * -2.0f * gravityValue / 2);
        }


        playerVelocity.y += gravityValue * Time.deltaTime * 2;
        
        

        controller.Move(playerVelocity * Time.deltaTime);
    }
}