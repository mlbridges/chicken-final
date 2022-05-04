using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public Transform inventory;
    public GameObject HealthGem;
    public GameObject SpeedGem;
    public GameObject StrengthGem;
    public GemSelect[] InventoryButtons;
    public InventoryDict InventoryDicts;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void UpdateButtons()
    {
        foreach(var button in InventoryButtons)
        {
            button.gameObject.SetActive(false);
        }
        // for each item in the inventorydict 
        // set GemSelect GemType 
        int Index = 0;
        foreach (var item in InventoryDicts.GemInv)
        {
            Debug.Log("something");
            InventoryButtons[Index].GemType = item.Key;
            InventoryButtons[Index].gameObject.SetActive(true);
            Index += 1;
            /*            if(item.Key = "health gems")
                        {
                            //make the health gem button visible/clickable
                            if (health gem button is clicked)
                            {
                                send the event message to gemeating to do the health gem eating
                            }
                        } */
        }
    }
    // Update is called once per frame
    void Update() {
        // menu summoned with escape key
        if (Input.GetKeyDown(KeyCode.LeftShift))
    {
            if (inventory.gameObject.activeSelf) 
            {
                inventory.gameObject.SetActive(false);
            }
            else
            {
                inventory.gameObject.SetActive(true);
                UpdateButtons();
            }
    }
    }
}
