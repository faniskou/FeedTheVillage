using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

	Rigidbody pl = new Rigidbody ();
	private float speeed = 3;
	private int camerastate = 0 ;
	private Vector3 offset;
	private Vector3 back = new Vector3 (0,0,10);
	private int Score,oldScore,countScore = 0 ;
	public GameObject pcam,pcamdefault ;
	public GameObject sboard,hboard,wboard ;
	public float j = 0;
	private System.DateTime changecameraTime;
	// Fix Update is called once per frame
	void Start(){
		pl = this.gameObject.GetComponent<Rigidbody> ();
		pcam.SetActive(false); pcamdefault.SetActive(true); 
		offset = pcam.transform.position - gameObject.transform.position;
		wboard.SetActive (false);
		changecameraTime = System.DateTime.Now;

	}

	void FixedUpdate () {
		//kathigitis anastas@unipi.gr 
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		bool jb = Input.GetKey("space");
		bool chabngecamera = Input.GetKey("c");
		if (chabngecamera == true && (System.DateTime.Now - changecameraTime ) >= System.TimeSpan.FromMilliseconds(200)   )
		{ 
			if (camerastate == 0  )
			{
				pcamdefault.SetActive(false);  pcam.SetActive(true); camerastate = 1 ;
				sboard.transform.parent =  pcam.transform;
				sboard.transform.position = sboard.transform.position + back;


			}
			else {pcam.SetActive(false); pcamdefault.SetActive(true);camerastate = 0 ; 
				sboard.transform.parent =  pcamdefault.transform;
				sboard.transform.position = sboard.transform.position - back;
			}
			changecameraTime = System.DateTime.Now;
		}
		//else {}

		if (jb == true && pl.position.y<=0.7f)
		{ j =100;}
		else {j=0;}
		//Debug.Log ("this is H: " + h +" -- this is v: "+ v);
	    if (pl.position.y <= -5)
		{
			ResetBall();

		}
		pl.AddForce (h*speeed, j, v*speeed);
//		Vector3 a = new Vector3 (pl.transform.position.x,pl.transform.position.y + j ,pl.transform.position.z);
//		pl.MovePosition ( a);
		Vector3 new_pos = gameObject.transform.position + offset;
		pcam.transform.position = new_pos;

	}
	void OnTriggerEnter (Collider other)
	{ 
		if (other.gameObject.CompareTag ("Pick Up")) 
		{
			Destroy(other.gameObject);
			Score = Score + 1;
			countScore = countScore +1 ;
			ScoreboardUpdate ();
			if (countScore >= 12)
			{
				if (Score >= 12)
				{wboard.GetComponent<TextMesh>().text = "PErfect Score !!!  Your Score Is  " + Score;}
				else
				{wboard.GetComponent<TextMesh>().text = "The End. Your Score Is " + Score;}
				wboard.SetActive(true);

			}
		}
	}
	void ResetBall ()
	{
		Vector3 a = new Vector3 (0,7,0);
		pl.MovePosition (a);
		Score = Score - 5;

		ScoreboardUpdate ();

	}
	void ScoreboardUpdate ()
	{ 
		string text;
		int number;
		text = "Score : " + Score;
		number =  oldScore -Score;
		if (oldScore != 0 )
		{
			text = text + "\nLast action  -" + number;
		}
		oldScore = Score ;
		sboard.GetComponent<TextMesh>().text = text;

	}


}
