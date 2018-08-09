using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class NPCHandler : MonoBehaviour {

    private Animator animator;
    // Use this for initialization
    void Start () {
        // existing components on the GameObject
        animator = GetComponent<Animator>();
        AnimationClip clip;
        Animator anim;

        // new event created
        AnimationEvent evt;
        evt = new AnimationEvent();

        // put some parameters on the AnimationEvent
        //  - call the function called PrintEvent()
        //  - the animation on this object lasts 2 seconds
        //    and the new animation created here is
        //    set up to happens 1.3s into the animation
        evt.intParameter = 12345;
        evt.time = 1.3f;
        evt.functionName = "PerformAttack";

        // get the animation clip and add the AnimationEvent
        anim = GetComponent<Animator>();
        clip = anim.runtimeAnimatorController.animationClips[0];
        clip.AddEvent(evt);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void PerformAttack()
    {
        animator.SetBool("isWalking", true);
    }
}
