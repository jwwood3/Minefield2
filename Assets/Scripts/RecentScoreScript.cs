using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RecentScoreScript : MonoBehaviour
{

    public TextMeshProUGUI score;

    // Use this for initialization
    void Update()
    {
        score.text = "<" + Statics.masterMind.score.ToString() + ">";
    }

}
