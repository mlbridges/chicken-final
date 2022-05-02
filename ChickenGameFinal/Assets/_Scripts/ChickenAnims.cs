using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is fucked beyond belief
//it's really hard to merge new character animations with the premade character controller
//so until I can make that work this is going to be a duck-taped together substitute
public class ChickenAnims : MonoBehaviour
{
    //storing the chicken's animator
    public Animator ChickenAnim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //calling functions for the chicken's animations (except the ones that don't work
        ChickenIdle();

        //ChickenWalk();

        //ChickenRun();

        ChickenJump();

        ChickenEat();
    }

    //running the idle animation and shutting down the other ones
    public void ChickenIdle()
    {
        ChickenAnim.SetBool("Idle", true);
        ChickenAnim.SetBool("Eat", false);
        ChickenAnim.SetBool("Run", false);
        ChickenAnim.SetBool("Walk", false);
        ChickenAnim.SetBool("Jump", false);
    }

    //I need to figure out a better solution for the run/walk animation conundrum
    /*public void ChickenWalk()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            ChickenAnim.SetBool("Walk", true);
            ChickenAnim.SetBool("Jump", false);
        }
    }*/

    /*public void ChickenRun()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ChickenAnim.SetBool("Run", true);
            ChickenAnim.SetBool("Walk", false);
            ChickenAnim.SetBool("Eat", false);
            ChickenAnim.SetBool("Idle", false);
            ChickenAnim.SetBool("Jump", false);
        }
    }*/

    //running the jump animation when the player hits space (it doesn't work great atm)
    public void ChickenJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChickenAnim.SetBool("Jump", true);
            ChickenAnim.SetBool("Eat", false);
            ChickenAnim.SetBool("Run", false);
            ChickenAnim.SetBool("Walk", false);
            ChickenAnim.SetBool("Idle", false);
        }
    }

    //running the eat animation when the player hits j (this is at least pretty cute)
    public void ChickenEat() 
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            ChickenAnim.SetBool("Eat", true); 
            ChickenAnim.SetBool("Run", false);
            ChickenAnim.SetBool("Walk", false);
            ChickenAnim.SetBool("Idle", false);
            ChickenAnim.SetBool("Jump", false);
        }
    }
}
