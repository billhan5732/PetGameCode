using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BillBoard : MonoBehaviour
{
    static Transform cam;
    static bool camFound = false;


    public string name;

    public Text text;
    public int rarity;
    public int maxHP;
    public int currentHP;

    bool fadeIn, fadeOut,valueChanging = false;
    public Slider healthSlider;
    float updateCounter = 0f;

    static float maxChangeVel = 100f;
    static float startingChangeVel = 10f;
    static float changeAccel = 20f;
    float currentVel = 0.5f;
    static float slowPoint = 0.8f;
    float targetValue;
    float currentValue;
    int changeDirection = 1;

    CanvasGroup canvasGroup;



    void Start()
    {
        cam = IGGameController.instance.mainCam.transform;
        /*
        if (!camFound) 
        {
            cam = IGGameController.instance.mainCam.transform;
            camFound = true;
        }
        */
        canvasGroup = this.GetComponent<CanvasGroup>();
        HideUI();
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }

    public void init(string name, int maxHP, int currentHP, int rarity) 
    {
        this.name = name;
        this.maxHP = maxHP;
        this.currentHP = currentHP;
        this.rarity = rarity;

        healthSlider.maxValue = maxHP;
    }

    void GetChangeDirection() 
    {
        if (targetValue > currentValue)
        {
            changeDirection = 1;
        }
        else if (targetValue < currentValue) { changeDirection = -1; }
        else 
        {
            changeDirection = 0;
            valueChanging = false;
        }
    }

    public void UpdateHealthBar(int newValue) 
    {
        ShowUI();

        int change = Mathf.Abs(currentHP - newValue);
        change = change / (change + 50);

        if (currentHP != newValue)
        {
            valueChanging = true;
            currentValue = currentHP;
            targetValue = Mathf.Min(maxHP, (newValue));
            GetChangeDirection();
            currentVel = startingChangeVel*change;
        }
        else 
        {
            valueChanging = false;
        }
        currentHP = Mathf.Min(maxHP, newValue);
        
        
        text.text = name +" (" + currentHP.ToString() + "/" + maxHP.ToString() + ")";
    }

    public void ShowUI() { fadeIn = true; fadeOut = false; }
    public void HideUI() { fadeOut = true; fadeIn = false; }

    public void UpdateFading()
    {
        if (fadeIn) { if (canvasGroup.alpha < 1) { canvasGroup.alpha += Time.deltaTime * 5; if (canvasGroup.alpha >= 1) { fadeIn = false; } } }
        if (fadeOut) { if (canvasGroup.alpha >= 0) { canvasGroup.alpha -= Time.deltaTime; if (canvasGroup.alpha <= 0) { fadeOut = false; } } }
    }
    public void UpdateUIFadeTimer()
    {
        if (updateCounter > 0) { updateCounter -= Time.deltaTime; if (updateCounter <= 0) { HideUI(); } }
    }

    public void UpdateValueChanging() 
    {
        
        if ((Mathf.Abs(currentValue - targetValue)) >= currentVel)
        {
            //Transitioning

            healthSlider.value = currentValue;
            
            currentValue += currentVel * changeDirection * Time.deltaTime;
            float temp = currentVel + changeAccel * Time.deltaTime;
            //currentVel += changeAccel * Time.deltaTime;
            currentVel = Mathf.Min(temp, maxChangeVel);
            //Slowing Down

        }
        else 
        {
            valueChanging = false;
            currentValue = targetValue;
            //currentVel = m
        }
    }

    public void UpdateUI() 
    {
        UpdateFading(); 
        UpdateUIFadeTimer();
        if (valueChanging) { UpdateValueChanging(); }
    }
}
