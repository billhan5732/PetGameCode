using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraEffects : MonoBehaviour
{
    CinemachineVirtualCamera vCam;
    CinemachineBasicMultiChannelPerlin perlin;
    void Start() 
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
        

    }

    public IEnumerator Shake(float intensity, float time) 
    {
        float elapsed = 0f;
        perlin = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        perlin.m_AmplitudeGain = intensity;
        while (elapsed < time) 
        {


            elapsed += Time.deltaTime;
            yield return null;
        }
        perlin.m_AmplitudeGain = 0f;


    }
}
