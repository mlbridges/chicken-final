using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script for object collection, attached to gems which the player collects
public class ObjectCollection : MonoBehaviour
{
    public GameObject player;
    public float captureDistance = 0.8f;

    //creating an event handler for the inventory dictionary to subscribe to
    //the dictionary will receive the tags of the collectables, which it uses to name the keys in the dictionary
    public delegate void Placeholder(string _tag);
    public static event Placeholder OnPlayerColl;

    //value to be used if we want to move or hide the objects instead of destroying them
    //int y = -100;

    private void PlayerProximity() 
    {
        var dist = Vector3.Distance(player.transform.position, gameObject.transform.position);
        //Debug.Log(dist);

        if (dist < captureDistance) 
        {
            //Debug.Log("this happened");

            //sending the message out to the dictionary
            OnPlayerColl?.Invoke(gameObject.tag);

            //keeping this line in case we want to hide the objects instead of destroying them
            //transform.position = new Vector3(0, y, 0);

            //destroying the objects on collection
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        PlayerProximity();
    }
}