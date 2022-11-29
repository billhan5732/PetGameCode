using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wobble : MonoBehaviour
{
    public GameObject gObject;
    private float velocity = 50f;
    private float acceleration = 10f;
    private int count;
    private int countIndex = 1;
    private int half;
    // Start is called before the first frame update
    void Start()
    {
        gObject = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //gObject.transform.Rotate(velocity * Time.deltaTime, 0, 0);
        gObject.transform.Rotate(50 * Time.deltaTime, 0,0);
        
    }
}
