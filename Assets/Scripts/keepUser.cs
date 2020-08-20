using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class keepUser : MonoBehaviour
{

    public TMP_InputField field;

    // Use this for initialization
    void Start()
    {
        if (Statics.masterMind.currentUser != "Player 1")
        {
            field.text = Statics.masterMind.currentUser;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
