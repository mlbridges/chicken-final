using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//a script for the player to be able to eat gems, which is attached to the player
public class GemEating : InventoryDict
{
    public ChickenMoveAnims player;
    private float speedInc = 0.2f;
    public delegate void SendDictMessage(string gemType);
    public static event SendDictMessage gemRemove;

    //trying to refer to the character controller
    //public ChickenMoveAnims playerAnims;

    //bools to set gem usage based on event triggering
    private bool HealthGemEatingAbility = false;
    private bool SpeedGemEatingAbility = false;
    private bool StrengthGemEatingAbility = false;

    public PointCounter score;
    // Start is called before the first frame update
    void Start()
    {
        //subscribing to the event to turn on gem eating
        InventoryDict.OnHealthGemAdded += EatHealthGem;
        InventoryDict.OnSpeedGemAdded += EatSpeedGem;
        InventoryDict.OnStrengthGemAdded += EatStrengthGem;

        //subscribing to the event to turn OFF gem eating
        InventoryDict.OnHealthGemRemoved += RemoveHealthGem;
        InventoryDict.OnSpeedGemRemoved += RemoveSpeedGem;
        InventoryDict.OnStrengthGemRemoved += RemoveStrengthGem;

        //subscribing to the event to eat the gems (from the menu manager script)

    }

    // Update is called once per frame
    void Update()
    {
        //i'm sure there's a more efficient way than to put this in update but this works for now
        //more efficient way: when the buttons are clicked (via menu manager script), these lines of code
        //will fire in separate functions (which are subscribed to the event in the menu manager script)
        if (HealthGemEatingAbility == true)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                //tell the UI they gained 1 health
                score.healthValue++;

                //find and remove one heart gem from the dictionary
                if (gemRemove != null)
                {
                    gemRemove("health gems");
                }

                //if the number of gems of heart type <= 0, disable HeartGemEatingAbility
            }
        }

        if(SpeedGemEatingAbility == true)
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                //increase speed
                //playerAnims.speed += 0.3f;

                //tell the UI they gained 1 speed + actually change player speed
                score.speedValue ++;
                player.speed += speedInc;

                //find and remove one speed gem from dict
                if (gemRemove != null)
                {
                    gemRemove("speed gems");
                }
            }
        }

        if (StrengthGemEatingAbility == true)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                //tell the UI they gained 1 strength
                score.strengthValue ++;
                //actually increase strength
                player.playerStrength ++;
                //Debug.Log("player strength: " + player.playerStrength);

                //find and remove one strength gem from dict
                if (gemRemove != null)
                {
                    gemRemove("strength gems");
                }
            }
        }
    }
    
    public void EatHealthGem()
    {
        //turns on gem eating ability
        if (HealthGemEatingAbility == false)
        {
            HealthGemEatingAbility = true;
        }
        //Debug.Log("health gem status: " + HealthGemEatingAbility);
    }

    public void EatSpeedGem()
    {
        //turns on gem eating ability
        if (SpeedGemEatingAbility == false)
        {
            SpeedGemEatingAbility = true;
        }
        //Debug.Log("speed gem status: " + SpeedGemEatingAbility);
    }

    public void EatStrengthGem()
    {
        //turns on gem eating ability
        if (StrengthGemEatingAbility == false)
        {
            StrengthGemEatingAbility = true;
        }
        //Debug.Log("strength gem status: " + StrengthGemEatingAbility);
    }

    public void RemoveHealthGem()
    {
        if (HealthGemEatingAbility == true)
        {
            HealthGemEatingAbility = false;
        }
    }

    public void RemoveSpeedGem()
    {
        if (SpeedGemEatingAbility == true)
        {
            SpeedGemEatingAbility = false;
        }
    }

    public void RemoveStrengthGem()
    {
        if (StrengthGemEatingAbility == true)
        {
            StrengthGemEatingAbility = false;
        }
    }
}
