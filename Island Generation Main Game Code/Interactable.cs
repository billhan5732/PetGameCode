using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour, DamageAbleInterface
{
    // Start is called before the first frame update
    public string name;
    public Transform billBoard;

    public int hp = 100;
    int currentHP;
    public int resistance;

    private Animation anim;

    private float updateCounter = 0f;

    public BillBoard billBoardOBJ;

    public GameObject[] drops;
    public float[] dropRates;

    public int minimumElevationToSpawn;
    public int maximumElevationToSpawn;
    public float spawnChance;


    void Start()
    {

        currentHP = hp;
        //SetHP(currentHP, hp);
        anim = GetComponent<Animation>();

        billBoardOBJ.init(name,hp,currentHP,0);
        billBoardOBJ.HideUI();
        //originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void Interact() 
    {
        
        anim.Stop();
        anim.Play("BasicBoing");
        //billBoardOBJ.ShowUI();
        updateCounter = 2f;
        //StartCoroutine(WaitDelayBeforeFading());
        //HideUI();
        if (currentHP <= 0){ HandleDeath(); }
    }

    void HandleDeath() 
    {
        billBoardOBJ.HideUI();
        Destroy(gameObject);
    }

    void DropItems() 
    {
        if (drops.Length <= 0) { return; }

        GameObject drop = Instantiate(drops[UnityEngine.Random.Range(0,drops.Length)]);
        //drop.GetComponent<Drop>() 
        Vector3 position = this.transform.position;
        position.y += 5f;
        drop.GetComponent<Drop>().PopOut(position);
    }

    public void TakeDamage(int damage, float armorPen)
    {
        float targetDefense = this.resistance * (float)(1 - armorPen);
        float multiplier = targetDefense / (targetDefense + 100);
        int targetDamageTaken = (int)(damage * multiplier);

        currentHP -= damage;
        //Debug.Log(targetDamageTaken);
        billBoardOBJ.UpdateHealthBar(currentHP);

        Interact();
        DropItems();
    }

    public bool canSpawnInElevation(int elevation) 
    {
        return ((elevation > minimumElevationToSpawn) && ((maximumElevationToSpawn == -1) || (elevation <= maximumElevationToSpawn)));
    }


}
