using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Image inventoryImage;
    public Transform inventory;
    public GameObject HealthGem;
    public GameObject SpeedGem;
    public GameObject StrengthGem;
    public GemSelect[] InventoryButtons;
    public InventoryDict InventoryDicts;
    // Start is called before the first frame update
    void Start()
    {
        inventoryImage.enabled = true;
        Debug.Log("inventory state: " + inventoryImage.enabled);
        //inventory.gameObject.SetActive(false);
    }
    void UpdateButtons()
    {
        foreach(var button in InventoryButtons)
        {
            Debug.Log("buttons should be set false");
            button.gameObject.GetComponent<Image>().enabled = false;
        }
        // for each item in the inventorydict 
        // set GemSelect GemType 
        int Index = 0;
        foreach (var item in InventoryDicts.GemInv)
        {
            Debug.Log("something");
            InventoryButtons[Index].GemType = item.Key;
            InventoryButtons[Index].gameObject.GetComponent<Image>().enabled = true;
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
    private void Update()
    {
        InventoryDisplay();
    }

    void InventoryDisplay() {
        // menu summoned with escape key
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (inventoryImage.enabled == true) 
            {
                inventoryImage.enabled = false;
                Debug.Log("inventory state: " + inventoryImage.enabled);
            }

            if(inventoryImage.enabled == false)
            {
                inventoryImage.enabled = true;
                Debug.Log("inventory state: " + inventoryImage.enabled);
                UpdateButtons();
            }
        }
    }
}
