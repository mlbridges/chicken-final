using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrengthTracker : MonoBehaviour
{
    public PointCounter strength;
    Text strengthScore;
    // Start is called before the first frame update
    void Start()
    {
        strengthScore = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        strengthScore.text = "Strength: " + strength.strengthValue;
    }
}
