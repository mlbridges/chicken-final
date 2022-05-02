using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using UnityEngine.SceneManagement;

public class EnemyControl : MonoBehaviour
{
    //the animator on this is way fucked up please send help - perpetually walking forward
    public Transform[] Waypoints;
    private Transform currentDestination;
    private int finalIndex;
    private int curIndex = 0;
    private float minDist = 0.1f;
    private NavMeshAgent enemyAgent;

    public GameObject player;

    public PointCounter lives;

    private Animator anim;
    private const string idle_bool = "Idle";
    private const string walk_bool = "Walk Forward";
    private const string attack_trigger = "Stab Attack";

    private bool isAttacking = false;
    // Start is called before the first frame update
    void Start()
    {
        //following Mig's waypoints code example for the patrol element of this script

        //setting up a variable for the enemy's NavMesh
        enemyAgent = GetComponent<NavMeshAgent>();

        //marking the final waypoint so we can cycle through them
        finalIndex = Waypoints.Length;

        //setting the starting destination as zero/the first waypoint
        currentDestination = Waypoints[curIndex];

        //assigning the animator to the anim variable
        anim = GetComponent<Animator>();

        //starting the enemy at the idle animation
        AnimateIdle();
        anim.SetBool(walk_bool, false);
    }

    // Update is called once per frame
    void Update()
    {
        DetectPlayer();

        WaypointNav();

        AttackMode();
    }

    public void OnCollisionEnter(Collision collision)
    {
        //setting the enemy to attack when it collides with the player
        //but I couldn't get this to work
        //AnimateAttack();

        //Debug.Log("this is occurring");
        if (collision.transform == player.transform)
        {
            
            isAttacking = true;
            Debug.Log(lives.healthValue);
            lives.healthValue--;

            if (lives.healthValue <= 0)
            {
                //Destroy(player, 0.1f);
                Debug.Log("you died");
                //load the game over screen
            }
        }
    }

    //function to force fire the attack animation because it was being overridden by the walk animation
    //when I put it inside the walking functions
    void AttackMode()
    {
        if (isAttacking == true)
        {
            AnimateAttack();
        }
    }

    //function to keep the enemies patrolling
    void WaypointNav() 
    {
        //setting the enemy to walk
        AnimateWalk();
        //checking to see if the enemy has reached the waypoint
        if (enemyAgent.remainingDistance <= minDist)
        {
            //if they are close enough, the destination iterates to the next one
            curIndex++;

            //checking to see if the current index is bigger than the number of waypoints
            if (curIndex >= finalIndex)
            {
                //and reseting it to the beginning if it is
                curIndex = 0;
            }

            //setting the destination to the current index
            currentDestination = Waypoints[curIndex];
        }

        //sending the enemy to the current destination
        enemyAgent.SetDestination(currentDestination.position);

    }

    //function to detect player via raycasting
    void DetectPlayer()
    {
        //creating a ray between the enemy and the player
        Ray ray = new Ray(transform.position, player.transform.position - transform.position);

        //draws the ray
        Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);

        RaycastHit hit;

        Physics.Raycast(ray, out hit);

        if (hit.collider.tag == "Player")
        {
            //another place where making the enemy attack didn't work
            //AnimateAttack();
            Debug.Log("enemy sees player");
            currentDestination = hit.transform;
        }
        else 
        {
            //setting the attack variable back to false as they return to their routine
            isAttacking = false;
            
            //animating the walk
            AnimateIdle();

            //going back on patrol
            currentDestination = Waypoints[curIndex];
        }
    }


    //animation functions all based on the example video from Omnirift

    //function to fire Idle animation
    public void AnimateIdle()
    {
        anim.SetBool(idle_bool, true);
        anim.SetBool(walk_bool, false);
        //Animate(idle_bool);
    }

    //function to fire walk animation
    public void AnimateWalk()
    {
        anim.SetBool(idle_bool, false);
        anim.SetBool(walk_bool, true);
        //Animate(walk_bool);
    }

    //function to fire attack animation
    public void AnimateAttack()
    {
        anim.SetBool(walk_bool, false);
        anim.SetBool(idle_bool, false);
        anim.SetTrigger(attack_trigger);
        //Animate(attack_trigger);
    }

    //function to set any animation going
    //private void Animate(string boolName)
    //{
    //    ////turning off any other animations
    //    //DisableUnusedAnims(anim, boolName);

    //    ////activating the desired animation
    //    //anim.SetBool(boolName, true);
    //}

    ////function to turn off undesired animations
    //private void DisableUnusedAnims(Animator animator, string animation)
    //{
    //    //iterating through all available animations
    //    foreach (AnimatorControllerParameter parameter in animator.parameters)
    //    {
    //        //checking if their name matches the desired name
    //        if (parameter.name != animation)
    //        {
    //            //and turning them off if it doesn't
    //            animator.SetBool(parameter.name, false);
    //        }
    //    }
    //}
}
