using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour {
    public float speed;
	public Camera cam;
    public Vector3 target;
	private Vector3 touchTarget;
	public float deltaX, deltaY;
	public Rigidbody2D rb;
	public GameObject PlayingStuff;
	public GameObject GameOverStuff;
	
	// Use this for initialization
	void Start () {
		speed = 3f;
		target = this.transform.position;
		rb = GetComponent<Rigidbody2D> ();
		#if UNITY_STANDALONE
		Cursor.visible=false;
		#endif
	}
	
	// Update is called once per frame
	void Update () {
		
		/*if(Input.touches.Length>0 && Input.touches[0].phase!=TouchPhase.Ended && Input.touches[0].phase!=TouchPhase.Canceled){
			touchTarget.x = Input.touches[0].position.x;
			touchTarget.y = Input.touches[0].position.y;
		}
		Vector2 touchPos;
		touchPos.x = touchTarget.x;
        touchPos.y = touchTarget.y;
		if(touchTarget.x==0 & touchTarget.y==0)
		{
		}
		else
		{
			target = cam.ScreenToWorldPoint(new Vector3(touchPos.x, touchPos.y, cam.nearClipPlane));
			target.z=0;
		}
        Vector3 newPos = new Vector3(((this.transform.position.x-target.x)),((this.transform.position.y-target.y)),0);
		newPos = unitVectorize(newPos);
		newPos.x*=(speed*Time.deltaTime);
		newPos.y*=(speed*Time.deltaTime);
		newPos.x=this.transform.position.x-newPos.x;
		newPos.y=this.transform.position.y-newPos.y;
		this.transform.position=newPos;
		if((Mathf.Abs(this.transform.position.x-target.x)<(0.01*speed*speed)) && (Mathf.Abs(this.transform.position.y-target.y)<(0.01*speed*speed)))
		{
			this.transform.position=target;
		}*/
		if(Statics.masterMind.gameState!=2)
		{
			#if UNITY_ANDROID
				if (Input.touchCount > 0) {
				

					Touch touch = Input.GetTouch (0);


					Vector2 touchPos = Camera.main.ScreenToWorldPoint (touch.position);

					if(Vector2.Distance(touchPos,transform.position)<0.9)
					{
						switch (touch.phase) {


							case TouchPhase.Began:	


								if (GetComponent<Collider2D> () == Physics2D.OverlapPoint (touchPos)) {
									deltaX = touchPos.x - transform.position.x;
									deltaY = touchPos.y - transform.position.y;
								}
								break;

							case TouchPhase.Moved:
								rb.MovePosition (new Vector2 (touchPos.x - deltaX, touchPos.y - deltaY));
								break;
							default:
								break;
						}
						print("fast");
					}
					else
					{
						rb.MovePosition (new Vector2 (touchPos.x - (deltaX*Time.deltaTime), touchPos.y - (deltaY*Time.deltaTime)));
						print("Slow");
					}
				}
			#endif
			
			#if UNITY_STANDALONE
				Vector3 screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
				Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
				Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint);
				transform.position = cursorPosition;
			#endif
		}
		else
		{
		}
	}
	
	void OnCollisionEnter2D (Collision2D coll)
    {
		Debug.Log("Contact");
        if(coll.gameObject.tag=="Enemy")
        {
			coll.gameObject.tag="Winner";
            lose();
        }
    }
	
	void OnTriggerEnter2D (Collider2D other)
	{
		Debug.Log("hContact");
		if(other.gameObject.tag=="Enemy")
        {
			other.gameObject.tag="Winner";
            lose();
        }
		else if(other.gameObject.tag=="PowerUp")
		{
			foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
			{
				Destroy(obj);
			}
			Statics.masterMind.hasPowerUp=false;
			Statics.masterMind.powerUpCountdown=20;
			Destroy(other.gameObject);
		}
	}
	
	void lose()//when you lose... WIP
	{
		foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
		{
			Destroy(obj);
		}
		GameOverStuff.SetActive(true);
		PlayingStuff.SetActive(false);
		Statics.masterMind.gameState = 2;
		Statics.masterMind.x=60;
		Statics.masterMind.phase=4;
		#if UNITY_STANDALONE
		Cursor.visible=true;
		#endif
		
	}

    
}
