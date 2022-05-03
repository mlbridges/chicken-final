using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//a script for the player to be able to eat gems, which is attached to the player
public class GemEating : InventoryDict
{
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
    }

    // Update is called once per frame
    void Update()
    {
        //i'm sure there's a more efficient way than to put this in update but this works for now
        if (HealthGemEatingAbility == true)
        {
            if (Input.GetKeyDown(KeyCode.O))
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
            if (Input.GetKeyDown(KeyCode.I))
            {
                //increase speed
                //playerAnims.speed += 0.3f;

                //tell the UI they gained 1 speed
                score.speedValue ++;

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
