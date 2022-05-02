using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedTracker : MonoBehaviour
{
    public PointCounter speed;
    Text speedScore;
    // Start is called before the first frame update
    void Start()
    {
        speedScore = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        speedScore.text = "Speed: " + speed.speedValue;
    }
}
