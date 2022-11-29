using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBlackOut : MonoBehaviour
{
    static Vector3 originalPos;

    public float time;
    public Transform blackOutOBJ;
    void Start()
    {
        originalPos = blackOutOBJ.transform.position;
        RemoveBlackOutScreen();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveBlackOutScreen() 
    {
        blackOutOBJ.position = originalPos;
        StartCoroutine(moveOutOfWay());
    }

    public void BringBackBlackOutScreen() 
    {
        StartCoroutine(bringBack());
    }

    IEnumerator moveOutOfWay() 
    {
        float elapsed = 0f;

        float velocity = 5f;
        float accel = 50f;

        while (elapsed < time) 
        {
            blackOutOBJ.Translate(new Vector3(-1 * velocity * Time.deltaTime, 0));
            velocity += accel*Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }
        //StartCoroutine(bringBack());
        yield break;

    }
    IEnumerator bringBack()
    {
        float elapsed = 0f;

        float velocity = 5f;
        float accel = 50f;

        while (elapsed < time)
        {
            blackOutOBJ.Translate(new Vector3( velocity * Time.deltaTime, 0));
            velocity += accel * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }

        blackOutOBJ.position = originalPos;

        yield break;

    }
}
