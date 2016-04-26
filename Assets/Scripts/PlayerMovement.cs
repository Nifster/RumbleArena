using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	// Use this for initialization
	//float comboTimer = 0;
	//public float comboTimerMax = 2;
	private Combo falconPunch= new Combo(new KeyCode[] {KeyCode.A, KeyCode.S,KeyCode.D});
	private Combo falconKick= new Combo(new KeyCode[] {KeyCode.S, KeyCode.S,KeyCode.A});
	public float speed = 2f;

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Vector2 velocity = Vector2.zero;
		if(Input.GetKey(KeyCode.A)){
			velocity -= Vector2.right*speed;
		}
		if(Input.GetKey(KeyCode.D)){
			velocity += Vector2.right*speed;
		}
		GetComponent<Rigidbody2D>().velocity  = velocity;
	
		if (falconPunch.Check())
		{
			// do the falcon punch
			Debug.Log("PUNCH"); 
		}		
		if (falconKick.Check())
		{
			// do the falcon punch
			Debug.Log("KICK"); 
		}


	}

	void Combo(){
		//comboTimer -= Time.deltaTime;
		//Debug.Log(comboTimer);
	}


}

