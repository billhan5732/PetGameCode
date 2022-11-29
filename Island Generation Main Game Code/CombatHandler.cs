using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatHandler
{
    public static bool isCrit(Transform self, Transform target) 
    {
        float selfDir = Mathf.Min(self.eulerAngles.y, 360-self.eulerAngles.y);
        float targDir = Mathf.Min(target.eulerAngles.y + 180, 360 - (target.eulerAngles.y + 180));//target.eulerAngles.y;
        float angle = Vector3.Angle(self.forward, target.position - self.position);
        //Debug.Log(angle);
        Debug.Log(selfDir.ToString() +" | " + targDir.ToString());

        if (Mathf.Abs(selfDir - targDir) < 60)
        {
            return true;
        }
        return false;
        /*
        if (Mathf.Abs(angle) < 15)
        {
            
            if (Mathf.Abs(selfDir - targDir) < 30) 
            {
                return true;
            }
            return false;
            
        }
        else 
        {
            return false;
        }
        */

    }
}
