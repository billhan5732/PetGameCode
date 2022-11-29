using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour, DamageAbleInterface
{
    public Pet selectedPet;
    public GameObject manisfestationParent;
    public GameObject manisfestation;
    public GameObject gameControl;
    public PlayerMovement playerMovementScript;
    public CinemachineFreeLook cam;
    Jumping jumpingScript;

    CameraEffects camEfx;

    public float levelSizeScale = 0.05f;
    public GameObject newPetModel;
    public GameObject attackColiderHolder;
    BoxCollider attackBoxCollider;
    public LayerMask m_LayerMask;
    PlayerAnimationController animationController;

    Inventory inventory;

    public CharStats charStats = new CharStats();

    public float activePetFullEnergy = 200f;
    public float activePetCurrentEnergy = 50f;
    public float activePetSprintSpeed;

    float attackDelayTimer = 0f;

    bool testStart = false;
    public bool isSwimming = false;
    public bool isBlocking = false;
    public bool isDashing = false;

    static float[] babyCamValues = new float[6] {12.78f, 1.5f, 5.6f, 18.61f, -1.47f, 2.48f };
    static float[] maxCamValues = new float[6] { 20.6f, 10.13f, 13.3f, 28.81f, -1.47f, 8.44f };
    const int TOP_H = 0, TOP_R = 1, MID_H = 2, MID_R = 3, BOT_H = 4, BOT_R = 5;

    void Start()
    {
        testStart = true;

        gameControl = GameObject.FindGameObjectWithTag("GameController");
        playerMovementScript = this.transform.GetComponent<PlayerMovement>();
        jumpingScript = this.transform.GetComponent<Jumping>();
        inventory = Inventory.instance;
        //SetPet();
        

        IGGameController.SetTagDeeply(gameObject, "Player");

    }
    // Update is called once per frame
    void Update()
    {
        UpdateEnergy();
        CheckKeyUpdates();
        TimerUpdates();

        //isSwimming = Physics.CheckSphere(transform.position, (Vector3.Distance(groundCheck.transform.position, transform.position)), waterMask);
    }

    void SetCamera(int petLevel) 
    {
        float[] values = new float[6]
        {
            babyCamValues[TOP_H] + ((float)(petLevel/35)*(maxCamValues[TOP_H]-babyCamValues[TOP_H])),
            babyCamValues[TOP_R] + ((float)(petLevel/35)*(maxCamValues[TOP_R]-babyCamValues[TOP_R])),
            babyCamValues[MID_H] + ((float)(petLevel/35)*(maxCamValues[MID_H]-babyCamValues[MID_H])),
            babyCamValues[MID_R] + ((float)(petLevel/35)*(maxCamValues[MID_R]-babyCamValues[MID_R])),
            babyCamValues[BOT_H] + ((float)(petLevel/35)*(maxCamValues[BOT_H]-babyCamValues[BOT_H])),
            babyCamValues[BOT_R] + ((float)(petLevel/35)*(maxCamValues[BOT_R]-babyCamValues[BOT_R]))
        };
        cam.m_Orbits[0] = new CinemachineFreeLook.Orbit(values[TOP_H], values[TOP_R]);
        cam.m_Orbits[1] = new CinemachineFreeLook.Orbit(values[MID_H], values[MID_R]);
        cam.m_Orbits[2] = new CinemachineFreeLook.Orbit(values[BOT_H], values[BOT_R]);
        //cam.m_Orbits.SetValue((values[TOP_H], values[TOP_R]),0);
        //cam.m_Orbits.SetValue((values[MID_H], values[MID_R]), 1);
        //cam.m_Orbits.SetValue((values[BOT_H], values[BOT_R]), 2);

    }

    void TimerUpdates() 
    {
        if (attackDelayTimer > 0) { attackDelayTimer -= Time.deltaTime; }
    }

    void CheckKeyUpdates()
    {
        if (Input.GetKeyDown("left shift") && !isBlocking) 
        { 
            if (activePetCurrentEnergy <= 0) 
            { 
                playerMovementScript.speed = charStats.baseSpeed;
                isDashing = false;
                return; 
            }
            playerMovementScript.speed = activePetSprintSpeed * 2; isDashing = true; 
        }

        if (Input.GetKeyUp("left shift") && !isBlocking) 
        {
            playerMovementScript.speed = charStats.baseSpeed;
            isDashing = false;
        }

        if (Input.GetKey(KeyCode.Mouse0)) { Attack(); }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (activePetCurrentEnergy <= 0)
            {
                playerMovementScript.speed = charStats.baseSpeed;
                isBlocking = false;
                return;
            }
            isBlocking = true;
            playerMovementScript.speed = charStats.baseSpeed / 2;

        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            isBlocking = false;
            playerMovementScript.speed = charStats.baseSpeed;
        }
    }

    public void SetPet() 
    {
        Inventory.loadDataHolder();
        //Debug.Log(Inventory.selectedPet.name);
        CreateNewPetModel(Inventory.selectedPet);
        
    }

    public void CreateNewPetModel(Pet pet)
    {
        //Destroy(manisfestation);
        float sizeBoost = levelSizeScale * pet.Level;
        //Vector3 newSize = new Vector3(manisfestation.transform.localScale.x + sizeBoost, manisfestation.transform.localScale.y + sizeBoost, manisfestation.transform.localScale.z + sizeBoost);

        newPetModel = Instantiate(pet.getPhysicalManisfestation());
        newPetModel.transform.position = manisfestation.transform.position;
        newPetModel.transform.SetParent(manisfestationParent.transform);
        newPetModel.transform.localScale = manisfestation.transform.localScale;
        //newPetModel.transform.localScale = newSize;

        Vector3 newSize = new Vector3(1f + sizeBoost, 1f + sizeBoost, 1f + sizeBoost);

        transform.localScale = newSize;

        //newPetModel.layer = LayerMask.NameToLayer("Player Model");

        //newPetModel.transform.rotation.y = 180;
        manisfestation.SetActive(false);

        attackColiderHolder = newPetModel.transform.Find("Attack Box").gameObject;
        attackBoxCollider = attackColiderHolder.GetComponent<BoxCollider>();

        UpdateStats(pet);

        SetCamera(pet.Level);

    }
    public void UpdateStats(Pet pet) 
    {
        
        int[] stats = pet.getStats();

        charStats = new CharStats(stats);
        activePetSprintSpeed = charStats.baseSpeed * 2;

        Debug.Log("Attack Delay: " + charStats.atkSpd.ToString());

        playerMovementScript.speed = (float)(charStats.baseSpeed);
        SetCamera(pet.Level);
    }

    public void UpdateEnergy() 
    {
        float changeAMT = 0f;

        

        if (isBlocking)
        {
            changeAMT -= 2f *Time.deltaTime;
            //return;
        }
        if (isDashing) 
        {
            changeAMT -= 10f * Time.deltaTime;
        }

        EnergyChange(changeAMT);
        changeAMT = 0f;

        

        if (activePetCurrentEnergy < activePetFullEnergy) 
        {
            if (playerMovementScript.ANIM_STATE == 0)
            {
                if (jumpingScript.groundedPlayer)
                {
                    activePetCurrentEnergy += 0.5f;
                }
                else 
                {
                    activePetCurrentEnergy += 0.05f;
                }
                
            }
            else if (playerMovementScript.ANIM_STATE == 1 && !isDashing) { activePetCurrentEnergy += 0.05f; }
        }

        if (activePetCurrentEnergy <= 0) 
        {
            isBlocking = false;
            isDashing = false;
        }

    }

    public void EnergyChange(float change) 
    {
        activePetCurrentEnergy += change;
    }

    public void TakeDamage(int damage, float armorPen) 
    {
        float targetDefense = charStats.defense * (1 - armorPen);
        float multiplier = (targetDefense / (targetDefense + 100));
        int targetDamageTaken = damage - (int)(damage * multiplier);

        charStats.hp -= targetDamageTaken;
        Debug.Log("Player took " + targetDamageTaken.ToString() + " Damage");
    }

    public void Attack() 
    {
        if (attackDelayTimer > 0) { return; }

        if (animationController == null) { animationController = this.GetComponent<PlayerAnimationController>(); }
        Collider[] hitColliders = Physics.OverlapBox(attackColiderHolder.transform.position, attackBoxCollider.size , attackColiderHolder.transform.rotation, m_LayerMask);
        for (int i = 0; i < hitColliders.Length; i++) 
        {
            DamageAbleInterface testInterface = hitColliders[i].gameObject.GetComponent<DamageAbleInterface>();

            if ((testInterface != null) && !hitColliders[i].CompareTag("Player"))
            {
                int dam = charStats.damage;
                if (CombatHandler.isCrit(transform, hitColliders[i].gameObject.transform))
                {
                    dam *= 2;

                    Debug.Log("CRIT HIT");
                }
                else { Debug.Log("REGULAR HIT"); }
                //StartCoroutine(camEfx.Shake(attackDelay, 0.5f));
                testInterface.TakeDamage(dam, 1f);
            }
        }
        //animationController.SetState(2); animationController.attackTimer = 5f;
        StartCoroutine(animationController.PlayAnimationUntilDone("Attack", true));
        attackDelayTimer = charStats.atkSpd;
    }

    void OnDrawGizmos()
    {
        if (testStart)
        {
            Gizmos.DrawWireCube(attackColiderHolder.transform.position, attackBoxCollider.size);
        }
    }
}
