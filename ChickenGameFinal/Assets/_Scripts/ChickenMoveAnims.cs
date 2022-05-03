using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChickenMoveAnims : MonoBehaviour
{
    //enemy health stuff
    public delegate void PlaceholderName();
    public static event PlaceholderName onPlayerHit;
    public float enemyKills = 0;
    public float playerStrength = 1;

    //this is based heavily on the Third Person Movement Brackeys video
    //public variables for character controller and camera
    public CharacterController controller;
    public Transform cam;

    public Animator anim;

    //speed of movement
    public float speed = 6f;

    //values used for smoothing the rotation of the character and camera
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    //jump stuff
    public float gravity = -9.81f;
    public float jumpHeight = 3;
    private Vector3 velocity;
    private bool isGrounded;

    //setting an object to check where the ground is
    public Transform groundCheck;
    //how far away the ground can be
    public float groundDistance = 0.4f;
    //and what layer is legally the ground
    public LayerMask groundMask;

    //checking how close the chicken needs to be to an enemy to attack
    public float attackDistance = 2f;
    //a list of gameObjects that the chicken will consider an enemy
    public List<GameObject> Enemies;

    void Start() 
    {
        //add all items tagged "enemy" to the enemies list?
    }
    // Update is called once per frame
    void Update()
    {
        ChickenWalk();

        ChickenJump();

        ChickenAttack();
    }

    public void ChickenWalk() 
    {
        //getting the movement input from the WASD or Arrow Keys in the horizontal and vertical directions
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        //putting those inputs into a normalized vector for the character to travel along
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        //if the magnitude along that vector is significant
        if (direction.magnitude >= 0.1f)
        {
            //move the character
            anim.SetBool("Walk", true);
            anim.SetBool("Idle", false);
            //calculating the angle of the movement and attaching it to the direction the camera is pointing
            //(I don't fully understand the math here, but I am trying)
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            //smoothing the angle of rotation for the character
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //calculating the direction to move the character
            Vector3 MoveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            //injecting that movement into the character (and normalizing it and making it update by frame so it looks smooth)
            controller.Move(MoveDir.normalized * speed * Time.deltaTime);
        }
        else 
        {
            anim.SetBool("Idle", true);
            anim.SetBool("Walk", false);
        }
    }

    //function to make the chicken jump
    public void ChickenJump()
    {
        //bool checking if the chicken is currently on the objects designated as the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //if the chicken is grounded and its y velocity is less than 0
        if (isGrounded && velocity.y < 0) 
        {
            //keep it against the ground with negative velocity
            velocity.y = -2f;
        }

        //if the player hits Space while the chicken is on the ground
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            //play the jump animation
            anim.SetBool("Jump", true);

            //also add velocity to the chicken to launch it into the air
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
        else 
        {
            //if the chicken's not jumping, don't play the jump animation
            anim.SetBool("Jump", false);
        }

        //also always being calculating the effect of gravity on the chicken
        velocity.y += gravity * Time.deltaTime;
        //and apply it to the chicken
        controller.Move(velocity * Time.deltaTime);
    }

    //shell function so the chicken can engage in combat
    public void ChickenAttack() 
    {
        //check to see if there are enemies in the Enemies list
        if (Enemies != null) 
        {
            //Debug.Log("this happened");

            //run through each of the enemies
            foreach (var enemy in Enemies) 
            {
                //calculate how close the chicken is to the enemy
                var dist = Vector3.Distance(enemy.transform.position, gameObject.transform.position);

                //if the distance between chicken and enemy is small enough
                if (dist < attackDistance)
                {
                    //Debug.Log("this is actually happening");

                    //if the player hits the designated key (currently J but can be changed)
                    if (Input.GetKeyDown(KeyCode.J))
                    {
                        //Debug.Log("truly it's happening");

                        //play the attack animation
                        anim.SetBool("Eat", true);

                        //and hopefully send a message to the enemy that it is being damaged
                        if(onPlayerHit != null)
                        {
                            onPlayerHit();
                        }

                        //if we killed all the enemies, play the win scene
                        if (enemyKills >= 3)
                        {
                            //play the win scene
                            SceneManager.LoadScene(2);
                            Debug.Log("you won");
                        }
                    }
                    else
                    {
                        //if the player's not pressing the Attack key, don't play the Attack animation
                        anim.SetBool("Eat", false);
                    }
                }
            }
        }
    }
}
