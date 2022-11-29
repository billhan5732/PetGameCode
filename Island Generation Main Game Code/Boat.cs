using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Boat : MonoBehaviour
{
    // Start is called before the first frame update
    public Cinemachine.CinemachineFreeLook c_VirtualCam;

    public LayerMask m_LayerMask;
    float speed = 100f;

    public GameObject rangeColliderHolder;
    public BoxCollider rangeCollider;

    bool landed = false;
    bool targetFound = false;

    public Rigidbody rigidbody;

    public PromptYN prompt;

    Transform closestLand;

    public IGGameController gameController;

    public GameObject player;

    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        if (!landed) { BoatGoesToNearestLand(); } 
        
    }

    void BoatGoesToNearestLand() 
    {
        if (!targetFound)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, Mathf.Infinity, m_LayerMask))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * hit.distance, Color.yellow);
                Debug.Log(hit.transform.gameObject.name);
                targetFound = true;
                closestLand = hit.transform;
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * 1000, Color.red);
                Debug.Log("Did not Hit");
            }
        }
        else if (targetFound)
        {
            float dist = Vector3.Distance(transform.position, closestLand.position);
            if (dist <= 20f) 
            {
                speed = 0;
                landed = true;
                try 
                {
                    player.transform.position = closestLand.GetComponent<TilePiece>().GetTopPosition();
                }
                catch (Exception e) 
                {
                    Debug.Log("Couldn't find component");
                }
                player.SetActive(true);
                c_VirtualCam.m_Follow = player.transform;
                c_VirtualCam.m_LookAt = player.transform;
                return;
            }

            //Vector3 newPosition = (new Vector3(1, 0, 0)) ;

            //rigidbody.MovePosition(transform.position + newPosition * speed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            //playerObj.transform.position = newPosition;// + playerObj.transform.position;
            //player.gameObject.transform.position = newPosition;
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("Player Wants to Interact");
            //Time.timeScale = 0;
            prompt.getUserInput("Do you Want to Return?", EndRun);

            //Debug.Log("Poggers");
        }
    }

    public void EndRun() 
    {
        gameController.ReturnToMenu();
    }
}
