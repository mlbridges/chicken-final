using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class enemyControl2 : MonoBehaviour
{
    //referring to ChickenMoveAnims
    public ChickenMoveAnims playerScript;
    //ray is looking for the player, so we have to refer to the player
    public GameObject player;
    //waypoint (sort of) for enemy to return to if it loses the player
    public Transform guardPost;
    //enemy animator
    private Animator anim;
    //player's health counter
    public PointCounter playerStats;
    //save the enemy start position
    //private float enemyStartPos;
    private bool enemyAttack;

    public float health = 6;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        //enemyStartPos = transform.rotation.y;
        enemyAttack = true;
        //subscribe to damage taking event
        ChickenMoveAnims.onPlayerHit += TakeDamage;
    }

    // Update is called once per frame
    void Update()
    {
        checkLocation();
    }

    void checkLocation()
    {
        RaycastHit hit;
        //creating and drawing a ray from the enemy to the player
        if (Physics.Raycast(transform.position, player.transform.position - transform.position, out hit))
        {
            //if we hit the player, then walk toward the player
            if (hit.transform == player.transform)
            {
                Debug.Log("enemy sees player");
                //set destination to the player's position
                transform.GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
                //set anim to walking
                if(Vector3.Distance(transform.position, player.transform.position) > 2)
                {
                    WalkAnim();
                }
                //if enemy is close enough to player, attack the player and do damage
                if(Vector3.Distance(transform.position, player.transform.position) <= 2)
                {
                    Debug.Log("enemy close to player");
                    if(enemyAttack == true)
                    {
                        StartCoroutine(AttackPlayer());
                    }
                }
            }
            else
            {
                //walk back to the guard post
                transform.GetComponent<NavMeshAgent>().SetDestination(guardPost.position);
                Debug.Log("walking to guard post");
                //if enemy is far away from guard post, set anim to walking
                if (Vector3.Distance(transform.position, guardPost.position) > 2)
                {
                    WalkAnim();
                }
                //if enemy is close to guard post, set anim to idle
                if (Vector3.Distance(transform.position, guardPost.position) <= 2)
                {
                    Debug.Log("close to guard post");
                    IdleAnim();
                    //make the enemy turn around and face away from the wall, like it does in starting position
                    //transform.rotation = new Vector3(0, enemyStartPos, 0);
                }
            }
        }
    }

    IEnumerator AttackPlayer()
    {
        //the enemy has hit the player!
        Debug.Log("AttackPlayer is called");
        //stop the enemy so it doesn't get on top of the player
        //so, if we collide with the player, we want the following things to happen, IN THIS ORDER:
        //1. the attack animation plays
        AttackAnim();
        //2. enemy deals damage to the player
        playerStats.healthValue--;
        //3. enemy waits a few seconds, not attacking the player (player is briefly invincible? enemy speed 0?)
        enemyAttack = false;
        Debug.Log("enemy can't attack rn");
        //wait a few seconds
        yield return new WaitForSeconds(3);
        //4. enemy continues its pursuit of the player
        //turn on enemy collider again? turn raycasting back on?
        enemyAttack = true;
        //AttackAnim();
        Debug.Log("enemy can attack again");

        if(playerStats.healthValue <= 0)
        {
            //go to the game over scene
            SceneManager.LoadScene(8);
            Debug.Log("game over");
        }
    }

    public void TakeDamage()
    {
        //if the enemy is close to the player
        if(Vector3.Distance(transform.position, player.transform.position) <= 2)
        {
            health -= playerScript.playerStrength;
            if(health <= 0)
            {
                //error message: basically i'm removing the gameobject from the list but the list is still looking for it
                //playerScript.Enemies.Remove(gameObject);
                gameObject.SetActive(false);
                playerScript.enemyKills += 1;
                //playerScript.Enemies.Count -= 1;
            }
        }
    }

    void WalkAnim()
    {
        //set anim to walking
        anim.SetBool("walking", true);
        anim.SetBool("idle", false);
    }

    void IdleAnim()
    {
        //set anim to idle
        anim.SetBool("walking", false);
        anim.SetBool("idle", true);
    }

    void AttackAnim()
    {
        //set anim to attacking
        anim.SetBool("walking", false);
        anim.SetBool("idle", false);
        anim.SetTrigger("is attacking");
    }
}
