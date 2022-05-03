using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdultChickenAnim : MonoBehaviour
{
    //this is a script to control the animations for the chicken NPC

    //variable to hold the animator for the chicken NPC
    Animator anim;

    //variables to fire the different animations
    private const string walk_bool = "Walk";
    private const string run_bool = "Run";
    private const string eat_bool = "Eat";
    private const string turn_bool = "Turn Head";
    private const string idle_bool = "Idle";

    // Start is called before the first frame update
    void Start()
    {
        //assigning the animator to the anim variable
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimateTurn();
    }

    //function to play an animation while turning off the other animations
    public void Animate(string boolName)
    {
        DisableUnusedAnims(anim, boolName);

        anim.SetBool(boolName, true);
    }

    //function to turn off all animations except for the one designated in the function
    private void DisableUnusedAnims(Animator animator, string animation)
    {
        //iterating through all available animations
        foreach (AnimatorControllerParameter parameter in animator.parameters)
        {
            //checking if their name matches the desired name
            if (parameter.name != animation)
            {
                //and turning them off if it doesn't
                animator.SetBool(parameter.name, false);
            }
        }
    }

    //functions that play each of the chicken's animations
    public void AnimateIdle()
    {
        Animate(idle_bool);
    }

    public void AnimateWalk()
    {
        Animate(walk_bool);
    }

    public void AnimateEat()
    {
        Animate(eat_bool);
    }

    public void AnimateTurn()
    {
        Animate(turn_bool);
    }

    public void AnimateRun()
    {
        Animate(run_bool);
    }
}
