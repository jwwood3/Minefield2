﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Statics;

public class Spawner : MonoBehaviour
{
    float timer = 0.0f;
    int index;
    public GameObject Enemy;
    // Start is called before the first frame update
    void Start()
    {
        this.timer = 0.0f;
    }

    public void setIndex(int i)
    {
        index = i;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Statics.masterMind.gameState != 2)
        {
            //print("right state to spawn");
            float interval = (((float)Statics.masterMind.startX) / ((float)Statics.masterMind.x));
            if (timer >= interval)
            {
                //print("right time to spawn");
                timer -= interval;
                if (Random.Range(1, 101) < ((Statics.masterMind.spawnChance - (Statics.masterMind.spawnFactor * (Statics.masterMind.phase - 1))) + (Statics.masterMind.x - Statics.masterMind.minX)))
                {
                    //print("random spawn achieved");
                    GameObject enem = Instantiate(Enemy, this.transform.position, Quaternion.identity);
                    enem.GetComponent<Enemy_Script>().setIndex(index);
                }
            }
            timer += Time.fixedDeltaTime;
        }
    }
}
