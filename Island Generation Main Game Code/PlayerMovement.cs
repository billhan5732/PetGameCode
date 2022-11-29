using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public float speed = 10f;
    //public float currentSpeed;
    public float turnSmoothTime = 0.1f;

    public GameObject playerGO;

    //GroundCHeckStuff
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public bool isGrounded;

    private float jumpHeight = 3.0f;
    private float gravityValue = -9.81f*2;
    float turnSmoothVelocity;

    private bool isCamLocked = false;

    private Rigidbody rb;

    private Animation animationList;
    PlayerAnimationController animationController;
    Player thisPlayer;


    #region States
    public int ANIM_STATE;

    private readonly int STATE_IDLE = 0;
    private readonly int STATE_MOVING = 1;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        animationList = this.GetComponent < Player > ().newPetModel.GetComponent < Animation > ();
        ANIM_STATE = STATE_IDLE;
        thisPlayer = GetComponent<Player>();
        //currentSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {

        CamLockUpdate();

        MovementUpdate();

    }

    void CamLockUpdate() 
    {
        if (Input.GetMouseButton(1) || thisPlayer.isSwimming)
        {
            float z = 0f, x = 0f;
            if (thisPlayer.isSwimming && !isGrounded) { z = cam.transform.rotation.eulerAngles.z; x = cam.transform.rotation.eulerAngles.x; }
            isCamLocked = true;
            transform.rotation = Quaternion.Euler(x, cam.eulerAngles.y, z);
        }
        else { isCamLocked = false; }
    }

    void MovementUpdate() 
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        float targetAngle;
        float angle;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded) { transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0); }

        Vector3 moveDirection = Vector3.zero;

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (thisPlayer.isSwimming)
        {
            direction = new Vector3(horizontal, 0f, vertical).normalized;
        }
        

        ANIM_STATE = STATE_IDLE;

        if (animationController == null) { animationController = this.GetComponent<PlayerAnimationController>(); }
        //animationController.SetState(STATE_IDLE);

        //Movement
        if (direction.magnitude >= 0.1)
        {
            ANIM_STATE = STATE_MOVING;
            //animationController.SetState(STATE_MOVING);
            targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            //if (Input.GetMouseButton(1)) {  targetAngle = cam.eulerAngles.y; }

            angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            if (!isCamLocked) //Only Updates rotation when cam is not locked
            {
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }

            
            moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            if (thisPlayer.isSwimming && !isGrounded) 
            {
                moveDirection = Quaternion.Euler(cam.eulerAngles.x,targetAngle, 0) * Vector3.forward;

            }

            //moveDirection.y += Mathf.Sqrt(jumpHeight * 2.0f * gravityValue);
        }
        //moveDirection.y += Mathf.Sqrt(jumpHeight * 2.0f * gravityValue);

        moveDirection = moveDirection.normalized;
        controller.Move(moveDirection * speed * Time.deltaTime);

        animationController.SetState(ANIM_STATE);

        //AnimationHandler();
    }
}
