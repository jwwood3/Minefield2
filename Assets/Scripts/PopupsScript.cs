using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Statics;

public class PopupsScript : MonoBehaviour
{

    public bool isPoint;
    float life;
    float fullLife;
    float timer;
    Color trueColor;
    SpriteRenderer sr;
    Collider2D c2D;
    // Use this for initialization
    void Start()
    {
        timer = Statics.masterMind.itemBuffer;
        if (isPoint)
        {
            life = Statics.masterMind.pointDuration;
        }
        else
        {
            life = Statics.masterMind.powerUpDuration;
        }
        float x = (float)((Random.value * Statics.masterMind.screenWidth*2) - Statics.masterMind.screenWidth);
        float y = (float)((Random.value * Statics.masterMind.screenHeight*1.5) - (Statics.masterMind.screenHeight*0.75));
        c2D = this.gameObject.GetComponent<Collider2D>();
        sr = this.gameObject.GetComponent<SpriteRenderer>();
        trueColor = sr.color;
        sr.color = Color.yellow;
        c2D.enabled = false;
        this.gameObject.transform.position = new Vector3(x, y, 1);
        fullLife = life;
    }

    void FixedUpdate()
    {
        if (timer > 0)
        {
            timer -= Time.fixedDeltaTime;
        }
        else
        {
            if (life == fullLife)
            {
                c2D.enabled = true;
                sr.color = trueColor;
            }
            life -= Time.fixedDeltaTime;
            if (life <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }


}
