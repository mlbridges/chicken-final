using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//player's dictionary for inventory, attached to an empty game object
public class InventoryDict : MonoBehaviour
{
    //initializing a dictionary variable
    public Dictionary<string, int> GemInv;

    //creating events for the gem eater to subscribe to
    public delegate void DoSomething();
    public static event DoSomething OnHealthGemAdded;
    public static event DoSomething OnSpeedGemAdded;
    public static event DoSomething OnStrengthGemAdded;
    public delegate void DoRemove();
    public static event DoRemove OnHealthGemRemoved;
    public static event DoRemove OnSpeedGemRemoved;
    public static event DoRemove OnStrengthGemRemoved;

    public int currentAmount;

    private void Start()
    {
        //on start we create the dictionary new every time so there's no hold over from last play
        GemInv = new Dictionary<string, int>();

        //we also subscribe the dictionary to the objectcollection script's messages
        ObjectCollection.OnPlayerColl += AddGem;

        GemEating.gemRemove += RemoveGem;
    }

    //on every frame we're outputing the dictionary to see if it's working
    private void Update()
    {
        foreach (var item in GemInv) 
        {
            //Debug.Log("this happened");
            //printing the name of the item and how many we're holding
            Debug.Log(item.Key + ": " + item.Value);
        }
    }

    //function which is fired whenever this script receives the player collision message
    public void AddGem(string _tag) 
    {
        //right now the amount of gems added is always one, but I may restructure later if we foresee a case in which that would be different
        int amount = 1;

        //currentAmount will check and see how many of each item we already possess
        currentAmount = 0;

        //if this function goes to the dictionary and finds no entry named for the gem we just picked up
        if (!GemInv.TryGetValue(_tag, out currentAmount)) 
        {
            //it'll assume we have none currently in our inventory
            currentAmount = 0;
        }

        //if the currentAmount and the amount we're adding (right now 1) add up to more than zero
        //which they always will, but we are playing it safe
        //then we are adding the amount to the currentAmount
        if (currentAmount + amount > 0)
        {
            currentAmount += amount;
        }
        //if they add up to zero or less than zero, we always set currentAmount back to zero
        //this doesn't look useful now but it will help us when we need to remove stuff from the inventory
        else 
        {
            currentAmount = 0;
        }

        //this removes the entry in the dictionary for this type of gem
        GemInv.Remove(_tag);

        //and then if currentAmount is greater than 0 (i.e. we have adding something, and not removed it)
        if (currentAmount > 0)
        { 
            //it puts the entry back into the dictionary with the same name and the increased quantity
            GemInv.Add(_tag, currentAmount);

            //from molly: if the current amount is greater than zero then the player should also be able to eat the gem
            //we should have an event system in this script, which the player then subscribes to
            //this is the spot where the bat signal to turn on gem eating gets sent to the player:
            if (_tag == "health gems")
            {
                if (OnHealthGemAdded != null)
                {
                    OnHealthGemAdded();
                    //Debug.Log("heart gem eating enabled");
                }
            }

            if (_tag == "speed gems")
            {
                if (OnSpeedGemAdded != null)
                {
                    OnSpeedGemAdded();
                    //Debug.Log("speed gem eating enabled");
                }
            }

            if (_tag == "strength gems")
            {
                if (OnStrengthGemAdded != null)
                {
                    OnStrengthGemAdded();
                    //Debug.Log("strength gem eating enabled");
                }
            }
            
        }
    }

    public void RemoveGem(string gemType)
    {
        //Debug.Log("remove gem function is called");
        //needs to: receive what type of gem it is (screen the tag)
        //subtract one from the int amount
        //if the amount were less than 0, turn off gem eating in GemEating
        //right now the amount of gems added is always one, but I may restructure later if we foresee a case in which that would be different
        int amount = -1;

        //currentAmount will check and see how many of each item we already possess
        currentAmount = 0;

        //if this function goes to the dictionary and finds no entry named for the gem we just picked up
        if (!GemInv.TryGetValue(gemType, out currentAmount))
        {
            //it'll assume we have none currently in our inventory
            currentAmount = 0;
        }

        //if the currentAmount and the amount we're adding (right now 1) add up to more than zero
        //which they always will, but we are playing it safe
        //then we are adding the amount to the currentAmount
        if (currentAmount + amount > 0)
        {
            currentAmount += amount;
        }
        //if they add up to zero or less than zero, we always set currentAmount back to zero
        //this doesn't look useful now but it will help us when we need to remove stuff from the inventory
        else
        {
            currentAmount = 0;
        }

        //this removes the entry in the dictionary for this type of gem
        GemInv.Remove(gemType);

        //and then if currentAmount is greater than 0 (i.e. we have adding something, and not removed it)
        if (currentAmount > 0)
        {
            //it puts the entry back into the dictionary with the same name and the increased quantity
            GemInv.Add(gemType, currentAmount);
        }
        else
        {
            //from molly: if the current amount is less than zero, you should not be able to continue eating gems
            //we should have an event system in this script, which the player then subscribes to
            //this is the spot where the bat signal to turn off gem eating gets sent to the player:
            if (gemType == "health gems")
            {
                if (OnHealthGemRemoved != null)
                {
                    OnHealthGemRemoved();
                    //Debug.Log("heart gem eating enabled");
                }
            }

            if (gemType == "speed gems")
            {
                if (OnSpeedGemRemoved != null)
                {
                    OnSpeedGemRemoved();
                    //Debug.Log("speed gem eating enabled");
                }
            }

            if (gemType == "strength gems")
            {
                if (OnStrengthGemRemoved != null)
                {
                    OnStrengthGemRemoved();
                    //Debug.Log("strength gem eating enabled");
                }
            }
        }
    }
}
