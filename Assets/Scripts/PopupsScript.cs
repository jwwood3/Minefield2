using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupsScript : MonoBehaviour {

    public float life = 3.0f;
	// Use this for initialization
	void Start () {
		float x = (float)((Random.value*17)-8.5);
		float y= (float)((Random.value*10)-5);
		this.gameObject.transform.position=new Vector3(x,y,1);
	}

    void FixedUpdate()
    {
        life -= Time.fixedDeltaTime;
        if (life <= 0)
        {
            Destroy(this.gameObject);
        }
    }
	
	
}
