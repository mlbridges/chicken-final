using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthTracker : MonoBehaviour
{
    public PointCounter health;
    Text healthScore;
    // Start is called before the first frame update
    void Start()
    {
        healthScore = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        healthScore.text = "Health: " + health.healthValue;
    }
}
