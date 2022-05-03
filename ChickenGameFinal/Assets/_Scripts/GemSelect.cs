using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSelect : MonoBehaviour
{
    // Start is called before the first frame update
    public string GemType;
    public void ClickOnGem()
    {
        //call function based on gem type
        Debug.Log(GemType);
    }
}