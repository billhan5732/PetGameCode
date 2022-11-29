using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour
{
    //Using

    public int hp;
    public int damage;
    public int defense;
    public int speed;

    public int level;

    public float atkSpd;

    //Base

    public int fullHP;
    public int baseDamage;
    public int baseDefense;
    public int baseSpeed;
    //float baseAtkSpd;


    public void SetUp(int[] statArray) 
    {
        fullHP = statArray[0];
        baseDamage = statArray[1];
        baseDefense = statArray[2];
        baseSpeed = statArray[3];
        atkSpd = (float)(5f / statArray[3]);

        hp = fullHP;
        damage = baseDamage;
        defense = baseDefense;
        speed = baseSpeed;
        
    }

    public CharStats() { }
    public CharStats(int[] arr) 
    {
        SetUp(arr);
    }

}
