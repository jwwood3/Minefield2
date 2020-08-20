using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupsScript : MonoBehaviour {

    public float life = 3.0f;
    float fullLife;
    float timer = 0.2f;
    Color trueColor;
    SpriteRenderer sr;
    Collider2D c2D;
	// Use this for initialization
	void Start () {
		float x = (float)((Random.value*5)-2.5);
		float y= (float)((Random.value*6)-5);
        c2D = this.gameObject.GetComponent<Collider2D>();
        sr = this.gameObject.GetComponent<SpriteRenderer>();
        trueColor = sr.color;
        sr.color = Color.yellow;
        c2D.enabled = false;
        this.gameObject.transform.position=new Vector3(x,y,1);
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
            if(life == fullLife)
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
