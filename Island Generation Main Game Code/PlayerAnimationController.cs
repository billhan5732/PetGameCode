using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{

    public Animation anim;

    public int ANIM_STATE;

    public float attackTimer = 0f;

    public float currentClipDelay = 0f;
    public int overideCLipState = 0;

    private readonly int STATE_IDLE = 0;
    private readonly int STATE_MOVING = 1;
    private readonly int STATE_ATTACKING = 2;
    private readonly int STATE_TRANSITIONING = 3;

    string[] STATE_ARRAY = {"Idle", "Move", "Attack" };

    float animTimePlayed = 0f;

    bool overrided = false;

    void Update()
    {
        OverideAllForSeconds();
        AnimationHandler();
    }

    public void SetState(int NEW_STATE) 
    {
        if ((ANIM_STATE != NEW_STATE) && anim.isPlaying) 
        {
            RapidStateChange(ANIM_STATE, NEW_STATE);
            animTimePlayed = 0f;
            
        }
        ANIM_STATE = NEW_STATE; 
    }

    void RapidStateChange(int OLD_STATE, int NEW_STATE) 
    {
        //anim.CrossFade(STATE_ARRAY[NEW_STATE]);
        float duration = Mathf.Abs(anim[STATE_ARRAY[OLD_STATE]].length - animTimePlayed);
        StartCoroutine(RSCV2(NEW_STATE, duration));
        //Debug.Log("Animation Transition to: " + STATE_ARRAY[NEW_STATE]);
    }//{ anim.Play("Reset");anim.Play(STATE_ARRAY[NEW_STATE]); }

    IEnumerator RSCV2(int NEW_STATE, float time)
    {
        float elapsed = 0f;
        if (overrided) { yield break; }
        overrided = true;

        while (elapsed < time) 
        {
            //anim.Stop(STATE_ARRAY[ANIM_STATE]);
            anim.CrossFade(STATE_ARRAY[NEW_STATE], time);


            elapsed += Time.deltaTime;
            yield return null;
        }

        overrided = false;
    }

    public void OverideAnimationUntilDone(int state, string name) 
    {
        if (currentClipDelay > 0) { return; } 
        overideCLipState = state; 
        currentClipDelay = anim[name].length * anim[name].speed;
    }

    public void OverideAllForSeconds() { if (currentClipDelay > 0) { ANIM_STATE = overideCLipState; currentClipDelay -= Time.deltaTime; }  }

    void AnimationHandler() 
    {
        if (anim == null){anim = this.GetComponent<Player>().newPetModel.GetComponent<Animation>(); return; }

        if (overrided) { return; }

        switch (ANIM_STATE)
        {
            case 1://Move
                anim.Play("Move");
                //StartCoroutine(Delay(anim["Move"].length * anim["Move"].speed));
                break;
            case 2://Attack
                anim.Play("Attack");
                //StartCoroutine(PlayAnimationUntilDone("Attack"));
                //if (attackTimer > 0) { attackTimer -= Time.deltaTime; SetState(STATE_ATTACKING); if (attackTimer <= 0) { anim.Stop(); SetState(STATE_IDLE); } }
                break;
            default://Idling
                //anim["Move"].time = 0;
                //anim.Play("Reset");
                anim.Play("Idle");
                //StartCoroutine(Delay(anim["Idle"].length * anim["Idle"].speed));
                break;
        }
        animTimePlayed += Time.deltaTime;
    }

    public IEnumerator PlayAnimationUntilDone(string animationName, bool priority = false) 
    {

        float elapsed = 0f;
        float length = anim[animationName].length;

        if (overrided && !(priority)) { yield break; }

        overrided = true;

        while (elapsed < length) 
        {
            anim.Play(animationName);

            elapsed += Time.deltaTime;
            yield return null;
        }

        anim.Stop();

        overrided = false;
    }

    IEnumerator Delay(float duration) 
    {
        yield return new WaitForSeconds(duration);
        //anim.Stop();
    }
}
