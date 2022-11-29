using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour, DamageAbleInterface
{
    // Start is called before the first frame update

    public string name;

    public static BackPack pack;
    public static IGGameController gameController;

    static Vector3 zero = Vector3.zero;

    bool GroundCollisions = false;

    public GameObject gameObjectModel;
    public int value;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<IGGameController>();
        value = Random.Range(0, value);
        if (value == 0) { Destroy(gameObject); }
        //this.gameObject.tag 
        //pack = GameObject.FindGameObjectWithTag("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        if (GroundCollisions) 
        {

            transform.Rotate(0.0f, 1f, 0.0f);
        }
        //transform.Rotate(0.0f,1f, 0.0f);
    }

    void OnCollisionEnter(Collision collision) 
    {
        if (!GroundCollisions && collision.gameObject.CompareTag("Ground") )
        {
            //Debug.Log("Hit Ground");
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            GroundCollisions = true;
            float newHeight = transform.position.y + 0.5f;
            transform.position = new Vector3(transform.position.x, newHeight, transform.position.z);
        }
        else if (collision.gameObject.CompareTag("Interactable") || collision.gameObject.CompareTag("Drop") || (!GroundCollisions && collision.gameObject.CompareTag("Player")))
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
            //Debug.Log("Ignored");
        }
        else if (GroundCollisions && collision.gameObject.CompareTag("Player") ) 
        {
            SendToBackpack();
            //Debug.Log("Touched By Player");
        }
    }

    public void TakeDamage(int damage, float armorPen)
    {
        SendToBackpack();
    }

        public void SendToBackpack() 
    {
        //Debug.Log(this.name + " has been sent to back pack");
        BackPack.AddItem(this);
        Destroy(gameObject);
        //this.setAsStatic();
        //gameObject.SetActive(false);
        //Destroy(gameObject);
    }

    public void PopOut(Vector3 startingLocation) 
    {
        this.transform.position = startingLocation;
        Rigidbody rb = GetComponent<Rigidbody>();
        float randomDirection = Random.Range(-1f,1f) * 10f;
        rb.AddForce(Random.Range(-1f, 1f) * 5f, 10f, Random.Range(-1f, 1f) * 5f, ForceMode.Impulse);
        //GroundCollisions = true;
    }

    public void setAsStatic() 
    {
        //transform.SetParent(parent);
        Destroy(GetComponent<Rigidbody>());
        Destroy(GetComponent<BoxCollider>());

        transform.rotation = Quaternion.Euler(zero);
        transform.position = zero;

        //Destroy(this);


    }
}
